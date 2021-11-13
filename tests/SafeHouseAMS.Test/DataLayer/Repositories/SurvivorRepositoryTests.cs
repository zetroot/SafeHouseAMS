using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SafeHouseAMS.BizLayer.Survivors;
using SafeHouseAMS.DataLayer;
using SafeHouseAMS.DataLayer.Models.Survivors;
using SafeHouseAMS.DataLayer.Repositories;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.DataLayer.Repositories
{
    public class SurvivorRepositoryTests
    {
        [Fact, UnitTest]
        public void Ctor_WhenDataContextIsNull_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new SurvivorsRepository(null!, Mock.Of<IMapper>()));

        [Fact, UnitTest]
        public void Ctor_WhenMapperIsNull_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new SurvivorsRepository(new DataContext(new DbContextOptions<DataContext>()), null!));

        private IMapper CreateMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(SurvivorsRepository).Assembly));
            return new Mapper(cfg);
        }

        [Fact, IntegrationTest]
        public async Task GetTotalCount_WhenCalled_DoesntCountDeleted()
        {
            //arrange
            await using var ctx = InMemDbHelper.CreateInMemoryDatabase();
            await ctx.Survivors.AddRangeAsync(new SurvivorDAL {ID = Guid.NewGuid(), IsDeleted = true, Num = 4},
            new() {ID = Guid.NewGuid(), IsDeleted = false, Num = 1},
            new() {ID = Guid.NewGuid(), IsDeleted = true, Num = 2},
            new() {ID = Guid.NewGuid(), IsDeleted = false, Num = 3});
            await ctx.SaveChangesAsync();

            var sut = new SurvivorsRepository(ctx, CreateMapper());
            //act
            var cnt = await sut.GetTotalCount();

            //assert
            cnt.Should().Be(2);
        }

        [Theory,IntegrationTest]
        [InlineData(false, "name", SexEnum.Female, null)]
        [InlineData(true, "name", SexEnum.Female, null)]
        [InlineData(false, "", SexEnum.Female, null)]
        [InlineData(false, "name", SexEnum.Other, null)]
        [InlineData(false, "name", SexEnum.Other, "")]
        [InlineData(false, "name", SexEnum.Other, "other")]
        public async Task Create_WhenCalled_ActuallyAddsNewRecord(bool isDeleted, string name, SexEnum sex, string? otherSex)
        {
            //arrange
            await using var ctx = InMemDbHelper.CreateInMemoryDatabase();
            var sut = new SurvivorsRepository(ctx, CreateMapper());
            var id = Guid.NewGuid();
            var time = DateTime.Now;

            //act
            await sut.Create(id, isDeleted, time, time, name, sex, otherSex, new DateTime(1970, 01, 01), null);

            //assert
            var foundRecord = await ctx.Survivors.SingleAsync(x => x.ID == id);

            foundRecord.ID.Should().Be(id);
            foundRecord.IsDeleted.Should().Be(isDeleted);
            foundRecord.Created.Should().Be(time);
            foundRecord.LastEdit.Should().Be(time);
            foundRecord.Name.Should().Be(name);
            foundRecord.Sex.Should().Be((int)sex);
            foundRecord.OtherSex.Should().Be(otherSex);
            foundRecord.BirthDateAccurate.Should().Be(new DateTime(1970,01,01));
            foundRecord.BirthDateCalculated.Should().BeNull();
        }

        [Fact,IntegrationTest]
        public async Task Create_WhenCalled_IncrementsNumValue()
        {
            //arrange
            await using var ctx = InMemDbHelper.CreateInMemoryDatabase();
            await ctx.Survivors.AddAsync(new()
            {
                ID = Guid.NewGuid(),
                Num = 42,
                Name = ""
            });
            await ctx.SaveChangesAsync();
            var sut = new SurvivorsRepository(ctx, CreateMapper());
            var id = Guid.NewGuid();

            //act
            await sut.Create(id, false, DateTime.Now, DateTime.Now, "name", SexEnum.Female, default, default, default);

            //assert
            var foundRecord = await ctx.Survivors.SingleAsync(x => x.ID == id);
            foundRecord.Num.Should().Be(43);
        }

        [Fact, IntegrationTest]
        public async Task GetSingleAsync_WhenCalled_ReturnsEntity()
        {
            //arrange
            var id = Guid.NewGuid();
            await using var ctx = InMemDbHelper.CreateInMemoryDatabase();
            await ctx.Survivors.AddAsync(new()
            {
                ID = id,
                Num = 42,
                Name = ""
            });
            await ctx.SaveChangesAsync();
            var sut = new SurvivorsRepository(ctx, CreateMapper());

            //act
            var foundRecord = await sut.GetSingleAsync(id, CancellationToken.None);

            //assert
            foundRecord.Should().NotBeNull();
            foundRecord?.ID.Should().Be(id);
        }

        [Fact, IntegrationTest]
        public async Task GetSingleAsync_WhenRecordIsDeleted_Throws()
        {
            //arrange
            var id = Guid.NewGuid();
            await using var ctx = InMemDbHelper.CreateInMemoryDatabase();
            await ctx.Survivors.AddAsync(new()
            {
                ID = id,
                IsDeleted = true,
                Num = 42,
                Name = ""
            });
            await ctx.SaveChangesAsync();
            var sut = new SurvivorsRepository(ctx, CreateMapper());

            //act
            var result = await sut.GetSingleAsync(id, CancellationToken.None);

            //assert
            result.Should().BeNull();
        }

        [Fact, IntegrationTest]
        public async Task GetSingleAsync_WhenCancelled_ThrowsOperationCancelled()
        {
            //arrange
            await using var ctx = InMemDbHelper.CreateInMemoryDatabase();
            var sut = new SurvivorsRepository(ctx, CreateMapper());
            var ct = new CancellationToken(true);
            //act && assert
            await Assert.ThrowsAnyAsync<OperationCanceledException>(() => sut.GetSingleAsync(default, ct));
        }

        [Fact, IntegrationTest]
        public async Task GetCollection_WhenLimited_ReturnsNotDeletedRecords()
        {
            //arrange
            await using var ctx = InMemDbHelper.CreateInMemoryDatabase();
            await ctx.Survivors.AddRangeAsync(
            new SurvivorDAL {ID = Guid.NewGuid(), IsDeleted = true, Num = 42, Name = "", LastEdit = DateTime.Now-TimeSpan.FromMinutes(5)},
            new () {ID = Guid.NewGuid(), IsDeleted = false, Num = 43, Name = "",LastEdit = DateTime.Now-TimeSpan.FromMinutes(4)},
            new () {ID = Guid.NewGuid(), IsDeleted = false, Num = 44, Name = "",LastEdit = DateTime.Now-TimeSpan.FromMinutes(3)},
            new () {ID = Guid.NewGuid(), IsDeleted = false, Num = 45, Name = "",LastEdit = DateTime.Now-TimeSpan.FromMinutes(2)},
            new () {ID = Guid.NewGuid(), IsDeleted = false, Num = 46, Name = "",LastEdit = DateTime.Now-TimeSpan.FromMinutes(1)},
            new () {ID = Guid.NewGuid(), IsDeleted = false, Num = 47, Name = "",LastEdit = DateTime.Now-TimeSpan.FromMinutes(0)}
            );
            await ctx.SaveChangesAsync();
            var sut = new SurvivorsRepository(ctx, CreateMapper());

            //act
            var result = new List<Survivor>();
            await foreach (var s in sut.GetCollection(1, 3, CancellationToken.None))
            {
                result.Add(s);
            }
            result.Should().HaveCount(3)
                .And.Contain(x => x.Num == 44)
                .And.Contain(x => x.Num == 45)
                .And.Contain(x => x.Num == 46);
        }

        [Fact, IntegrationTest]
        public async Task GetCollection_WhenUnLimited_ReturnsNotDeletedRecords()
        {
            //arrange
            await using var ctx = InMemDbHelper.CreateInMemoryDatabase();
            await ctx.Survivors.AddRangeAsync(
            new SurvivorDAL {ID = Guid.NewGuid(), IsDeleted = true, Num = 42, Name = "", LastEdit = DateTime.Now-TimeSpan.FromMinutes(5)},
            new () {ID = Guid.NewGuid(), IsDeleted = false, Num = 43, Name = "",LastEdit = DateTime.Now-TimeSpan.FromMinutes(4)},
            new () {ID = Guid.NewGuid(), IsDeleted = false, Num = 44, Name = "",LastEdit = DateTime.Now-TimeSpan.FromMinutes(3)},
            new () {ID = Guid.NewGuid(), IsDeleted = false, Num = 45, Name = "",LastEdit = DateTime.Now-TimeSpan.FromMinutes(2)},
            new () {ID = Guid.NewGuid(), IsDeleted = false, Num = 46, Name = "",LastEdit = DateTime.Now-TimeSpan.FromMinutes(1)},
            new () {ID = Guid.NewGuid(), IsDeleted = false, Num = 47, Name = "",LastEdit = DateTime.Now-TimeSpan.FromMinutes(0)}
            );
            await ctx.SaveChangesAsync();
            var sut = new SurvivorsRepository(ctx, CreateMapper());

            //act
            var result = new List<Survivor>();
            await foreach (var s in sut.GetCollection(1, null, CancellationToken.None))
            {
                result.Add(s);
            }
            result.Should().HaveCount(4)
                .And.Contain(x => x.Num == 43)
                .And.Contain(x => x.Num == 44)
                .And.Contain(x => x.Num == 45)
                .And.Contain(x => x.Num == 46);
        }
    }
}

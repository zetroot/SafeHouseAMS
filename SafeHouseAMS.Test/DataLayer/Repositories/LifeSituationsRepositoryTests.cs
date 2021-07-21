using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.InquirySources;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.DataLayer;
using SafeHouseAMS.DataLayer.Models.LifeSituations;
using SafeHouseAMS.DataLayer.Repositories;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.DataLayer.Repositories
{
    public class LifeSituationsRepositoryTests
    {
        private IMapper CreateMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(SurvivorsRepository).Assembly));
            return new Mapper(cfg);
        }
        
        private DataContext CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var dbctxOptsBuilder = new DbContextOptionsBuilder()
                .UseLazyLoadingProxies()
                .UseSqlite(connection, opt => 
                    opt.MigrationsAssembly(typeof(DataContext).Assembly.FullName));
            var ctx = new DataContext(dbctxOptsBuilder.Options);
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            return ctx;
        }

        [Fact,UnitTest]
        public void Ctor_WhenDataContextIsNull_Throws() =>
            Assert.Throws<ArgumentNullException>(() => 
                new LifeSituationDocumentsRepository(null!, Mock.Of<IMapper>()));
        
        [Fact, UnitTest]
        public void Ctor_WhenMapperIsNull_Throws() =>
            Assert.Throws<ArgumentNullException>(() => 
                new LifeSituationDocumentsRepository(new DataContext(new DbContextOptions<DataContext>()), null!));

        
        [Fact, IntegrationTest]
        public async Task GetSingleAsync_WhenCalled_ReturnsEntity()
        {
            //arrange
            var id = Guid.NewGuid();
            await using var ctx = CreateInMemoryDatabase();
            var survivorId = Guid.NewGuid();
            await ctx.Survivors.AddAsync(new() {ID = survivorId, Num = 42, Name = "name"});
            await ctx.LifeSituationDocuments.AddAsync(new InquiryDAL {ID = id, SurvivorID = survivorId});
            var citizenshipRecord = new CitizenshipRecord(Guid.NewGuid(), "c");
            await ctx.Records.AddAsync(new CitizenshipRecordDAL {ID = Guid.NewGuid(), Content = JsonSerializer.Serialize(citizenshipRecord), DocumentID = id});
            await ctx.SaveChangesAsync();
            var sut = new LifeSituationDocumentsRepository(ctx, CreateMapper());
            
            //act
            var foundRecord = await sut.GetSingleAsync(id, CancellationToken.None);
            
            //assert
            foundRecord.ID.Should().Be(id);
        }
        
        [Fact, IntegrationTest]
        public async Task GetSingleAsync_WhenRecordIsDeleted_Throws()
        {
            //arrange
            var id = Guid.NewGuid();
            await using var ctx = CreateInMemoryDatabase();

            var survivorId = Guid.NewGuid();
            await ctx.Survivors.AddAsync(new() {ID = survivorId, Num = 42, Name = "name"});
            await ctx.LifeSituationDocuments.AddAsync(new InquiryDAL{ID = id, IsDeleted = true, SurvivorID = survivorId});
            await ctx.SaveChangesAsync();
            var sut = new LifeSituationDocumentsRepository(ctx, CreateMapper());
            
            //act && assert
            await Assert.ThrowsAnyAsync<Exception>(() => sut.GetSingleAsync(id, CancellationToken.None));
        }
        
        [Fact, IntegrationTest]
        public async Task GetSingleAsync_WhenCancelled_ThrowsOperationCancelled()
        {
            //arrange
            await using var ctx = CreateInMemoryDatabase();
            var sut = new LifeSituationDocumentsRepository(ctx, CreateMapper());
            var ct = new CancellationToken(true);
            //act && assert
            await Assert.ThrowsAnyAsync<OperationCanceledException>(() => sut.GetSingleAsync(default, ct));
        }
        
        [Fact,IntegrationTest]
        public async Task CreateInquiry_WhenCalled_ActuallyAddsNewRecord()
        {
            //arrange
            await using var ctx = CreateInMemoryDatabase();
            var surId = Guid.NewGuid();
            await ctx.Survivors.AddAsync(new() {ID = surId, Num = 42, Name = "ololo"});
            await ctx.SaveChangesAsync();
            var sut = new LifeSituationDocumentsRepository(ctx, CreateMapper());
            var id = Guid.NewGuid();
            var time = DateTime.Now;
            
            //act
            await sut.CreateInquiry(id, false, time, time, surId, DateTime.Today, false, new IInquirySource[0]);
            
            //assert
            var foundRecord = await ctx.LifeSituationDocuments.SingleAsync(x => x.ID == id);

            foundRecord.ID.Should().Be(id);
            foundRecord.IsDeleted.Should().Be(false);
            foundRecord.Created.Should().Be(time);
            foundRecord.LastEdit.Should().Be(time);
        }
    }
}
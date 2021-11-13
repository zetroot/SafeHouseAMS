using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.Commands;
using SafeHouseAMS.DataLayer.Models.LifeSituations;
using SafeHouseAMS.DataLayer.Repositories;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.BizLayer.LifeSituations
{
    public class EndToEndTests
    {
        private IMapper CreateMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(EpisodesRepository).Assembly));
            return new Mapper(cfg);
        }

        [Fact, IntegrationTest]
        public async Task DeleteCommand_WhenApplied_MakesDocumentInvisibleToGetSingle()
        {
            //arrange
            var dbContext = InMemDbHelper.CreateInMemoryDatabase();
            var repo = new LifeSituationDocumentsRepository(dbContext, CreateMapper());
            var bizLogic = new LifeSituationDocumentsAggregate(repo);

            var survivorId = Guid.NewGuid();
            var documentId = Guid.NewGuid();

            dbContext.Survivors.Add(new() { ID = survivorId, Name = "name", Num = 42});
            dbContext.LifeSituationDocuments.Add(new ChildrenUpdateDAL() { ID = documentId, SurvivorID = survivorId });

            await dbContext.SaveChangesAsync();

            var sut = new DeleteDocument(documentId);

            //act
            await bizLogic.ApplyCommand(sut, CancellationToken.None);
            var result = await bizLogic.GetSingleAsync(documentId, CancellationToken.None);

            //assert
            result.Should().BeNull();
        }

        [Fact, IntegrationTest]
        public async Task DeleteCommand_WhenApplied_MakesDocumentInvisibleToGetAllBySurvivor()
        {
            //arrange
            var dbContext = InMemDbHelper.CreateInMemoryDatabase();
            var repo = new LifeSituationDocumentsRepository(dbContext, CreateMapper());
            var bizLogic = new LifeSituationDocumentsAggregate(repo);

            var survivorId = Guid.NewGuid();
            var documentId = Guid.NewGuid();

            dbContext.Survivors.Add(new() { ID = survivorId, Name = "name", Num = 42});
            dbContext.LifeSituationDocuments.Add(new ChildrenUpdateDAL() { ID = documentId, SurvivorID = survivorId });

            await dbContext.SaveChangesAsync();

            var sut = new DeleteDocument(documentId);

            var docCnt = 0;
            //act
            await bizLogic.ApplyCommand(sut, CancellationToken.None);
            await foreach (var _ in bizLogic.GetAllBySurvivor(survivorId, CancellationToken.None))
                ++docCnt;

            //assert
            docCnt.Should().Be(0);
        }
    }
}

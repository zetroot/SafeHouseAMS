using System;
using System.Threading.Tasks;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Moq;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.Commands;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.BizLayer.LifeSituations
{
    public class CreateRecordUpdateDocumentTests
    {
        [Property]
        public void Ctor_Always_SetsProperties()
        {
            Arb.Register<NotNullStringsGenerators>();

            Prop.ForAll<DateTime>((docDate) =>
            {
                //arrange
                var docId = Guid.NewGuid();
                var survuvorId = Guid.NewGuid();

                //act
                var cmd = new CreateRecordUpdateDocument<BaseRecord>(docId, survuvorId, docDate);

                //assert
                cmd.EntityID.Should().Be(docId);
                cmd.SurvivorID.Should().Be(survuvorId);
                cmd.DocumentDate.Should().Be(docDate);
            }).QuickCheckThrowOnFailure();
        }

        [Fact, UnitTest]
        public Task ApplyOn_WhenRepoIsNull_Throws() =>
            Assert.ThrowsAsync<ArgumentNullException>(() =>
                new CreateRecordUpdateDocument<BaseRecord>(default, default, default)
                    .ApplyOn(null!));

        [Fact, UnitTest]
        public async Task ApplyOn_WhenCalled_InvokesRepoAddNewDocument()
        {
            //arrange
            var docId = Guid.NewGuid();
            var surId = Guid.NewGuid();
            var docDate = DateTime.Now - TimeSpan.FromDays(5);

            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock
                .Setup(x => x.CreateRecordUpdateDocument(
                It.IsAny<Guid>(),
                It.IsAny<bool>(),
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>(),
                It.IsAny<Guid>(),
            It.IsAny<DateTime>(),
                It.IsAny<Type>()));

            var sut = new CreateRecordUpdateDocument<BaseRecord>(docId, surId, docDate);

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x =>
                x.CreateRecordUpdateDocument(docId, false, It.IsAny<DateTime>(), It.IsAny<DateTime>(),
                surId, docDate, typeof(BaseRecord)), Times.Once());
        }
    }
}

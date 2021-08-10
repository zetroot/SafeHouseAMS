using System;
using System.Threading.Tasks;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Moq;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.Commands;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.Test.Transport.MapperProfiles;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.BizLayer.LifeSituations
{
    public class CreateChildrenChangeCommandTests
    {
        [Property]
        public void Ctor_Always_SetsProperties()
        {
            Prop.ForAll<DateTime, bool, string?>((docDate, haschildren, details) =>
            {
                //arrange
                var docId = Guid.NewGuid();
                var survuvorId = Guid.NewGuid();

                //act
                var cmd = new CreateChildrenChange(docId, survuvorId, docDate, haschildren, details);

                //assert
                cmd.EntityID.Should().Be(docId);
                cmd.SurvivorID.Should().Be(survuvorId);
                cmd.DocumentDate.Should().Be(docDate);
                cmd.HasChildren.Should().Be(haschildren);
                cmd.Details.Should().Be(details);
            }).QuickCheckThrowOnFailure();
        }

        [Fact, UnitTest]
        public Task ApplyOn_WhenRepoIsNull_Throws() =>
            Assert.ThrowsAsync<ArgumentNullException>(() =>
                new CreateChildrenChange(default, default, default, default, "details")
                    .ApplyOn(null!));

        [Fact, UnitTest]
        public async Task ApplyOn_WhenCalled_InvokesRepoAddNewDocument()
        {
            //arrange
            var docId = Guid.NewGuid();
            var surId = Guid.NewGuid();
            var docDate = DateTime.Now;
            const string citizenship = "citizenship";

            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock
                .Setup(x => x.CreateCitizenshipChange(
                It.IsAny<Guid>(),
                It.IsAny<bool>(),
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>(),
                It.IsAny<Guid>(),
            It.IsAny<DateTime>()));

            var sut = new CreateCitizenshipChange(docId, surId, docDate, citizenship);

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x =>
                x.CreateCitizenshipChange(docId, false, It.IsAny<DateTime>(), It.IsAny<DateTime>(),
                surId, docDate), Times.Once());
        }

        [Fact, UnitTest]
        public async Task ApplyOn_WhenCalled_InvokesRepoAddRecord()
        {
            //arrange
            var docId = Guid.NewGuid();
            var surId = Guid.NewGuid();
            var docDate = DateTime.Now;
            const string citizenship = "citizenship";

            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock
                .Setup(x => x.AddRecord(docId, It.IsAny<CitizenshipRecord>()));

            var sut = new CreateCitizenshipChange(docId, surId, docDate, citizenship);

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x => x.AddRecord(docId,
                It.Is<CitizenshipRecord>(r => r.Citizenship == citizenship)), Times.Once());
        }

        [Fact, UnitTest]
        public async Task ApplyOn_WhenCreateDocumentFails_DoesNotAddsRecord()
        {
            //arrange
            var docId = Guid.NewGuid();
            var surId = Guid.NewGuid();
            var docDate = DateTime.Now;
            const string citizenship = "citizenship";

            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock
                .Setup(x => x.CreateCitizenshipChange(
                It.IsAny<Guid>(),
                It.IsAny<bool>(),
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>(),
                It.IsAny<Guid>(),
                It.IsAny<DateTime>())).ThrowsAsync(new Exception());
            repoMock
                .Setup(x => x.AddRecord(It.IsAny<Guid>(), It.IsAny<CitizenshipRecord>()));

            var sut = new CreateCitizenshipChange(docId, surId, docDate, citizenship);

            //act
            try
            {
                await sut.ApplyOn(repoMock.Object);
            }
            catch (Exception) {}

            //assert
            repoMock.Verify(x => x.AddRecord(It.IsAny<Guid>(),
            It.IsAny<BaseRecord>()), Times.Never());
        }
    }
}

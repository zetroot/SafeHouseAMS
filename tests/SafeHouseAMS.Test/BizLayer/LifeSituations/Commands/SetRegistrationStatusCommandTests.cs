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

namespace SafeHouseAMS.Test.BizLayer.LifeSituations.Commands
{
    public class SetRegistrationStatusCommandTests
    {
        [Property, UnitTest]
        public void Ctor_Always_SetsProperties()
        {
            Arb.Register<NotNullStringsGenerators>();
            Prop.ForAll<Guid, string>((docId, details) =>
            {
                var sut = new RegistrationStatusRecord(docId, details);
                sut.ID.Should().Be(docId);
                sut.Details.Should().Be(details);
            }).QuickCheckThrowOnFailure();
        }

        [Fact, UnitTest]
        public Task ApplyOn_WhenRepositoryIsNull_Throws() =>
            Assert.ThrowsAsync<ArgumentNullException>(() =>
                new SetRegistrationStatus(Guid.NewGuid(), "details").ApplyOn(null!));

        [Fact, UnitTest]
        public async Task ApplyOn_WhenCalled_InvokesRepoAddRecord()
        {
            //arrange
            var docId = Guid.NewGuid();
            var sut = new SetRegistrationStatus(docId, "details");
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.AddRecord(It.IsAny<Guid>(), It.IsAny<BaseRecord>()));

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x =>
                x.AddRecord(docId, It.Is<RegistrationStatusRecord>(r => r.Details == "details")));
        }
    }
}

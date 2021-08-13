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
    public class SetCitizenshipCommandTests
    {
        [Property, Category("Property")]
        public void Ctor_Always_SetsProperties()
        {
            Arb.Register<NotNullStringsGenerators>();
            Prop.ForAll<Guid, string>((id,citizenship) =>
            {
                var cmd = new SetCitizenship(id, citizenship);

                cmd.EntityID.Should().Be(id);
                cmd.Citizenship.Should().Be(citizenship);
            }).QuickCheckThrowOnFailure();
        }

        [Fact, UnitTest]
        public Task ApplyOn_WhenRepositoryIsNull_Throws() =>
            Assert.ThrowsAsync<ArgumentNullException>(() =>
                new SetCitizenship(Guid.NewGuid(), "some").ApplyOn(null!));

        [Fact, UnitTest]
        public void Ctor_WhenCitizenshipIsNull_Throws() =>
            Assert.Throws<ArgumentNullException>(() =>
                new SetCitizenship(Guid.NewGuid(), null!));

        [Fact, UnitTest]
        public async Task ApplyOn_WhenCalled_InvokesRepoAddRecord()
        {
            //arrange
            var docId = Guid.NewGuid();
            var sut = new SetCitizenship(docId, "details");
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.AddRecord(It.IsAny<Guid>(), It.IsAny<BaseRecord>()));

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x =>
                x.AddRecord(docId, It.IsAny<CitizenshipRecord>()), Times.Once());
        }
    }
}

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.Commands;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.BizLayer.LifeSituations.Commands
{
    public class SetDomicileCommandTests
    {
        [Theory, UnitTest]
        [InlineData("place", DomicileRecord.PlaceKind.OwnHome, "livingPlace", true, true, "children", "parents", "relatives", "people")]
        [InlineData("place", null, "", false, false, null, null, null, null)]
        [InlineData("place", null, "", false, false, null, null, null, "null")]
        public void Ctor_WhenCalled_SetsProperties(string place, DomicileRecord.PlaceKind? placeKind, string livingPlaceComment, bool livesAlone, bool withPartner, string? childrenDetails, string? parentsDetails,
            string? otherRelativesDetails, string? otherPeopleDetails)
        {
            //arrange
            var docId = Guid.NewGuid();

            //act
            var sut = new SetDomicile(docId, place, placeKind, livingPlaceComment, livesAlone, withPartner,
            childrenDetails, parentsDetails, otherRelativesDetails, otherPeopleDetails);

            //assert
            sut.EntityID.Should().Be(docId);
            sut.Place.Should().Be(place);
            sut.LivingPlaceComment.Should().Be(livingPlaceComment);
            sut.Kind.Should().Be(placeKind);
            sut.LivesAlone.Should().Be(livesAlone);
            sut.WithPartner.Should().Be(withPartner);

            sut.WithChildren.Should().Be(childrenDetails != null);
            sut.ChildrenDetails.Should().Be(childrenDetails);

            sut.WithParents.Should().Be(parentsDetails != null);
            sut.ParentsDetails.Should().Be(parentsDetails);

            sut.WithOtherRelatives.Should().Be(otherRelativesDetails != null);
            sut.OtherRelativesDetails.Should().Be(otherRelativesDetails);

            sut.WithOtherPeople.Should().Be(otherPeopleDetails != null);
            sut.OtherPeopleDetails.Should().Be(otherPeopleDetails);
        }

        [Fact, UnitTest]
        public void Ctor_WhenPlaceIsNull_Throws() =>
            Assert.Throws<ArgumentNullException>(() =>
                new SetDomicile(default, null!, default, "",
                default, default, default, default, default, default));

        [Fact, UnitTest]
        public Task ApplyOn_WhenRepositoryIsNull_Throws() =>
            Assert.ThrowsAsync<ArgumentNullException>(() =>
                new SetDomicile(default, "place", default, "",
                    default, default, default, default, default, default)
                    .ApplyOn(null!));

        [Fact,UnitTest]
        public async Task ApplyOn_WhenCalled_InvokesRepoAddRecordMethod()
        {
            //arrange
            var docId = Guid.NewGuid();
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.AddRecord(It.IsAny<Guid>(), It.IsAny<BaseRecord>()));
            var sut = new SetDomicile(docId, "place", default, "", default, default,
            default, default, default, default);

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x => x.AddRecord(docId, It.IsAny<DomicileRecord>()), Times.Once());
        }
    }
}

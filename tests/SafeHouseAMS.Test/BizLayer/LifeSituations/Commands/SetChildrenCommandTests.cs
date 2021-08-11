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
    public class SetChildrenCommandTests
    {
        [Theory, UnitTest]
        [InlineData(true, "details")]
        [InlineData(true, "")]
        [InlineData(true, null)]
        [InlineData(false, "details")]
        [InlineData(false, "")]
        [InlineData(false, null)]
        public void Ctor_Always_SetsProperties(bool hasChildren, string? childrenDetails)
        {
            //arrange
            var docId = Guid.NewGuid();

            //act
            var result = new SetChildren(docId, hasChildren, childrenDetails);

            //assert
            result.EntityID.Should().Be(docId);
            result.HasChildren.Should().Be(hasChildren);
            result.Details.Should().Be(childrenDetails);
        }

        [Fact, UnitTest]
        public Task ApplyOn_WhenRepositoryIsNull_Throws() =>
            Assert.ThrowsAsync<ArgumentNullException>(() =>
                new SetChildren(Guid.NewGuid(), true, "").ApplyOn(null!));

        [Theory, UnitTest]
        [InlineData(true, "details")]
        [InlineData(true, "")]
        [InlineData(true, null)]
        [InlineData(false, "details")]
        [InlineData(false, "")]
        [InlineData(false, null)]
        public async Task ApplyOn_WhenCalled_InvokesRepoAddRecord(bool hasChildren, string? details)
        {
            //arrange
            var docId = Guid.NewGuid();
            var sut = new SetChildren(docId, hasChildren, details);
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.AddRecord(It.IsAny<Guid>(), It.IsAny<BaseRecord>()));

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x =>
                x.AddRecord(docId, It.Is<ChildrenRecord>(r => r.HasChildren == hasChildren && r.Details == details)));
        }
    }
}

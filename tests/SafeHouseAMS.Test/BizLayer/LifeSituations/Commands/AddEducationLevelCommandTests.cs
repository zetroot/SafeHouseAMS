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
    public class AddEducationLevelCommandTests
    {
        [Theory, UnitTest]
        [InlineData(EducationLevelRecord.EduLevel.None, "details")]
        [InlineData(EducationLevelRecord.EduLevel.None, "")]
        [InlineData(EducationLevelRecord.EduLevel.None, null)]
        public void Ctor_Always_SetsProperties(EducationLevelRecord.EduLevel level, string? details)
        {
            //arrange
            var docId = Guid.NewGuid();

            //act
            var result = new AddEducationLevel(docId, level, details);

            //assert
            result.EntityID.Should().Be(docId);
            result.Level.Should().Be(level);
            result.Details.Should().Be(details);
        }

        [Fact, UnitTest]
        public Task ApplyOn_WhenRepositoryIsNull_Throws() =>
            Assert.ThrowsAsync<ArgumentNullException>(() =>
                new AddEducationLevel(Guid.NewGuid(), EducationLevelRecord.EduLevel.None, "").ApplyOn(null!));

        [Theory, UnitTest]
        [InlineData(EducationLevelRecord.EduLevel.None, "details")]
        [InlineData(EducationLevelRecord.EduLevel.None, "")]
        [InlineData(EducationLevelRecord.EduLevel.None, null)]
        public async Task ApplyOn_WhenCalled_InvokesRepoAddRecord(EducationLevelRecord.EduLevel level, string? details)
        {
            //arrange
            var docId = Guid.NewGuid();
            var sut = new AddEducationLevel(docId, level, details);
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.AddRecord(It.IsAny<Guid>(), It.IsAny<BaseRecord>()));

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x =>
                x.AddRecord(docId, It.Is<EducationLevelRecord>(r => r.Level == level && r.Details == details)));
        }
    }
}

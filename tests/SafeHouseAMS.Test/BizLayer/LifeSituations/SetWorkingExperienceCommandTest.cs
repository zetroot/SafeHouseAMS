using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.Commands;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.BizLayer.LifeSituations
{
    public class SetWorkingExperienceCommandTests
    {
        [Fact, UnitTest]
        public void Ctor_WhenDetailsIsNull_Throws() =>
            Assert.Throws<ArgumentNullException>(() => 
                new SetWorkingExperience(Guid.NewGuid(), null!));
        
        [Fact, UnitTest]
        public void Ctor_Always_SetsProperties()
        {
            //arrange
            var docId = Guid.NewGuid();
            
            //act
            var result = new SetWorkingExperience(docId, "experience");
            
            //assert
            result.EntityID.Should().Be(docId);
            result.Details.Should().Be("experience");
        }

        [Fact, UnitTest]
        public Task ApplyOn_WhenRepositoryIsNull_Throws() =>
            Assert.ThrowsAsync<ArgumentNullException>(() => 
                new SetWorkingExperience(Guid.NewGuid(), "details").ApplyOn(null!));

        [Fact, UnitTest]
        public async Task ApplyOn_WhenCalled_InvokesRepoSetWorkingExp()
        {
            //arrange
            var docId = Guid.NewGuid();
            const string details = "details";
            var sut = new SetWorkingExperience(docId, details);
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.SetWorkingExperience(It.IsAny<Guid>(), It.IsAny<string>()));
            
            //act
            await sut.ApplyOn(repoMock.Object);
            
            //assert
            repoMock.Verify(x => 
                x.SetWorkingExperience(docId, It.Is<string>(r => r == details)));
        }
    }
}
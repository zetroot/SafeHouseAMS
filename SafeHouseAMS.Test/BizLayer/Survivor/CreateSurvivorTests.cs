using System;
using System.Threading.Tasks;
using Moq;
using SafeHouseAMS.BizLayer.Survivor;
using SafeHouseAMS.BizLayer.Survivor.Commands;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.BizLayer.Survivor
{
    public class CreateSurvivorTests
    {
        [Fact, UnitTest]
        public void Ctor_WhenNameIsNull_throws() =>
            Assert.Throws<ArgumentNullException>(() => 
                new CreateSurvivor(Guid.Empty, null!, SexEnum.Female, null, null, null));

        [Fact, UnitTest]
        public async Task ApplyOn_WhenRepositoryIsNull_Throws()
        {
            //arrange
            var sut = new CreateSurvivor(default, "", default, default, default, default);
            
            // act && assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.ApplyOn(null!));
            
        }

        [Fact, UnitTest]
        public async Task ApplyOn_WhenCalled_InvokesRepositoryToCreate()
        {
            //arrange
            var id = Guid.NewGuid();
            const string name = "NAME";
            var sex = SexEnum.Other;
            const string otherSex = "OTHER";
            var accurateDob = DateTimeOffset.Now;
            var repoMock = new Mock<ISurvivorRepository>();
            repoMock.Setup(x => x.Create(It.IsAny<Guid>(), It.IsAny<string>(), 
                    It.IsAny<SexEnum>(), It.IsAny<string?>(),
                    It.IsAny<DateTimeOffset?>(), It.IsAny<DateTimeOffset?>()))
                .Returns(Task.CompletedTask);
            
            var sut = new CreateSurvivor(id, name, sex, otherSex, accurateDob, null);
            
            //act
            await sut.ApplyOn(repoMock.Object).ConfigureAwait(false);
            
            //assert
            repoMock.Verify(x => x.Create(id, name, sex, otherSex, accurateDob, null), Times.Once());
        }
    }
}
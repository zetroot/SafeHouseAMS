using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SafeHouseAMS.BizLayer.Survivor;
using SafeHouseAMS.BizLayer.Survivor.Commands;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.BizLayer.Survivor
{
    public class SurvivorCatalogueTests
    {
        [Fact, UnitTest]
        public async Task GetSingleAsync_WhenCalled_InvokesRepository()
        {
            //arrange
            var survivor = new SafeHouseAMS.BizLayer.Survivor.Survivor(default, false, default, default, "",
                default, default, default, default, default);
            var repoMock = new Mock<ISurvivorRepository>();
            repoMock.Setup(x => x.GetSingleAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(survivor));

            var sut = new SurvivorCatalogue(repoMock.Object);
            var requestId = Guid.NewGuid();
            //act
            var result = await sut.GetSingleAsync(requestId, CancellationToken.None);
            
            //assert
            repoMock.Verify(x => x.GetSingleAsync(requestId, CancellationToken.None), Times.Once());
            result.Should().BeSameAs(survivor);
        }
    }
}
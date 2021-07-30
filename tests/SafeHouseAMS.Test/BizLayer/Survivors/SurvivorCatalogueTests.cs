using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SafeHouseAMS.BizLayer.Survivors;
using SafeHouseAMS.BizLayer.Survivors.Commands;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.BizLayer.Survivors
{
    public class SurvivorCatalogueTests
    {
        [Fact, UnitTest]
        public async Task GetSingleAsync_WhenCalled_InvokesRepository()
        {
            //arrange
            var survivor = new Survivor(default, false, default, default, "",
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

        private class MockCommand : SurvivorCommand
        {
            public int ApplyOnInvocationsCounter { get; private set; }
            public ISurvivorRepository? LastUsedRepository { get; private set; }
            public MockCommand() : base(default) { }

            internal override Task ApplyOn(ISurvivorRepository repository)
            {
                ApplyOnInvocationsCounter++;
                LastUsedRepository = repository;
                return Task.CompletedTask;
            }
        }
        
        [Fact, UnitTest]
        public Task ApplyCommand_WhenCommandIsNull_Throws() =>
            Assert.ThrowsAsync<ArgumentNullException>(() =>
                new SurvivorCatalogue(Mock.Of<ISurvivorRepository>()).ApplyCommand(null!, new()));

        [Fact, UnitTest]
        public async Task ApplyCommand_WhenCalled_InvokesCommandApplyOnAndPassesRepository()
        {
            //arrange
            var repoMock = Mock.Of<ISurvivorRepository>();
            var sut = new SurvivorCatalogue(repoMock);
            var mockCmd = new MockCommand();

            //act
            await sut.ApplyCommand(mockCmd, new());
            
            //assert
            mockCmd.ApplyOnInvocationsCounter.Should().Be(1);
            mockCmd.LastUsedRepository.Should().BeSameAs(repoMock);
        }

        private async IAsyncEnumerable<T> GetEmptyAsyncEnumerable<T>()
        {
            await Task.Yield();
            yield break;
        }
        
        [Fact, UnitTest]
        public void GetCollection_WhenCalled_InvokesRepositoryAndReturnsItsAsyncEnum()
        {
            //arrange
            var asyncEnum = GetEmptyAsyncEnumerable<Survivor>();
            var repoMock = new Mock<ISurvivorRepository>();
            repoMock.Setup(x => x.GetCollection(It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).Returns(asyncEnum);
            var sut = new SurvivorCatalogue(repoMock.Object);

            //act
            var resultedEnum = sut.GetCollection(42, 54, CancellationToken.None);
            
            //assert
            repoMock.Verify(x => x.GetCollection(42, 54, CancellationToken.None), Times.Once());
            resultedEnum.Should().BeSameAs(asyncEnum);
        }
        
        [Fact, UnitTest]
        public async Task GetTotalCount_WhenCalled_InvokesRepositoryAndReturnsItsResult()
        {
            //arrangev
            var repoMock = new Mock<ISurvivorRepository>();
            repoMock.Setup(x => x.GetTotalCount()).ReturnsAsync(42);
            var sut = new SurvivorCatalogue(repoMock.Object);

            //act
            var result = await sut.GetTotalCount();
            
            //assert
            repoMock.Verify(x => x.GetTotalCount(), Times.Once());
            result.Should().Be(42);
        }
    }
}
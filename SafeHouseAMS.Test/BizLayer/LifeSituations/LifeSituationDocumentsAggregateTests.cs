using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.Commands;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.BizLayer.LifeSituations
{
    public class LifeSituationDocumentsAggregateTests
    {
        [Fact, UnitTest]
        public void Ctor_WhenRepositoryIsNull_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new LifeSituationDocumentsAggregate(null!));

        [Fact, UnitTest]
        public async Task GetSingleAsync_WhenCalled_InvokesRepository()
        {
            //arrange
            var id = Guid.NewGuid();
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.GetSingleAsync(id, It.IsAny<CancellationToken>()));
            var sut = new LifeSituationDocumentsAggregate(repoMock.Object);
            
            //act
            _ = await sut.GetSingleAsync(id, CancellationToken.None);
            
            //assert
            repoMock.Verify(x => x.GetSingleAsync(id, CancellationToken.None), Times.Once());
        }
        
        [Fact, UnitTest]
        public void GetAllBySurvivor_WhenCalled_InvokesRepository()
        {
            //arrange
            var id = Guid.NewGuid();
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.GetAllBySurvivor(id, It.IsAny<CancellationToken>()));
            var sut = new LifeSituationDocumentsAggregate(repoMock.Object);
            
            //act
            _ = sut.GetAllBySurvivor(id, CancellationToken.None);
            
            //assert
            repoMock.Verify(x => x.GetAllBySurvivor(id, CancellationToken.None), Times.Once());
        }
        
        [Fact, UnitTest]
        public async Task ApplyCommand_WhenCommandIsNull_Throws()
        {
            //arrange
            var sut = new LifeSituationDocumentsAggregate(Mock.Of<ILifeSituationDocumentsRepository>());
            
            //act && assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.ApplyCommand(null!, CancellationToken.None));
        }
        
        private class CmdMock : LifeSituationDocumentCommand
        {
            public int InvocationCount { get; private set; }
            public ILifeSituationDocumentsRepository? LastRepo { get; private set; }
            public CmdMock() : base(Guid.NewGuid()) {}
            
            internal override Task ApplyOn(ILifeSituationDocumentsRepository repository)
            {
                InvocationCount++;
                LastRepo = repository;
                return Task.CompletedTask;
            }
        }
        
        [Fact, UnitTest]
        public async Task ApplyCommand_WhenCalled_InvokesCommandApplyOnMethod()
        {
            //arrange
            var repoMock = Mock.Of<ILifeSituationDocumentsRepository>();
            var cmdMock = new CmdMock();
            var sut = new LifeSituationDocumentsAggregate(repoMock);
            
            //act
            await sut.ApplyCommand(cmdMock, CancellationToken.None);
            
            //assert
            cmdMock.InvocationCount.Should().Be(1);
            cmdMock.LastRepo.Should().BeSameAs(repoMock);
        }

    }
}
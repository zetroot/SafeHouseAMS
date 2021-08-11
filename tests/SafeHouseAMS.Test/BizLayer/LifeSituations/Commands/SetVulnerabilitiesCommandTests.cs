using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.Commands;
using SafeHouseAMS.BizLayer.LifeSituations.Vulnerabilities;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.BizLayer.LifeSituations.Commands
{
    public class SetVulnerabilitiesCommandTests
    {
        [Fact, UnitTest]
        public void Ctor_WhenCollectionIsNull_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new SetVulnerabilities(default, null!));


        [Fact, UnitTest]
        public void Ctor_WhenCalled_SetVulnerabilities()
        {
            //arrange
            var collection = new List<Vulnerability>
            {
                new Addiction("addiction"),
                new Homelessness(),
                new Migration(),
                new ChildhoodViolence(),
                new OrphanageExperience(),
                new Other("other"),
                new HealthStatus(HealthStatus.HealthStatusVulnerabilityType.Other |
                                 HealthStatus.HealthStatusVulnerabilityType.HepatitisB,
                "other")
            };

            //act
            var sut = new SetVulnerabilities(default, collection);

            //assert
            sut.Vulnerabilities.Should().Contain(collection);
        }

        [Fact, UnitTest]
        public Task ApplyOn_WhenRepoIsNUll_Throws() =>
            Assert.ThrowsAsync<ArgumentNullException>(() =>
                new SetVulnerabilities(default, new Vulnerability[0]).ApplyOn(null!));

        [Fact, UnitTest]
        public async Task ApplyOn_WhenHasAddiction_InvokesSetAddictionRepoMethod()
        {
            //arrange
            var id = Guid.NewGuid();
            const string addictionKind = "addiction";
            var sut = new SetVulnerabilities(id, new[] {new Addiction(addictionKind)});
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.SetAddiction(It.IsAny<Guid>(), It.IsAny<string>()));

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x => x.SetAddiction(id, addictionKind), Times.Once());
        }

        [Fact, UnitTest]
        public async Task ApplyOn_WhenHasNoAddiction_InvokesClearAddictionRepoMethod()
        {
            //arrange
            var id = Guid.NewGuid();
            var sut = new SetVulnerabilities(id, new[] {new Other("")});
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.ClearAddiction(It.IsAny<Guid>()));

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x => x.ClearAddiction(id), Times.Once());
        }

        [Fact, UnitTest]
        public async Task ApplyOn_WhenHomeless_InvokesSetHomelessRepoMethod()
        {
            //arrange
            var id = Guid.NewGuid();
            var sut = new SetVulnerabilities(id, new[] {new Homelessness()});
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.SetHomeless(It.IsAny<Guid>()));

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x => x.SetHomeless(id), Times.Once());
        }

        [Fact, UnitTest]
        public async Task ApplyOn_WhenNotHomeless_InvokesClearHomelessRepoMethod()
        {
            //arrange
            var id = Guid.NewGuid();
            var sut = new SetVulnerabilities(id, new[] {new Other("")});
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.ClearHomeless(It.IsAny<Guid>()));

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x => x.ClearHomeless(id), Times.Once());
        }

        [Fact, UnitTest]
        public async Task ApplyOn_WhenMigration_InvokesSetMigrationRepoMethod()
        {
            //arrange
            var id = Guid.NewGuid();
            var sut = new SetVulnerabilities(id, new[] {new Migration()});
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.SetMigration(It.IsAny<Guid>()));

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x => x.SetMigration(id), Times.Once());
        }

        [Fact, UnitTest]
        public async Task ApplyOn_WhenNotMigration_InvokesClearMigrationRepoMethod()
        {
            //arrange
            var id = Guid.NewGuid();
            var sut = new SetVulnerabilities(id, new[] {new Other("")});
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.ClearMigration(It.IsAny<Guid>()));

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x => x.ClearMigration(id), Times.Once());
        }

        [Fact, UnitTest]
        public async Task ApplyOn_WhenChildhoodViolence_InvokesSetChildhoodViolenceRepoMethod()
        {
            //arrange
            var id = Guid.NewGuid();
            var sut = new SetVulnerabilities(id, new[] {new ChildhoodViolence()});
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.SetChildhoodViolence(It.IsAny<Guid>()));

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x => x.SetChildhoodViolence(id), Times.Once());
        }

        [Fact, UnitTest]
        public async Task ApplyOn_WhenNotChildhoodViolence_InvokesClearChildhoodViolenceRepoMethod()
        {
            //arrange
            var id = Guid.NewGuid();
            var sut = new SetVulnerabilities(id, new[] {new Other("")});
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.ClearChildhoodViolence(It.IsAny<Guid>()));

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x => x.ClearChildhoodViolence(id), Times.Once());
        }

        [Fact, UnitTest]
        public async Task ApplyOn_WhenOrphanageExperience_InvokesSetOrphanageExperienceRepoMethod()
        {
            //arrange
            var id = Guid.NewGuid();
            var sut = new SetVulnerabilities(id, new[] {new OrphanageExperience()});
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.SetOrphanageExperience(It.IsAny<Guid>()));

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x => x.SetOrphanageExperience(id), Times.Once());
        }

        [Fact, UnitTest]
        public async Task ApplyOn_WhenNotOrphanageExperience_InvokesClearOrphanageExperienceRepoMethod()
        {
            //arrange
            var id = Guid.NewGuid();
            var sut = new SetVulnerabilities(id, new[] {new Other("")});
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.ClearOrphanageExperience(It.IsAny<Guid>()));

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x => x.ClearOrphanageExperience(id), Times.Once());
        }

        [Fact, UnitTest]
        public async Task ApplyOn_WhenOther_InvokesSetOtherRepoMethod()
        {
            //arrange
            var id = Guid.NewGuid();
            const string details = "other";
            var sut = new SetVulnerabilities(id, new[] {new Other(details)});
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.SetOther(It.IsAny<Guid>(), It.IsAny<string>()));

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x => x.SetOther(id, details), Times.Once());
        }

        [Fact, UnitTest]
        public async Task ApplyOn_WhenNotOther_InvokesClearOtherRepoMethod()
        {
            //arrange
            var id = Guid.NewGuid();
            var sut = new SetVulnerabilities(id, new[] {new Homelessness()});
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.ClearOther(It.IsAny<Guid>()));

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x => x.ClearOther(id), Times.Once());
        }

        [Fact, UnitTest]
        public async Task ApplyOn_WhenHealthStatusVulnerability_InvokesSetHealthStatusVulnerabilityRepoMethod()
        {
            //arrange
            var id = Guid.NewGuid();
            const string details = "details";
            var sut = new SetVulnerabilities(id, new[] {new HealthStatus(HealthStatus.HealthStatusVulnerabilityType.Other, details)});
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.SetHealthStatusVulnerability(It.IsAny<Guid>(), It.IsAny<HealthStatus.HealthStatusVulnerabilityType>(), It.IsAny<string?>()));

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x => x.SetHealthStatusVulnerability(id, HealthStatus.HealthStatusVulnerabilityType.Other, details), Times.Once());
        }

        [Fact, UnitTest]
        public async Task ApplyOn_WhenNotHealthStatusVulnerability_InvokesClearHealthStatusVulnerabilityRepoMethod()
        {
            //arrange
            var id = Guid.NewGuid();
            var sut = new SetVulnerabilities(id, new[] {new Homelessness()});
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.ClearHealthStatusVulnerability(It.IsAny<Guid>()));

            //act
            await sut.ApplyOn(repoMock.Object);

            //assert
            repoMock.Verify(x => x.ClearHealthStatusVulnerability(id), Times.Once());
        }
    }
}

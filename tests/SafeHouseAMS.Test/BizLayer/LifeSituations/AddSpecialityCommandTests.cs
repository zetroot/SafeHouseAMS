using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.Commands;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.BizLayer.LifeSituations
{
    public class AddSpecialityCommandTests
    {
        [Fact, UnitTest]
        public void Ctor_Always_SetsProperties()
        {
            //arrange
            var docId = Guid.NewGuid();
            var spec = "speciality";
            
            //act
            var result = new AddSpeciality(docId, spec);
            
            //assert
            result.EntityID.Should().Be(docId);
            result.Speciality.Should().Be(spec);
        }

        [Fact, UnitTest]
        public void Ctor_WhenSpecialityIsNull_Throws() => 
            Assert.Throws<ArgumentNullException>(() => new AddSpeciality(Guid.NewGuid(), null!));

        [Fact, UnitTest]
        public Task ApplyOn_WhenRepositoryIsNull_Throws() =>
            Assert.ThrowsAsync<ArgumentNullException>(() => 
                new AddSpeciality(Guid.NewGuid(), "speciality").ApplyOn(null!));

        [Fact, UnitTest]
        public async Task ApplyOn_WhenCalled_InvokesRepoAddRecord()
        {
            //arrange
            var docId = Guid.NewGuid();
            const string speciality = "speciality";
            var sut = new AddSpeciality(docId, speciality);
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.AddRecord(It.IsAny<Guid>(), It.IsAny<BaseRecord>()));
            
            //act
            await sut.ApplyOn(repoMock.Object);
            
            //assert
            repoMock.Verify(x => 
                x.AddRecord(docId, It.Is<SpecialityRecord>(r => r.Speciality == speciality)));
        }
    }
}
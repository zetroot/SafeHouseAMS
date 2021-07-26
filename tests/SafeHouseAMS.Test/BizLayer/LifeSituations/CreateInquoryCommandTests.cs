using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.Commands;
using SafeHouseAMS.BizLayer.LifeSituations.InquirySources;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.BizLayer.LifeSituations
{
    public class CreateInquoryCommandTests
    {
        [Fact, UnitTest]
        public void Ctor_WhenCalled_SetsPublicProperties()
        {
            //arrange
            var docId = Guid.NewGuid();
            var docDate = new DateTime(1999, 01, 30);
            var survivorID = Guid.NewGuid();
            var inquirySources = new List<IInquirySource>();
            const string citizenship = "citizenship";
            
            //act
            var cmd = new CreateInquiry(docId, docDate, survivorID, true, inquirySources, citizenship);
            
            //assert
            cmd.EntityID.Should().Be(docId);
            cmd.DocumentDate.Should().Be(docDate);
            cmd.SurvivorID.Should().Be(survivorID);
            cmd.IsJuvenile.Should().BeTrue();
            cmd.InquirySources.Should().BeSameAs(inquirySources);
            cmd.Citizenship.Should().Be(citizenship);
        }
        
        [Fact, UnitTest]
        public void Ctor_WhenInquirySourcesIsNull_Throws() => 
            Assert.Throws<ArgumentNullException>(() =>
                new CreateInquiry(default,default, default, default, null!, string.Empty));
            
        [Fact, UnitTest]
        public void Ctor_WhenCitizenshipIsNull_Throws() => 
            Assert.Throws<ArgumentNullException>(() =>
                new CreateInquiry(default,default, default, default, new List<IInquirySource>(), null!));
        
        [Fact, UnitTest]
        public void Ctor_WhenIsJuvenileIsNull_SetsIsJuvenilleToFalse()
        {
            //act
            var cmd = new CreateInquiry(default, default, default, null, new List<IInquirySource>(), "citizenship");
            
            //assert
            cmd.IsJuvenile.Should().BeFalse();
        }
        
        [Fact, UnitTest]
        public async Task AplpyOn_WhenCalled_InvokesRepository()
        {
            //arrange
            var cmd = new CreateInquiry(default, default, default, null, new List<IInquirySource>(), "citizenship");
            var repoMock = new Mock<ILifeSituationDocumentsRepository>();
            repoMock.Setup(x => x.CreateInquiry(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<bool>(), It.IsAny<IEnumerable<IInquirySource>>()));
            repoMock.Setup(x => x.AddRecord(It.IsAny<Guid>(), It.IsAny<BaseRecord>()));
            
            //act
            await cmd.ApplyOn(repoMock.Object);
            
            //assert
            repoMock.Verify(x => x.CreateInquiry(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<bool>(), It.IsAny<IEnumerable<IInquirySource>>()), Times.Once());
            repoMock.Verify(x => x.AddRecord(It.IsAny<Guid>(), It.IsAny<BaseRecord>()), Times.Once());
        }

    }
    
}
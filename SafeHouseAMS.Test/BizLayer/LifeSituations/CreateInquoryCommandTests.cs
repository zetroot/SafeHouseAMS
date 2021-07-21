using System;
using System.Collections.Generic;
using FluentAssertions;
using SafeHouseAMS.BizLayer.LifeSituations.Commands;
using SafeHouseAMS.BizLayer.LifeSituations.InquirySources;
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
        
    }
    
}
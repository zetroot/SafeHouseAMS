using System;
using System.Text.Json;
using AutoMapper;
using FluentAssertions;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.DataLayer.MapperProfiles;
using SafeHouseAMS.DataLayer.Models.LifeSituations;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.DataLayer.MapperProfiles
{
    public class RecordsMappingProfileTests
    {
        private Mapper BuildMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(RecordsMappingProfile)));
            return new(cfg);
        }

        [Theory, UnitTest]
        [InlineData(false, null)]
        [InlineData(false, "")]
        [InlineData(false, "42")]
        [InlineData(true, null)]
        [InlineData(true, "")]
        [InlineData(true, "42")]
        public void Mapper_MapsChildrenRecord_Dal2Bl(bool hasChildren, string? details)
        {
            //arrange
            var recordId = Guid.NewGuid();
            var srcRec = new ChildrenRecord(recordId, hasChildren, details);
            var src = new ChildrenRecordDAL
            {
                ID = recordId,
                Content = JsonSerializer.Serialize(srcRec)
            } as BaseRecordDAL;
            var sut = BuildMapper();
            
            //act
            var result = sut.Map<BaseRecord>(src);

            //assert
            result.Should().BeOfType<ChildrenRecord>()
                .And.BeEquivalentTo(srcRec);
        }
        
        [Theory, UnitTest]
        [InlineData("")]
        [InlineData("42")]
        public void Mapper_MapsCitizenshipRecord_Dal2Bl(string citizenship)
        {
            //arrange
            var recordId = Guid.NewGuid();
            var src = new CitizenshipRecordDAL
            {
                ID = recordId,
                Content = JsonSerializer.Serialize(new CitizenshipRecord(recordId, citizenship))
            } as BaseRecordDAL;
            var sut = BuildMapper();
            
            //act
            var result = sut.Map<BaseRecord>(src);

            //assert
            result.Should().BeOfType<CitizenshipRecord>()
                .And.BeEquivalentTo(new CitizenshipRecord(recordId, citizenship));
        }
        
        [Theory, UnitTest]
        [InlineData("", DomicileRecord.PlaceKind.OwnHome, true, true, true, "42", true, "43", true, "44", true, "45")]
        [InlineData("place", DomicileRecord.PlaceKind.OwnHome, true, true, true, "42", true, "43", true, "44", true, "45")]
        [InlineData("place", DomicileRecord.PlaceKind.Homeless, true, true, true, "42", true, "43", true, "44", true, "45")]
        [InlineData("place", DomicileRecord.PlaceKind.Homeless, false, false, false, null, true, null, true, null, true, null)]
        public void Mapper_MapsDomicileRecord_Dal2Bl(string place, DomicileRecord.PlaceKind? placeKind, 
            bool livesAlone,
            bool withPartner,
            bool withChildren, string? childrenDetails,
            bool withParents, string? parentsDetails,
            bool withOtherRelatives, string? otherRelativesDetails, 
            bool withOtherPeople, string? otherPeopleDetails)
        {
            //arrange
            var recordId = Guid.NewGuid();
            var recordBl = new DomicileRecord(recordId, place, placeKind, livesAlone, withPartner, withChildren, childrenDetails, 
            withParents, parentsDetails, withOtherRelatives, otherRelativesDetails, withOtherPeople, otherPeopleDetails);
            var src = new DomicileRecordDAL()
            {
                ID = recordId,
                Content = JsonSerializer.Serialize(recordBl)
            } as BaseRecordDAL;
            var sut = BuildMapper();
            
            //act
            var result = sut.Map<BaseRecord>(src);

            //assert
            result.Should().BeOfType<DomicileRecord>()
                .And.BeEquivalentTo(recordBl);
        }
    }
}
using System;
using AutoMapper;
using FluentAssertions;
using SafeHouseAMS.BizLayer.Survivors;
using SafeHouseAMS.DataLayer.MapperProfiles;
using SafeHouseAMS.DataLayer.Models.Survivors;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.DataLayer.MapperProfiles
{
    public class SurvivorMappingProfileTest
    {
        private Mapper BuildMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(SurvivorMappingProfile)));
            return new(cfg);
        }
        
        [Fact,UnitTest]
        public void Mapper_MapsSurvivor_Dal2Bl()
        {
            //arrange
            var survivorDal = new SurvivorDAL
            {
                ID = Guid.NewGuid(),
                Created = DateTime.Now,
                LastEdit = DateTime.Now,
                IsDeleted = true,
                Name = "name",
                Num = 42,
                Sex = 2,
                OtherSex = "other",
                BirthDateAccurate = DateTime.Now,
                BirthDateCalculated = null
            };

            var sut = BuildMapper();
            
            //act
            var result = sut.Map<Survivor>(survivorDal);
            
            //assert
            result.ID.Should().Be(survivorDal.ID);
            result.Created.Should().Be(survivorDal.Created);
            result.LastEdit.Should().Be(survivorDal.LastEdit);
            result.IsDeleted.Should().BeTrue();
            result.Name.Should().Be("name");
            result.Num.Should().Be(42);
            result.Sex.Should().Be(SexEnum.Other);
            result.OtherSex.Should().Be("other");
            result.BirthDateAccurate.Should().Be(survivorDal.BirthDateAccurate);
            result.BirthDateCalculated.Should().BeNull();
        }
        
    }
}
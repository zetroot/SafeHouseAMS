using AutoMapper;
using SafeHouseAMS.BizLayer.Survivors;
using SafeHouseAMS.DataLayer.Models;
using SafeHouseAMS.DataLayer.Models.Survivors;

namespace SafeHouseAMS.DataLayer.MapperProfiles
{
    internal class SurvivorMappingProfile : Profile
    {
        public SurvivorMappingProfile()
        {
            MapSurvivors();
        }
        private void MapSurvivors()
        {
            CreateMap<SurvivorDAL, Survivor>()
                .ConstructUsing(src => new (src.ID, src.IsDeleted, src.Created, src.LastEdit, 
                    src.Name, src.Num, (SexEnum) src.Sex, src.OtherSex, src.BirthDateAccurate, src.BirthDateCalculated));
        }
    }
}
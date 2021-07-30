using AutoMapper;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class SurvivorMappingProfile : Profile
    {
        public SurvivorMappingProfile()
        {
            MapSurvivors();
        }
        
        private void MapSurvivors()
        {
            CreateMap<Survivor, Backend.Protos.Models.Survivors.Survivor>();
            CreateMap<Backend.Protos.Models.Survivors.Survivor, Survivor>();
        }
    }
}
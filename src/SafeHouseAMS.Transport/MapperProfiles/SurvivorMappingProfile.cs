using AutoMapper;
using SafeHouseAMS.BizLayer.Survivors;
using SafeHouseAMS.BizLayer.Survivors.Commands;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class SurvivorMappingProfile : Profile
    {
        public SurvivorMappingProfile()
        {
            MapSurvivors();
            MapCreateSurvivorCommand();
        }
        private void MapCreateSurvivorCommand()
        {
            CreateMap<CreateSurvivor, Protos.Models.Survivors.CreateSurvivor>();
            CreateMap<Protos.Models.Survivors.CreateSurvivor, CreateSurvivor>();
        }

        private void MapSurvivors()
        {
            CreateMap<Survivor, Protos.Models.Survivors.Survivor>();
            CreateMap<Protos.Models.Survivors.Survivor, Survivor>();
        }
    }
}
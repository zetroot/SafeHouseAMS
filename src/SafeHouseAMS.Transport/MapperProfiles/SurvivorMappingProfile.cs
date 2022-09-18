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

            CreateMap<DeleteSurvivor, Protos.Models.Survivors.DeleteSurvivor>();
            CreateMap<Protos.Models.Survivors.DeleteSurvivor, DeleteSurvivor>();
            
            CreateMap<UpdateSurvivor, Protos.Models.Survivors.UpdateSurvivor>();
            CreateMap<Protos.Models.Survivors.UpdateSurvivor, UpdateSurvivor>();
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

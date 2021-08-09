using AutoMapper;
using SafeHouseAMS.BizLayer.LifeSituations;
namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class CitizenshipChangeMappingProfile : Profile
    {
        public CitizenshipChangeMappingProfile()
        {
            MapCitizenshipChange();
        }
        private void MapCitizenshipChange()
        {
            CreateMap<CitizenshipChange, Protos.Models.LifeSituations.CitizenshipChange>();
            CreateMap<Protos.Models.LifeSituations.CitizenshipChange, CitizenshipChange>();
        }
    }
}

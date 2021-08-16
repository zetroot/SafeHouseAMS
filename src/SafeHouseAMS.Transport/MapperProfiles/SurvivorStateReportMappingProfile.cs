using AutoMapper;
using SafeHouseAMS.BizLayer.LifeSituations;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class SurvivorStateReportMappingProfile : Profile
    {
        public SurvivorStateReportMappingProfile()
        {
            CreateMap<SurvivorStateReport, Protos.Models.LifeSituations.SurvivorStateReport>();
            CreateMap<Protos.Models.LifeSituations.SurvivorStateReport, SurvivorStateReport>();
        }
    }
}

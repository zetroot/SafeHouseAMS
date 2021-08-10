using AutoMapper;
using SafeHouseAMS.BizLayer.LifeSituations.Documents;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

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
            CreateMap<SingleRecordUpdate<CitizenshipRecord>, Protos.Models.LifeSituations.CitizenshipChange>();
            CreateMap<Protos.Models.LifeSituations.CitizenshipChange, SingleRecordUpdate<CitizenshipRecord>>();
        }
    }
}

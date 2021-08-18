using AutoMapper;
using SafeHouseAMS.BizLayer.LifeSituations;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class RecordHistoryItemMappingProfile : Profile
    {
        public RecordHistoryItemMappingProfile()
        {
            CreateMap<RecordHistoryItem, Protos.Models.LifeSituations.RecordHistoryItem>();
            CreateMap<Protos.Models.LifeSituations.RecordHistoryItem, RecordHistoryItem>();
        }
    }
}

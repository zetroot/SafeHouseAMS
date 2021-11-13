using AutoMapper;
using SafeHouseAMS.BizLayer.AssistanceRequests;

namespace SafeHouseAMS.Transport.MapperProfiles;

internal class AssistanceRequestsMappingProfile : Profile
{
    public AssistanceRequestsMappingProfile()
    {
        MapActs();
        MapRequests();
    }
    private void MapRequests()
    {
        CreateMap<AssistanceRequest, Protos.Models.AssistanceRequests.AssistanceRequest>();
        CreateMap<Protos.Models.AssistanceRequests.AssistanceRequest, AssistanceRequest>();
    }
    private void MapActs()
    {
        CreateMap<AssistanceAct, Protos.Models.AssistanceRequests.AssistanceAct>();
        CreateMap<Protos.Models.AssistanceRequests.AssistanceAct, AssistanceAct>();
    }
}

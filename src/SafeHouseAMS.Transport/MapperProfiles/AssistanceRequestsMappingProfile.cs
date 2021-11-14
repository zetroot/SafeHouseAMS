using AutoMapper;
using SafeHouseAMS.BizLayer.AssistanceRequests;
using SafeHouseAMS.BizLayer.AssistanceRequests.Commands;

namespace SafeHouseAMS.Transport.MapperProfiles;

internal class AssistanceRequestsMappingProfile : Profile
{
    public AssistanceRequestsMappingProfile()
    {
        MapActs();
        MapRequests();
        MapCreateCommand();
        MapAttachActCommand();
        MapCommands();
    }

    private void MapCommands()
    {

    }

    private void MapAttachActCommand()
    {
        CreateMap<AttachAssistanceAct, Protos.Models.AssistanceRequests.Commands.AttachAssistanceAct>()
            .ReverseMap();
    }

    private void MapCreateCommand()
    {
        CreateMap<CreateAssistanceRequest, Protos.Models.AssistanceRequests.Commands.CreateAssistanceRequest>()
            .ReverseMap();
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

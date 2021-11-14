using System;
using AutoMapper;
using SafeHouseAMS.BizLayer.AssistanceRequests;
using SafeHouseAMS.BizLayer.AssistanceRequests.Commands;
using DTO = SafeHouseAMS.Transport.Protos.Models.AssistanceRequests.Commands;

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
        CreateMap<AssistanceRequestCommand, DTO.AssistanceRequestCommand>()
            .ConstructUsing((src, ctx) =>
            {
                var result = new DTO.AssistanceRequestCommand();
                switch (src)
                {
                    case CreateAssistanceRequest car:
                        result.CreateRequest = ctx.Mapper.Map<DTO.CreateAssistanceRequest>(car);
                        break;
                    case AttachAssistanceAct aaa:
                        result.AttachAct = ctx.Mapper.Map<DTO.AttachAssistanceAct>(aaa);
                        break;
                    default:
                        throw new InvalidOperationException($"Command type {src.GetType().Name} is not supported");
                }
                return result;
            });

        CreateMap<DTO.AssistanceRequestCommand, AssistanceRequestCommand>()
            .ConstructUsing((src, ctx) => src.CommandCase switch
            {
                DTO.AssistanceRequestCommand.CommandOneofCase.CreateRequest =>
                    ctx.Mapper.Map<CreateAssistanceRequest>(src.CreateRequest),
                DTO.AssistanceRequestCommand.CommandOneofCase.AttachAct =>
                    ctx.Mapper.Map<AttachAssistanceAct>(src.AttachAct),
                _ => throw new InvalidOperationException($"Command type {src.CommandCase} is not supported")
            });
    }

    private void MapAttachActCommand()
    {
        CreateMap<AttachAssistanceAct, DTO.AttachAssistanceAct>()
            .ReverseMap();
    }

    private void MapCreateCommand()
    {
        CreateMap<CreateAssistanceRequest, DTO.CreateAssistanceRequest>()
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

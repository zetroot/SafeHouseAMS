using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using SafeHouseAMS.BizLayer.AssistanceRequests;
using SafeHouseAMS.Transport.Protos.Models.AssistanceRequests.Commands;
using SafeHouseAMS.Transport.Protos.Models.Common;
using SafeHouseAMS.Transport.Protos.Services;
using AssistanceRequest=SafeHouseAMS.Transport.Protos.Models.AssistanceRequests.AssistanceRequest;

namespace SafeHouseAMS.Backend.Server.Services;

[ExcludeFromCodeCoverage, Authorize]
internal class AssistanceRequestsCatalogueService : AssistanceRequestCatalogue.AssistanceRequestCatalogueBase
{
    private readonly IAssistanceRequestAggregate _aggregate;
    private readonly IMapper _mapper;

    public AssistanceRequestsCatalogueService(IAssistanceRequestAggregate aggregate, IMapper mapper)
    {
        _aggregate = aggregate;
        _mapper = mapper;
    }

    public override async Task<AssistanceRequest> GetSingle(UUID request, ServerCallContext context)
    {
        var id = _mapper.Map<Guid>(request);
        var item = await _aggregate.GetSingleAsync(id, context.CancellationToken).ConfigureAwait(false);
        return _mapper.Map<AssistanceRequest>(item);
    }

    public override async Task<Empty> ApplyCommand(AssistanceRequestCommand request, ServerCallContext context)
    {
        var command = _mapper.Map<BizLayer.AssistanceRequests.Commands.AssistanceRequestCommand>(request);
        await _aggregate.ApplyCommand(command, context.CancellationToken).ConfigureAwait(false);
        return new();
    }

    public override async Task GetAllBySurvivor(UUID request, IServerStreamWriter<AssistanceRequest> responseStream, ServerCallContext context)
    {
        var id = _mapper.Map<Guid>(request);
        var seq = _aggregate.GetAllBySurvivor(id, context.CancellationToken);

        await foreach (var item in seq)
        {
            var chunk = _mapper.Map<AssistanceRequest>(item);
            await responseStream.WriteAsync(chunk).ConfigureAwait(false);
        }
    }
}

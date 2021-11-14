using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using SafeHouseAMS.BizLayer.AssistanceRequests;
using SafeHouseAMS.BizLayer.AssistanceRequests.Commands;
using SafeHouseAMS.Transport.Protos.Models.Common;
using SafeHouseAMS.Transport.Protos.Services;

namespace SafeHouseAMS.WasmApp.Services
{
    internal class AssistanceRequestsCatalogueClient : GrpcClientBase, IAssistanceRequestAggregate
    {
        private readonly AssistanceRequestCatalogue.AssistanceRequestCatalogueClient _client;
        private readonly IMapper _mapper;

        public AssistanceRequestsCatalogueClient(GrpcChannel channel, IMapper mapper, ILogger<AssistanceRequestsCatalogueClient> logger) :
            base(logger)
        {
            _mapper = mapper;
            _client = new(channel);
        }

        public Task ApplyCommand(AssistanceRequestCommand command, CancellationToken cancellationToken) =>
            CallHandler(async () =>
            {
                var request =
                    _mapper.Map<Transport.Protos.Models.AssistanceRequests.Commands.AssistanceRequestCommand>(command);
                await _client.ApplyCommandAsync(request, new CallOptions(cancellationToken: cancellationToken));
            });

        public async IAsyncEnumerable<AssistanceRequest> GetAllBySurvivor(Guid survivorId, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var requestSurvivorId = _mapper.Map<UUID>(survivorId);
            var buffer = new List<AssistanceRequest>();
            await CallHandler(async () =>
            {
                using var streamingCall =
                    _client.GetAllBySurvivor(requestSurvivorId, new CallOptions(cancellationToken: cancellationToken));

                while (await streamingCall.ResponseStream.MoveNext())
                {
                    var chunk = streamingCall.ResponseStream.Current;
                    buffer.Add(_mapper.Map<AssistanceRequest>(chunk));
                }
            });
            foreach (var item in buffer)
            {
                yield return item;
            }
        }

        public Task<AssistanceRequest?> GetSingleAsync(Guid id, CancellationToken cancellationToken) =>
            CallHandler(async () =>
            {
                var uuid = _mapper.Map<UUID>(id);
                var response = await _client.GetSingleAsync(uuid, new CallOptions(cancellationToken: cancellationToken));
                return _mapper.Map<AssistanceRequest?>(response);
            }, default);
    }
}

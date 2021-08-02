using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.Commands;
using SafeHouseAMS.Transport.Protos.Models.Common;
using SafeHouseAMS.Transport.Protos.Services;

namespace SafeHouseAMS.WasmApp.Services
{
    internal class LifeSituationDocumentsClient : ILifeSituationDocumentsAggregate
    {
        private readonly LifeSituationDocumentsCatalogue.LifeSituationDocumentsCatalogueClient _client;
        private readonly IMapper _mapper;
        private readonly ILogger<LifeSituationDocumentsClient> _logger;

        public LifeSituationDocumentsClient(IMapper mapper, ILogger<LifeSituationDocumentsClient> logger, GrpcChannel channel)
        {
            _client = new (channel);
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<LifeSituationDocument> GetSingleAsync(Guid id, CancellationToken cancellationToken)
        {
            var uuid = _mapper.Map<UUID>(id);
            var response = await _client.GetSingleAsync(uuid, new CallOptions(cancellationToken: cancellationToken));
            return _mapper.Map<LifeSituationDocument>(response);
        }
        
        public async Task ApplyCommand(LifeSituationDocumentCommand command, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<Transport.Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand>(command);
            await _client.ApplyCommandAsync(request, new CallOptions(cancellationToken: cancellationToken));
        }
        
        public async IAsyncEnumerable<LifeSituationDocument> GetAllBySurvivor(Guid survivorId, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            using var call = _client.GetAllBySurvivor(_mapper.Map<UUID>(survivorId), new CallOptions(cancellationToken: cancellationToken));
            while (await call.ResponseStream.MoveNext(cancellationToken))
            {
                yield return _mapper.Map<LifeSituationDocument>(call.ResponseStream.Current);
            }
        }
        
        public async IAsyncEnumerable<string> GetCitizenshipsCompletions([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var collection = await _client.GetCitizenshipsCompletionsAsync(new(), new CallOptions(cancellationToken: cancellationToken));
            foreach (var completion in collection.Item)
            {
                yield return completion;
            }
        }
    }
}
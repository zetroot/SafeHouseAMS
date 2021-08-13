using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using SafeHouseAMS.BizLayer.Survivors;
using SafeHouseAMS.BizLayer.Survivors.Commands;
using SafeHouseAMS.Transport.Protos.Models.Common;
using SafeHouseAMS.Transport.Protos.Services;

namespace SafeHouseAMS.WasmApp.Services
{
    internal class SurvivorCatalogueClient : ISurvivorCatalogue
    {
        private readonly SurvivorCatalogue.SurvivorCatalogueClient _client;
        private readonly IMapper _mapper;
        [SuppressMessage("ReSharper", "NotAccessedField.Local")]
        private readonly ILogger<SurvivorCatalogueClient> _logger;

        public SurvivorCatalogueClient(IMapper mapper, ILogger<SurvivorCatalogueClient> logger, GrpcChannel channel)
        {
            _mapper = mapper;
            _logger = logger;
            _client = new SurvivorCatalogue.SurvivorCatalogueClient(channel);
        }

        public async Task<Survivor> GetSingleAsync(Guid id, CancellationToken cancellationToken)
        {
            var uuid = _mapper.Map<UUID>(id);
            var response = await _client.GetSingleAsync(uuid, new CallOptions(cancellationToken: cancellationToken));
            return _mapper.Map<Survivor>(response);
        }

        public async Task ApplyCommand(SurvivorCommand command, CancellationToken cancellationToken)
        {
            var request = command switch
            {
                CreateSurvivor create => new CommandWrapper {CreateCommand = _mapper.Map<Transport.Protos.Models.Survivors.CreateSurvivor>(create)},
                _ => throw new InvalidOperationException()
            };
            await _client.ApplyCommandAsync(request, new CallOptions(cancellationToken: cancellationToken));
        }

        public async IAsyncEnumerable<Survivor> GetCollection(int skip, int? take, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            using var streamingCall = _client.GetCollection(new() {Skip = skip, Take = take}, new CallOptions(cancellationToken: cancellationToken));
            while (await streamingCall.ResponseStream.MoveNext())
            {
                var chunk = streamingCall.ResponseStream.Current;
                yield return _mapper.Map<Survivor>(chunk);
            }
        }

        public async Task<int> GetTotalCount()
        {
            var result = await _client.GetTotalCountAsync(new());
            return result.Count;
        }
    }
}

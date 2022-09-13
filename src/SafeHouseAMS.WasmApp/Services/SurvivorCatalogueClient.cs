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
    internal class SurvivorCatalogueClient : GrpcClientBase, ISurvivorCatalogue
    {
        private readonly SurvivorCatalogue.SurvivorCatalogueClient _client;
        private readonly IMapper _mapper;

        public SurvivorCatalogueClient(IMapper mapper, ILogger<SurvivorCatalogueClient> logger, GrpcChannel channel) :
            base(logger)
        {
            _mapper = mapper;
            _client = new SurvivorCatalogue.SurvivorCatalogueClient(channel);
        }

        public Task<Survivor?> GetSingleAsync(Guid id, CancellationToken cancellationToken) =>
            CallHandler(async () =>
            {
                var uuid = _mapper.Map<UUID>(id);
                var response = await _client.GetSingleAsync(uuid, new CallOptions(cancellationToken: cancellationToken));
                return _mapper.Map<Survivor?>(response);
            }, default);

        public Task ApplyCommand(SurvivorCommand command, CancellationToken cancellationToken) =>
            CallHandler(async () =>
            {
                var request = command switch
                {
                    CreateSurvivor create => new CommandWrapper { CreateCommand = _mapper.Map<Transport.Protos.Models.Survivors.CreateSurvivor>(create) },
                    DeleteSurvivor delete => new CommandWrapper { DeleteCommand = _mapper.Map<Transport.Protos.Models.Survivors.DeleteSurvivor>(delete) },
                    _ => throw new InvalidOperationException()
                };
                await _client.ApplyCommandAsync(request, new CallOptions(cancellationToken: cancellationToken));
            });

        public async IAsyncEnumerable<Survivor> GetCollection(int skip, int? take, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var buffer = new List<Survivor>();
            await CallHandler(async () =>
            {
                using var streamingCall = _client.GetCollection(new() { Skip = skip, Take = take },
                    new CallOptions(cancellationToken: cancellationToken));
                while (await streamingCall.ResponseStream.MoveNext())
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var chunk = streamingCall.ResponseStream.Current;
                    buffer.Add(_mapper.Map<Survivor>(chunk));
                }
            });

            foreach (var item in buffer)
            {
                yield return item;
            }
        }

        public Task<int> GetTotalCount() =>
            CallHandler(async () =>
            {
                var result = await _client.GetTotalCountAsync(new());
                return result.Count;
            }, default);
    }
}

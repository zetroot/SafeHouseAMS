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
using SafeHouseAMS.BizLayer.ExploitationEpisodes;
using SafeHouseAMS.BizLayer.ExploitationEpisodes.Commands;
using SafeHouseAMS.Transport.Protos.Models.Common;
using SafeHouseAMS.Transport.Protos.Services;

namespace SafeHouseAMS.WasmApp.Services
{
    internal class EpisodesCatalogueClient : IEpisodesCatalogue
    {
        private readonly EpisodesCatalogue.EpisodesCatalogueClient _client;
        private readonly IMapper _mapper;
        [SuppressMessage("ReSharper", "NotAccessedField.Local")]
        private readonly ILogger<EpisodesCatalogueClient> _logger;

        public EpisodesCatalogueClient(GrpcChannel channel, IMapper mapper, ILogger<EpisodesCatalogueClient> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _client = new EpisodesCatalogue.EpisodesCatalogueClient(channel);
        }

        public async Task<Episode?> GetSingleAsync(Guid id, CancellationToken cancellationToken)
        {
            var uuid = _mapper.Map<UUID>(id);
            var response = await _client.GetSingleAsync(uuid, new CallOptions(cancellationToken: cancellationToken));
            return _mapper.Map<Episode?>(response);
        }

        public async Task ApplyCommand(EpisodeCommand command, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<Transport.Protos.Models.ExploitationEpisodes.Commands.EpisodeCommand>(command);
            await _client.ApplyCommandAsync(request, new CallOptions(cancellationToken: cancellationToken));
        }

        public async IAsyncEnumerable<Episode> GetAllBySurvivor(Guid survivorId,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var requestSurvivorId = _mapper.Map<UUID>(survivorId);
            using var streamingCall =
                _client.GetAllBySurvivor(requestSurvivorId, new CallOptions(cancellationToken: cancellationToken));

            while (await streamingCall.ResponseStream.MoveNext())
            {
                var chunk = streamingCall.ResponseStream.Current;
                yield return _mapper.Map<Episode>(chunk);
            }
        }
    }
}

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using SafeHouseAMS.BizLayer.ExploitationEpisodes;
using SafeHouseAMS.Transport.Protos.Models.Common;
using SafeHouseAMS.Transport.Protos.Models.ExploitationEpisodes.Commands;
using SafeHouseAMS.Transport.Protos.Services;
using Episode=SafeHouseAMS.Transport.Protos.Models.ExploitationEpisodes.Episode;

namespace SafeHouseAMS.Backend.Server.Services
{
    [ExcludeFromCodeCoverage, Authorize]
    internal class EpisodesCatalogueService : EpisodesCatalogue.EpisodesCatalogueBase
    {
        private readonly ILogger<EpisodesCatalogueService> _logger;
        private readonly IEpisodesCatalogue _catalogue;
        private readonly IMapper _mapper;

        public EpisodesCatalogueService(ILogger<EpisodesCatalogueService> logger, IEpisodesCatalogue catalogue,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _catalogue = catalogue ?? throw new ArgumentNullException(nameof(catalogue));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<Episode> GetSingle(UUID request, ServerCallContext context)
        {
            var id = _mapper.Map<Guid>(request);
            var result = await _catalogue.GetSingleAsync(id, context.CancellationToken);
            if(result is null)
                _logger.LogWarning("Not found episode by id = {0}, returning null", id);
            return _mapper.Map<Episode>(result);
        }

        public override async Task<Empty> ApplyCommand(EpisodeCommand request, ServerCallContext context)
        {
            var command = _mapper.Map<BizLayer.ExploitationEpisodes.Commands.EpisodeCommand>(request);
            await _catalogue.ApplyCommand(command, context.CancellationToken);
            return new();
        }

        public override async Task GetAllBySurvivor(UUID request, IServerStreamWriter<Episode> responseStream, ServerCallContext context)
        {
            var survivorId = _mapper.Map<Guid>(request);
            var asyncResult = _catalogue.GetAllBySurvivor(survivorId, context.CancellationToken);

            await foreach (var episode in asyncResult)
                await responseStream.WriteAsync(_mapper.Map<Episode>(episode));
        }
    }
}

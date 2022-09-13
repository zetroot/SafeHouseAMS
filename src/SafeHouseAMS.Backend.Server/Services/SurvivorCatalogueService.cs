using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using SafeHouseAMS.BizLayer.Survivors;
using SafeHouseAMS.BizLayer.Survivors.Commands;
using SafeHouseAMS.Transport.Protos.Models.Common;
using SafeHouseAMS.Transport.Protos.Services;
using Survivor=SafeHouseAMS.Transport.Protos.Models.Survivors.Survivor;

namespace SafeHouseAMS.Backend.Server.Services
{
    [ExcludeFromCodeCoverage] [Authorize]
    internal class SurvivorCatalogueService : SurvivorCatalogue.SurvivorCatalogueBase
    {
        private readonly ILogger<SurvivorCatalogueService> _logger;
        private readonly ISurvivorCatalogue _catalogue;
        private readonly IMapper _mapper;
        

        public SurvivorCatalogueService(ILogger<SurvivorCatalogueService> logger, ISurvivorCatalogue catalogue, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _catalogue = catalogue ?? throw new ArgumentNullException(nameof(catalogue));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        /// <inheritdoc />
        public override async Task<Survivor> GetSingle(UUID request, ServerCallContext context)
        {
            var id = _mapper.Map<Guid>(request);
            var result = await _catalogue.GetSingleAsync(id, context.CancellationToken);
            return _mapper.Map<Survivor>(result);
        }
        /// <inheritdoc />
        public override async Task<Empty> ApplyCommand(CommandWrapper request, ServerCallContext context)
        {
            switch (request.CommandCase)
            {
                case CommandWrapper.CommandOneofCase.CreateCommand:
                    await _catalogue.ApplyCommand(_mapper.Map<CreateSurvivor>(request.CreateCommand), context.CancellationToken);
                    break;
                case CommandWrapper.CommandOneofCase.DeleteCommand:
                    await _catalogue.ApplyCommand(_mapper.Map<DeleteSurvivor>(request.CreateCommand), context.CancellationToken);
                    break;
                default:
                    _logger.LogError("Получен запрос с неизвестным типом команды");
                    throw new InvalidOperationException("Получен запрос с неизвестным типом команды");
            }
            return new();
        }

        public override async Task GetCollection(CollectionRequest request, IServerStreamWriter<Survivor> responseStream, ServerCallContext context)
        {
            var asyncResult = _catalogue.GetCollection(request.Skip, request.Take, context.CancellationToken);
            await foreach (var survivor in asyncResult)
                await responseStream.WriteAsync(_mapper.Map<Survivor>(survivor));
        }

        /// <inheritdoc />
        public override async Task<TotalCountResponse> GetTotalCount(Empty request, ServerCallContext context)
        {
            var cnt = await _catalogue.GetTotalCount();
            return new() {Count = cnt};
        }
    }
}

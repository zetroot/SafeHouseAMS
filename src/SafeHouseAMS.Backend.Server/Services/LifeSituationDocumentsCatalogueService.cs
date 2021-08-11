using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.Transport.Protos.Models.Common;
using SafeHouseAMS.Transport.Protos.Models.LifeSituations.Commands;
using SafeHouseAMS.Transport.Protos.Services;
using LifeSituationDocument=SafeHouseAMS.Transport.Protos.Models.LifeSituations.LifeSituationDocument;

namespace SafeHouseAMS.Backend.Server.Services
{
    [ExcludeFromCodeCoverage, Authorize]
    internal class LifeSituationDocumentsCatalogueService : LifeSituationDocumentsCatalogue.LifeSituationDocumentsCatalogueBase
    {
        [SuppressMessage("ReSharper", "NotAccessedField.Local")]
        private readonly ILogger<LifeSituationDocumentsCatalogueService> _logger;
        private readonly ILifeSituationDocumentsAggregate _catalogue;
        private readonly IMapper _mapper;

        public LifeSituationDocumentsCatalogueService(ILogger<LifeSituationDocumentsCatalogueService> logger, ILifeSituationDocumentsAggregate catalogue, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _catalogue = catalogue ?? throw new ArgumentNullException(nameof(catalogue));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<LifeSituationDocument> GetSingle(UUID request, ServerCallContext context)
        {
            var id = _mapper.Map<Guid>(request);
            var doc = await _catalogue.GetSingleAsync(id, context.CancellationToken);
            return _mapper.Map<LifeSituationDocument>(doc);
        }

        public override async Task<Empty> ApplyCommand(LifeSituationDocumentCommand request, ServerCallContext context)
        {
            var cmd = _mapper.Map<BizLayer.LifeSituations.Commands.LifeSituationDocumentCommand>(request);
            await _catalogue.ApplyCommand(cmd, context.CancellationToken);
            return new();
        }

        public override async Task GetAllBySurvivor(UUID request, IServerStreamWriter<LifeSituationDocument> responseStream, ServerCallContext context)
        {
            var id = _mapper.Map<Guid>(request);
            await foreach (var doc in _catalogue.GetAllBySurvivor(id, context.CancellationToken))
            {
                var docDto = _mapper.Map<LifeSituationDocument>(doc);
                await responseStream.WriteAsync(docDto);
            }
        }

        public override async Task<CitizenshipCompletionCollection> GetCitizenshipsCompletions(Empty request, ServerCallContext context)
        {
            var result = new CitizenshipCompletionCollection();
            await foreach (var completion in _catalogue.GetCitizenshipsCompletions(context.CancellationToken))
            {
                result.Item.Add(completion);
            }
            return result;
        }
    }
}

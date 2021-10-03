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
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.Commands;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.Transport.Protos.Models.Common;
using SafeHouseAMS.Transport.Protos.Services;

namespace SafeHouseAMS.WasmApp.Services
{
    internal class LifeSituationDocumentsClient : ILifeSituationDocumentsAggregate
    {
        private readonly LifeSituationDocumentsCatalogue.LifeSituationDocumentsCatalogueClient _client;
        private readonly IMapper _mapper;
        [SuppressMessage("ReSharper", "NotAccessedField.Local")]
        private readonly ILogger<LifeSituationDocumentsClient> _logger;

        public LifeSituationDocumentsClient(IMapper mapper, ILogger<LifeSituationDocumentsClient> logger, GrpcChannel channel)
        {
            _client = new (channel);
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<LifeSituationDocument?> GetSingleAsync(Guid id, CancellationToken cancellationToken)
        {
            var uuid = _mapper.Map<UUID>(id);
            var response = await _client.GetSingleAsync(uuid, new CallOptions(cancellationToken: cancellationToken));
            return _mapper.Map<LifeSituationDocument?>(response);
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
        public async Task<SurvivorStateReport> GetSurvivorReport(Guid survivorId, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<UUID>(survivorId);
            var result =
                await _client.GetSurvivorStateReportAsync(request,
                new CallOptions(cancellationToken: cancellationToken));
            return _mapper.Map<SurvivorStateReport>(result);
        }

        public async IAsyncEnumerable<RecordHistoryItem> GetRecordHistory<T>(Guid survivorId,
            [EnumeratorCancellation] CancellationToken cancellationToken) where T : BaseRecord
        {
            var request = new RecordHistoryRequest
            {
                SurvivorID = _mapper.Map<UUID>(survivorId)
            };
            if (typeof(T) == typeof(ChildrenRecord)) request.RecordType = RecordTypeEnum.Children;
            else if (typeof(T) == typeof(CitizenshipRecord)) request.RecordType = RecordTypeEnum.Citizenship;
            else if (typeof(T) == typeof(DomicileRecord)) request.RecordType = RecordTypeEnum.Domicile;
            else if (typeof(T) == typeof(EducationLevelRecord)) request.RecordType = RecordTypeEnum.Education;
            else if (typeof(T) == typeof(MigrationStatusRecord)) request.RecordType = RecordTypeEnum.Migration;
            else if (typeof(T) == typeof(RegistrationStatusRecord)) request.RecordType = RecordTypeEnum.Registration;
            else if (typeof(T) == typeof(SpecialityRecord)) request.RecordType = RecordTypeEnum.Speciality;

            using var call = _client.GetRecordHistory(request, new CallOptions(cancellationToken: cancellationToken));

            while (await call.ResponseStream.MoveNext(cancellationToken))
            {
                yield return _mapper.Map<RecordHistoryItem>(call.ResponseStream.Current);
            }
        }
    }
}

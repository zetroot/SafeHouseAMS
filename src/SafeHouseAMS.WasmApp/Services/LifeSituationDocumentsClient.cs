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
    internal class LifeSituationDocumentsClient : GrpcClientBase, ILifeSituationDocumentsAggregate
    {
        private readonly LifeSituationDocumentsCatalogue.LifeSituationDocumentsCatalogueClient _client;
        private readonly IMapper _mapper;

        public LifeSituationDocumentsClient(IMapper mapper, ILogger<LifeSituationDocumentsClient> logger, GrpcChannel channel) : base(logger)
        {
            _client = new (channel);
            _mapper = mapper;
        }

        public Task<LifeSituationDocument?> GetSingleAsync(Guid id, CancellationToken cancellationToken) =>
            CallHandler(async () =>
            {
                var uuid = _mapper.Map<UUID>(id);
                var response = await _client.GetSingleAsync(uuid, new CallOptions(cancellationToken: cancellationToken));
                return _mapper.Map<LifeSituationDocument?>(response);
            },default);

        public Task ApplyCommand(LifeSituationDocumentCommand command, CancellationToken cancellationToken) =>
            CallHandler(async () =>
            {
                var request =
                    _mapper.Map<Transport.Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand>(command);
                await _client.ApplyCommandAsync(request, new CallOptions(cancellationToken: cancellationToken));
            });

        public async IAsyncEnumerable<LifeSituationDocument> GetAllBySurvivor(Guid survivorId, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var buffer = new List<LifeSituationDocument>();
            await CallHandler(async () =>
            {
                using var call = _client.GetAllBySurvivor(_mapper.Map<UUID>(survivorId),
                new CallOptions(cancellationToken: cancellationToken));
                while (await call.ResponseStream.MoveNext(cancellationToken))
                {
                    buffer.Add(_mapper.Map<LifeSituationDocument>(call.ResponseStream.Current));
                }
            });
            foreach (var item in buffer)
            {
                yield return item;
            }
        }

        public async IAsyncEnumerable<string> GetCitizenshipsCompletions([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var buffer = new List<string>();
            await CallHandler(async () =>
            {
                var collection = await _client.GetCitizenshipsCompletionsAsync(new(),
                new CallOptions(cancellationToken: cancellationToken));
                foreach (var completion in collection.Item)
                {
                    buffer.Add(completion);
                }
            });

            foreach (var item in buffer)
            {
                yield return item;
            }
        }
        public Task<SurvivorStateReport> GetSurvivorReport(Guid survivorId, CancellationToken cancellationToken) =>
            CallHandler(async () =>
            {
                var request = _mapper.Map<UUID>(survivorId);
                var result =
                    await _client.GetSurvivorStateReportAsync(request,
                    new CallOptions(cancellationToken: cancellationToken));
                return _mapper.Map<SurvivorStateReport>(result);
            }, new(default, default,default,default,default,default,default,Array.Empty<EducationLevelRecord>(), default,default,default,default,default,Array.Empty<SpecialityRecord>(), default));

        public async IAsyncEnumerable<RecordHistoryItem> GetRecordHistory<T>(Guid survivorId,
            [EnumeratorCancellation] CancellationToken cancellationToken) where T : BaseRecord
        {
            var buffer = new List<RecordHistoryItem>();
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

            await CallHandler(async () =>
            {
                using var call =
                    _client.GetRecordHistory(request, new CallOptions(cancellationToken: cancellationToken));

                while (await call.ResponseStream.MoveNext(cancellationToken))
                {
                    buffer.Add(_mapper.Map<RecordHistoryItem>(call.ResponseStream.Current));
                }
            });

            foreach (var item in buffer)
            {
                yield return item;
            }
        }
    }
}

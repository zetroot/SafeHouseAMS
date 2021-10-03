using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.LifeSituations.Commands;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

namespace SafeHouseAMS.BizLayer.LifeSituations
{
    internal class LifeSituationDocumentsAggregate : ILifeSituationDocumentsAggregate
    {
        private readonly ILifeSituationDocumentsRepository _repository;

        public LifeSituationDocumentsAggregate(ILifeSituationDocumentsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<LifeSituationDocument?> GetSingleAsync(Guid id, CancellationToken cancellationToken) =>
            _repository.GetSingleAsync(id, cancellationToken);

        public async Task ApplyCommand(LifeSituationDocumentCommand command, CancellationToken cancellationToken)
        {
            if (command is null) throw new ArgumentNullException(nameof(command));
            await command.ApplyOn(_repository);
        }

        public IAsyncEnumerable<LifeSituationDocument> GetAllBySurvivor(Guid survivorId,
            CancellationToken cancellationToken) =>
            _repository.GetAllBySurvivor(survivorId, cancellationToken);

        public IAsyncEnumerable<string> GetCitizenshipsCompletions(CancellationToken cancellationToken) =>
            _repository.GetCitizenshipsCompletions(cancellationToken);

        public Task<SurvivorStateReport> GetSurvivorReport(Guid survivorId, CancellationToken cancellationToken) =>
            _repository.GetSurvivorReport(survivorId, cancellationToken);

        public IAsyncEnumerable<RecordHistoryItem> GetRecordHistory<T>(Guid survivorId,
            CancellationToken cancellationToken) where T : BaseRecord =>
            _repository.GetRecordHistory<T>(survivorId, cancellationToken);
    }
}

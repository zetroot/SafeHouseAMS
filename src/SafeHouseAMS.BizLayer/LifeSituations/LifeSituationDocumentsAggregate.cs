using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.LifeSituations.Commands;

namespace SafeHouseAMS.BizLayer.LifeSituations
{
    internal class LifeSituationDocumentsAggregate : ILifeSituationDocumentsAggregate
    {
        private readonly ILifeSituationDocumentsRepository _repository;

        public LifeSituationDocumentsAggregate(ILifeSituationDocumentsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<LifeSituationDocument> GetSingleAsync(Guid id, CancellationToken cancellationToken) => 
            _repository.GetSingleAsync(id, cancellationToken);
        
        public async Task ApplyCommand(LifeSituationDocumentCommand command, CancellationToken cancellationToken)
        {
            if(command is null) throw new ArgumentNullException(nameof(command));
            await command.ApplyOn(_repository);
        }

        public IAsyncEnumerable<LifeSituationDocument> GetAllBySurvivor(Guid survivorId, CancellationToken cancellationToken) =>
            _repository.GetAllBySurvivor(survivorId, cancellationToken);
    }
}
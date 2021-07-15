using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.Survivors.Commands;

namespace SafeHouseAMS.BizLayer.Survivors
{
    internal class SurvivorCatalogue : ISurvivorCatalogue
    {
        private readonly ISurvivorRepository _repository;

        public SurvivorCatalogue(ISurvivorRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public Task ApplyCommand(SurvivorCommand command, CancellationToken cancellationToken)
        {
            if (command is null) throw new ArgumentNullException(nameof(command));
            cancellationToken.ThrowIfCancellationRequested();
            return command.ApplyOn(_repository);
        }
        
        public Task<Survivor> GetSingleAsync(Guid id, CancellationToken cancellationToken) =>
            _repository.GetSingleAsync(id, cancellationToken);

        public IAsyncEnumerable<Survivor> GetCollection() => _repository.GetCollection();
    }
}
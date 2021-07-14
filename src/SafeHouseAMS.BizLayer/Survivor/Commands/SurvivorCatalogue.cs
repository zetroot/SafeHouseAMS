using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SafeHouseAMS.BizLayer.Survivor.Commands
{
    internal class SurvivorCatalogue : ISurvivorCatalogue
    {
        private readonly ISurvivorRepository _repository;

        public SurvivorCatalogue(ISurvivorRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<Survivor> GetSingleAsync(Guid id, CancellationToken cancellationToken) =>
            _repository.GetSingleAsync(id, cancellationToken);

        public Task ApplyCommand(SurvivorCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Survivor> GetCollection()
        {
            throw new NotImplementedException();
        }
    }
}
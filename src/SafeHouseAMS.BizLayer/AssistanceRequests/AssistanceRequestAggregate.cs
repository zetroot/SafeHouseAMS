using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.AssistanceRequests.Commands;

namespace SafeHouseAMS.BizLayer.AssistanceRequests
{
    /// <summary>
    /// реализация агрегата запросов помощи
    /// </summary>
    public class AssistanceRequestAggregate : IAssistanceRequestAggregate
    {
        private readonly IAssistanceRequestsRepository _repository;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="repository">репозиторий</param>
        public AssistanceRequestAggregate(IAssistanceRequestsRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public Task<AssistanceRequest?> GetSingleAsync(Guid id, CancellationToken cancellationToken) =>
            _repository.GetSingle(id);


        /// <inheritdoc />
        public IAsyncEnumerable<AssistanceRequest> GetAllBySurvivor(Guid survivorId, CancellationToken cancellationToken) =>
            _repository.GetAllBySurvivor(survivorId, cancellationToken);

        /// <inheritdoc />
        public Task ApplyCommand(AssistanceRequestCommand command, CancellationToken cancellationToken)
        {
            return command.ApplyOn(_repository);
        }
    }
}

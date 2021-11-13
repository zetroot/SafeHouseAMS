using System;
using System.Collections.Generic;
using System.Threading;
using SafeHouseAMS.BizLayer.AssistanceRequests.Commands;

namespace SafeHouseAMS.BizLayer.AssistanceRequests
{
    /// <summary>
    /// Интерфейс агрегата запросов помощи
    /// </summary>
    public interface IAssistanceRequestAggregate : IDomainAggregate<AssistanceRequest, AssistanceRequestCommand>
    {

        /// <summary>
        /// получить все запросы по пострадавшему
        /// </summary>
        /// <param name="survivorId">идентификатор пострадавшего</param>
        /// <param name="cancellationToken">токен отмены</param>
        /// <returns></returns>
        IAsyncEnumerable<AssistanceRequest> GetAllBySurvivor(Guid survivorId, CancellationToken cancellationToken);
    }
}

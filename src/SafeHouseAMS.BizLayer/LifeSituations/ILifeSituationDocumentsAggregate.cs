using System;
using System.Collections.Generic;
using System.Threading;
using SafeHouseAMS.BizLayer.LifeSituations.Commands;

namespace SafeHouseAMS.BizLayer.LifeSituations
{
    /// <summary>
    /// Агрегат документов жизненных ситуаций 
    /// </summary>
    public interface ILifeSituationDocumentsAggregate : IDomainAggregate<LifeSituationDocument, LifeSituationDocumentCommand>
    {
        /// <summary>
        /// Получить всю коллекцию документов по пострадавшему
        /// </summary>
        /// <param name="survivorId">Идентификатор пострадавшего</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns>Асинхронный поток документов, относящихся к пострадавшему</returns>
        IAsyncEnumerable<LifeSituationDocument> GetAllBySurvivor(Guid survivorId, CancellationToken cancellationToken);
    }

}
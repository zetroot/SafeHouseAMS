using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

        /// <summary>
        /// Получить список гражданств введённых для автозаполнения
        /// </summary>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns>Асинхронная последовательность строк - что вводили в поле "гражданство"</returns>
        IAsyncEnumerable<string> GetCitizenshipsCompletions(CancellationToken cancellationToken);

        /// <summary>
        /// Получить сводку-отчёт о пострадавшем
        /// </summary>
        /// <param name="survivroId">идентификатор пострадавшего</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Отчёт о текущем состоянии ситуации у пострадавшего</returns>
        Task<SurvivorStateReport> GetSurvivorReport(Guid survivroId, CancellationToken cancellationToken);
    }

}

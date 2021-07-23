using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.LifeSituations.InquirySources;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

namespace SafeHouseAMS.BizLayer.LifeSituations
{
    /// <summary>
    /// Репозиторий документов жизненных ситуаций
    /// </summary>
    public interface ILifeSituationDocumentsRepository
    {
        /// <summary>
        /// Получить одиночный документ по идентификатору
        /// </summary>
        /// <param name="id">идентификатор документа</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns>Полученный документ по идентификатору</returns>
        Task<LifeSituationDocument> GetSingleAsync(Guid id, CancellationToken cancellationToken);
        
        /// <summary>
        /// Получить все документы по пострадавшему
        /// </summary>
        /// <param name="survivorId">идентификатор пострадавшего</param>
        /// <param name="cancellationToken">токен отмены</param>
        /// <returns>асинхронная последовательность документов</returns>
        IAsyncEnumerable<LifeSituationDocument> GetAllBySurvivor(Guid survivorId, CancellationToken cancellationToken);
        
        /// <summary>
        /// СОздать документ обращения
        /// </summary>
        /// <param name="documentId">идентификатор документа</param>
        /// <param name="isDeleted">признак удаленной записи</param>
        /// <param name="created">создан</param>
        /// <param name="lastEdit">последнее редактирование</param>
        /// <param name="survivorID">идентификатор пострадавшего</param>
        /// <param name="documentDate">дата документа</param>
        /// <param name="isJuvenile">несовершеннолетний в момент обращения</param>
        /// <param name="inquirySources">источники обращения</param>
        Task CreateInquiry(Guid documentId, bool isDeleted, DateTime created, DateTime lastEdit,
            Guid survivorID, DateTime documentDate, bool isJuvenile, IEnumerable<IInquirySource> inquirySources);

        /// <summary>
        /// Добавить новую запись о жизненной ситуации
        /// </summary>
        /// <param name="documentId">Идентификатор документа, создавшего запись</param>
        /// <param name="record">Собственно объект записи</param>
        Task AddRecord(Guid documentId, BaseRecord record);
        
        /// <summary>
        /// Получить список гражданств введённых для автозаполнения
        /// </summary>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns>Асинхронная последовательность строк - что вводили в поле "гражданство"</returns>
        IAsyncEnumerable<string> GetCitizenshipsCompletions(CancellationToken cancellationToken);
    }
}
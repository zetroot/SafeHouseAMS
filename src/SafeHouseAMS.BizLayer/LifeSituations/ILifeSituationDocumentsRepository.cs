using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.LifeSituations.InquirySources;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.BizLayer.LifeSituations.Vulnerabilities;

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

        /// <summary>
        /// Заполнить "опыт работы"
        /// </summary>
        /// <param name="inquiryId">идентификатор документа</param>
        /// <param name="workingExperience">строка-описание опыта работы</param>
        Task SetWorkingExperience(Guid inquiryId, string workingExperience);

        /// <summary>
        /// Установить зависимость
        /// </summary>
        /// <param name="inquiryId">идентификатор обращения</param>
        /// <param name="addictionKind">тип зависимости</param>
        Task SetAddiction(Guid inquiryId, string addictionKind);
        
        /// <summary>
        /// Убрать зависимость
        /// </summary>
        /// <param name="inquiryId">идентификтаор обращения</param>
        Task ClearAddiction(Guid inquiryId);
        
        /// <summary>
        /// Установить признак бездомность
        /// </summary>
        /// <param name="inquiryId">идентификатор обращения</param>
        Task SetHomeless(Guid inquiryId);
        
        /// <summary>
        /// Снять признак бездомности 
        /// </summary>
        /// <param name="inquiryId">идентификатор обращения</param>
        Task ClearHomeless(Guid inquiryId);
        
        /// <summary>
        /// Установить признак мигрант_ка
        /// </summary>
        /// <param name="inquiryId">идентификатор обращения</param>
        Task SetMigration(Guid inquiryId);
        
        /// <summary>
        /// Снять признак мигрант_ка
        /// </summary>
        /// <param name="inquiryId">идентификатор обращения</param>
        /// <returns></returns>
        Task ClearMigration(Guid inquiryId);
        
        /// <summary>
        /// Установить признак "насилие в детстве"
        /// </summary>
        /// <param name="inquiryId">идентификатор обращения</param>
        Task SetChildhoodViolence(Guid inquiryId);
        
        /// <summary>
        /// Снять признак "насилие в детстве"
        /// </summary>
        /// <param name="inquiryId">идентификатор обращения</param>
        Task ClearChildhoodViolence(Guid inquiryId);
        
        /// <summary>
        /// Установить признак "опыт интернатного учреждения
        /// </summary>
        /// <param name="inquiryId">идентификатор обращения</param>
        Task SetOrphanageExperience(Guid inquiryId);
        
        /// <summary>
        /// Снять признак "опыт интернатного учреждения"
        /// </summary>
        /// <param name="inquiryId">идентификатор обращения</param>
        Task ClearOrphanageExperience(Guid inquiryId);
        
        /// <summary>
        /// Установить признак "другие факторы уязвимости"
        /// </summary>
        /// <param name="inquiryId">идентификатор обращения</param>
        /// <param name="details">описание других факторов</param>
        Task SetOther(Guid inquiryId, string details);
        
        /// <summary>
        /// Снять признак "другие факторы уязвимости"
        /// </summary>
        /// <param name="inquiryId">идентификатор обращения</param>
        Task ClearOther(Guid inquiryId);
        
        /// <summary>
        /// Установить признак "уявзимости связанные со здоровьем"
        /// </summary>
        /// <param name="inquiryId">идентификатор обращения</param>
        /// <param name="healthStatus">битовая маска факторов здоровья</param>
        /// <param name="details">уточнение</param>
        Task SetHealthStatusVulnerability(Guid inquiryId, HealthStatus.HealthStatusVulnerabilityType healthStatus, string? details);
        
        /// <summary>
        /// Снять признак "уязвимости связанные со здоровьем"
        /// </summary>
        /// <param name="inquiryId">идентификатор обращения</param>
        Task ClearHealthStatusVulnerability(Guid inquiryId);
    }
}
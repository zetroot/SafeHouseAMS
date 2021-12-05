using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace SafeHouseAMS.BizLayer.AssistanceRequests
{
    /// <summary>
    /// репозиторий запросов помощи
    /// </summary>
    public interface IAssistanceRequestsRepository
    {
        /// <summary>
        /// получить запись о запросе о помощи по ее идентификатору
        /// </summary>
        /// <param name="requestId">идентификатор запроса о помощи</param>
        /// <returns></returns>
        Task<AssistanceRequest?> GetSingle(Guid requestId);

        /// <summary>
        /// создать новый запрос о помощи
        /// </summary>
        /// <param name="id">идентификатор создаваемого запроса</param>
        /// <param name="isDeleted">признак удаленной записи</param>
        /// <param name="created">дата создания записи</param>
        /// <param name="lastEdit">дата последнего изменения записи</param>
        /// <param name="survivorId">идентификатор пострадавшего</param>
        /// <param name="assistanceKind">тип помощи</param>
        /// <param name="details">дополнительная информация о запросе</param>
        /// <param name="isAccomplished">выполнен ли запрос</param>
        /// <param name="documentDate"></param>
        /// <returns></returns>
        Task CreateAssistanceRequest(Guid id, bool isDeleted, DateTime created, DateTime lastEdit,
            Guid survivorId, AssistanceKind assistanceKind, string details, bool isAccomplished, DateTime documentDate);

        /// <summary>
        /// получить все записи о заопросах о помощи по пострадавшему
        /// </summary>
        /// <param name="survivorId">идентификатор пострадавшего</param>
        /// <param name="cancellationToken">токен отмены</param>
        /// <returns></returns>
        IAsyncEnumerable<AssistanceRequest> GetAllBySurvivor(Guid survivorId, CancellationToken cancellationToken);

        /// <summary>
        /// добавить новый акт оказания помощи
        /// </summary>
        /// <param name="id">идентификатор создаваемой записи</param>
        /// <param name="isDeleted">признак удалённой записи</param>
        /// <param name="created">дата создания</param>
        /// <param name="lastEdit">дата последнего редактирования</param>
        /// <param name="requestId">идентификатор запроса оказания помощи</param>
        /// <param name="details">дополнительная информация по оказанной помощи</param>
        /// <param name="workHours">потрачено часов</param>
        /// <param name="money">потрачено денег</param>
        /// <param name="documentDate"></param>
        /// <returns></returns>
        Task CreateAssistanceAct(Guid id, bool isDeleted, DateTime created, DateTime lastEdit,
            Guid requestId, string details, decimal workHours, decimal money, DateTime documentDate);
    }
}

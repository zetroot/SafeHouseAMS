using System;
using System.Threading.Tasks;

namespace SafeHouseAMS.BizLayer.AssistanceRequests.Commands
{
    /// <summary>
    /// Команда создания запроса помощи
    /// </summary>
    public class CreateAssistanceRequest : AssistanceRequestCommand
    {
        /// <summary>
        /// Идентификатор пострадавшего
        /// </summary>
        public Guid SurvivorID { get; }

        /// <summary>
        /// вид запрашиваемой помощи
        /// </summary>
        public AssistanceKind Kind { get; }

        /// <summary>
        /// детали запроса помощи
        /// </summary>
        public string Details { get; }

        /// <summary>
        /// Дата запроса помощи
        /// </summary>
        public DateTime DocumentDate { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="entityID">идентификатор запроса помощи</param>
        /// <param name="survivorID">идентификатор пострадавшего</param>
        /// <param name="kind">вид запрашиваемой помощи</param>
        /// <param name="details">дополнительная информация по запросу</param>
        /// <param name="documentDate">дата запроса помощи</param>
        public CreateAssistanceRequest(Guid entityID, Guid survivorID, AssistanceKind kind, string details, DateTime documentDate)
            : base(entityID)
        {
            SurvivorID = survivorID;
            Kind = kind;
            Details = details;
            DocumentDate = documentDate;
        }

        internal override Task ApplyOn(IAssistanceRequestsRepository repository) =>
            repository.CreateAssistanceRequest(EntityID, false, DateTime.Now, DateTime.Now, SurvivorID, Kind, Details, false, DocumentDate);
    }
}

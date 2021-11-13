using System;
using System.Threading.Tasks;

namespace SafeHouseAMS.BizLayer.AssistanceRequests.Commands
{
    /// <summary>
    /// Команда создания запроса помощи
    /// </summary>
    public class Create : AssistanceRequestCommand
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
        /// ctor
        /// </summary>
        /// <param name="entityID">идентификатор запроса помощи</param>
        /// <param name="survivorID">идентификатор пострадавшего</param>
        /// <param name="kind">вид запрашиваемой помощи</param>
        /// <param name="details">дополнительная информация по запросу</param>
        public Create(Guid entityID, Guid survivorID, AssistanceKind kind, string details) : base(entityID)
        {
            SurvivorID = survivorID;
            Kind = kind;
            Details = details;
        }

        internal override Task ApplyOn(IAssistanceRequestsRepository repository) =>
            repository.CreateAssistanceRequest(EntityID, false, DateTime.Now, DateTime.Now, SurvivorID, Kind, Details, false);
    }
}

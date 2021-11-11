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
        /// <param name="survivorID">идентификатор постардавшего</param>
        /// <param name="kind">вид запрошиваемой помощи</param>
        /// <param name="details">дополнительная информация по запросу</param>
        public Create(Guid entityID, Guid survivorID, AssistanceKind kind, string details) : base(entityID)
        {
            SurvivorID = survivorID;
            Kind = kind;
            Details = details;
        }

        internal override async Task ApplyOn(IAssistanceRequestsRepository repository)
        {
            throw new NotImplementedException();
        }
    }
}

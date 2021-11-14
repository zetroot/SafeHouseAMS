using System;
using System.Threading.Tasks;

namespace SafeHouseAMS.BizLayer.AssistanceRequests.Commands
{
    /// <summary>
    /// Добавить новую запись об оказании помощи к запросу
    /// </summary>
    public class AttachAssistanceAct : AssistanceRequestCommand
    {
        /// <summary>
        /// идентификатор акта помощи
        /// </summary>
        public Guid ActID { get; }

        /// <summary>
        /// Дополнительная информация по оказанной помощи
        /// </summary>
        public string Details { get; }

        /// <summary>
        /// количество потраченных часов
        /// </summary>
        public decimal WorkHours { get; }

        /// <summary>
        /// затраты по этому акту помощи
        /// </summary>
        public decimal Money { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="entityID">идентификатор запроса помощи к которому добавляется новый акт помощи</param>
        /// <param name="actID">идентификатор нового акта помощи</param>
        /// <param name="details">дополнительная информация об оказанной помощи</param>
        /// <param name="workHours">количество потраченных часов</param>
        /// <param name="money">потрачено денег</param>
        public AttachAssistanceAct(Guid entityID, Guid actID, string details, decimal workHours, decimal money) : base(entityID)
        {
            ActID = actID;
            Details = details;
            WorkHours = workHours;
            Money = money;
        }

        internal override Task ApplyOn(IAssistanceRequestsRepository repository) =>
            repository.CreateAssistanceAct(ActID, false, DateTime.Now, DateTime.Now, EntityID, Details, WorkHours, Money);
    }
}

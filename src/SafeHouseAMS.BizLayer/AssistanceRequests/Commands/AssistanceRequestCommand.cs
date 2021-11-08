using System;

namespace SafeHouseAMS.BizLayer.AssistanceRequests.Commands
{
    /// <summary>
    /// базовый класс команды для манипуляции с запросами помощи
    /// </summary>
    public abstract class AssistanceRequestCommand : BaseCommand<IAssistanceRequestsRepository>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="entityID">идентификатор запроса помощи с которым выполняется манипуляция</param>
        protected AssistanceRequestCommand(Guid entityID) : base(entityID)
        {
        }
    }
}

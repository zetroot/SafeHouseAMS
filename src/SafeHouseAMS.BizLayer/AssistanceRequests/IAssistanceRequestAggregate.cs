using SafeHouseAMS.BizLayer.AssistanceRequests.Commands;

namespace SafeHouseAMS.BizLayer.AssistanceRequests
{
    /// <summary>
    /// Интерфейс агрегата запросов помощи
    /// </summary>
    public interface IAssistanceRequestAggregate : IDomainAggregate<AssistanceRequest, AssistanceRequestCommand>
    {

    }
}

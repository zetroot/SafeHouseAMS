using System;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.BizLayer.AssistanceRequests
{
    /// <summary>
    /// Базовый класс запроса помощи
    /// </summary>
    /// <param name="ID">Идентификатор запроса</param>
    /// <param name="IsDeleted">Признак удалённой записи</param>
    /// <param name="Created">Дата создания</param>
    /// <param name="LastEdit">Дата последнего редактирования</param>
    /// <param name="Survivor">Пострадавший к которому относится запрос</param>
    /// <param name="IsAccomplished">Признак выполненного запроса</param>
    public abstract record AssistanceRequest(Guid ID, bool IsDeleted, DateTime Created, DateTime LastEdit,
        Survivor Survivor, bool IsAccomplished) : BaseDomainModel(ID, IsDeleted, Created, LastEdit);
}

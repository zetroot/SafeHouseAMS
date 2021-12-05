using System;
using System.Collections.Generic;
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
    /// <param name="AssistanceKind">вид запроса помощи</param>
    /// <param name="Details">Дополнительное описание запроса помощи</param>
    /// <param name="IsAccomplished">Признак выполненного запроса</param>
    /// <param name="DocumentDate">Дата документа - когда запрос начал действовать</param>
    /// <param name="AssistanceActs">Акты оказания помощи</param>
    public record AssistanceRequest(Guid ID, bool IsDeleted, DateTime Created, DateTime LastEdit,
        Survivor Survivor, AssistanceKind AssistanceKind, string Details, bool IsAccomplished, DateTime DocumentDate,
        IReadOnlyCollection<AssistanceAct> AssistanceActs) :
        BaseDomainModel(ID, IsDeleted, Created, LastEdit);
}

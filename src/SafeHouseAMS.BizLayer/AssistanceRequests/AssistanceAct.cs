using System;

namespace SafeHouseAMS.BizLayer.AssistanceRequests
{
    /// <summary>
    /// Акт оказания помощи
    /// </summary>
    /// <param name="ID">Идентификатор записи</param>
    /// <param name="IsDeleted">Признак удалённой записи</param>
    /// <param name="Created">Дата создания записи</param>
    /// <param name="LastEdit">Дата последнего изменения записи</param>
    /// <param name="Details">Дополнительная информация по выполненным работам</param>
    /// <param name="WorkHours">Затрачено часов</param>
    /// <param name="Money">Затрачено денег</param>
    public record AssistanceAct(Guid ID, bool IsDeleted, DateTime Created, DateTime LastEdit,
        string Details, decimal WorkHours, decimal Money) :
        BaseDomainModel(ID, IsDeleted, Created, LastEdit);
}

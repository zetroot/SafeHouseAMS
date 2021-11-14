using System;
using System.Collections.Generic;
using System.Linq;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.BizLayer.LifeSituations.Documents
{
    /// <summary>
    /// Документ обновляющий сразу коллекцию записей
    /// </summary>
    /// <typeparam name="T">тип записи</typeparam>
    /// <param name="ID">идентификатор записи</param>
    /// <param name="IsDeleted">признак удалённой записи</param>
    /// <param name="Created">дата создания записи</param>
    /// <param name="LastEdit">дата последнего изменения записи</param>
    /// <param name="DocumentDate">дата документа</param>
    /// <param name="Survivor">пострадавший, к которому относится этот документ</param>
    /// <param name="Records">собственно запись обновляемая в документе</param>
    public record MultiRecordsUpdate<T>(Guid ID, bool IsDeleted, DateTime Created, DateTime LastEdit,
        DateTime DocumentDate, Survivor Survivor, IReadOnlyCollection<T> Records) :
        LifeSituationDocument(ID, IsDeleted, Created, LastEdit, DocumentDate, Survivor);
}

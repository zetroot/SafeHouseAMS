using System;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.BizLayer.LifeSituations.Documents
{
    /// <summary>
    /// Документ обновления записи о пострадавшем
    /// </summary>
    /// <typeparam name="T">Тип обновляемой записи</typeparam>
    /// <param name="ID">идентификатор записи</param>
    /// <param name="IsDeleted">признак удалённой записи</param>
    /// <param name="Created">дата создания записи</param>
    /// <param name="LastEdit">дата последнего изменения записи</param>
    /// <param name="DocumentDate">дата документа</param>
    /// <param name="Survivor">пострадавший, к которому относится этот документ</param>
    /// <param name="Record">собственно запись обновляемая в документе</param>
    public record SingleRecordUpdate<T>(Guid ID, bool IsDeleted, DateTime Created, DateTime LastEdit,
        DateTime DocumentDate, Survivor Survivor, T? Record) :
        LifeSituationDocument(ID, IsDeleted, Created, LastEdit, DocumentDate, Survivor)
        where T : BaseRecord;

}

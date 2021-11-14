using System;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.BizLayer.LifeSituations
{
    /// <summary>
    /// Базовый класс документов описывающих жизненную ситуацию
    /// </summary>
    /// <param name="ID">идентификатор записи</param>
    /// <param name="IsDeleted">Признак удалённой записи</param>
    /// <param name="Created">Дата создания записи</param>
    /// <param name="LastEdit">Дата последнего редактирования записи</param>
    /// <param name="DocumentDate">Дата документа</param>
    /// <param name="Survivor">Пострадавший к которому относится эта запись</param>
    public abstract record LifeSituationDocument(Guid ID, bool IsDeleted, DateTime Created, DateTime LastEdit,
        DateTime DocumentDate, Survivor Survivor) : BaseDomainModel(ID, IsDeleted, Created, LastEdit);
}

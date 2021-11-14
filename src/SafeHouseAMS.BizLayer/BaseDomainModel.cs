using System;

namespace SafeHouseAMS.BizLayer
{
    /// <summary>
    /// Базовая доменная модель
    /// </summary>
    /// <param name="ID">идентификатор записи</param>
    /// <param name="IsDeleted">признак удаленной записи</param>
    /// <param name="Created">временная метка создания</param>
    /// <param name="LastEdit">временная метка последнего редактирования</param>
    public abstract record BaseDomainModel(Guid ID, bool IsDeleted, DateTime Created, DateTime LastEdit) : IDomainModel;
}

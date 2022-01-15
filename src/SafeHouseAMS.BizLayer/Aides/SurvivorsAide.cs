using System;

namespace SafeHouseAMS.BizLayer.Aides;

/// <summary>
/// "Помощник" выделенный для этого пострадавшего
/// </summary>
/// <param name="ID">идентификатор записи</param>
/// <param name="IsDeleted">признак удалённой записи</param>
/// <param name="Created">дата создания записи</param>
/// <param name="LastEdit">дата последнего редактирования записи</param>
/// <param name="SurvivorId">идентификатор пострадавшего</param>
/// <param name="Type">тип помощника</param>
/// <param name="Aide">имя помощника</param>
public record SurvivorsAide(Guid ID, bool IsDeleted, DateTime Created, DateTime LastEdit, Guid SurvivorId, AideType Type, string Aide)
    : BaseDomainModel(ID, IsDeleted, Created, LastEdit);

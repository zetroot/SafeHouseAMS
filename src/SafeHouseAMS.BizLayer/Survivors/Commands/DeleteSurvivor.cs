using System;
using System.Threading.Tasks;

namespace SafeHouseAMS.BizLayer.Survivors.Commands;

/// <summary>
/// Команда удаления записи о прострадавшем
/// </summary>
public class DeleteSurvivor : SurvivorCommand
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="entityID">идентификатор пострадавшего подлежащего удалению</param>
    public DeleteSurvivor(Guid entityID) : base(entityID)
    {
    }

    internal override Task ApplyOn(ISurvivorRepository repository) => repository.DeleteAsync(EntityID);
}

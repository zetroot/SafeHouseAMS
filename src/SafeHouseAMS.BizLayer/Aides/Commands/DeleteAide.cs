using System;
using System.Threading.Tasks;

namespace SafeHouseAMS.BizLayer.Aides.Commands;

/// <summary>
/// Команда удаления записи о помощнике
/// </summary>
public class DeleteAide : AideCommand
{
    /// <summary>
    /// default ctor
    /// </summary>
    /// <param name="entityID">идентификатор удаляемой записи</param>
    public DeleteAide(Guid entityID) : base(entityID)
    {
    }

    internal override Task ApplyOn(IAidesRepository repository) => repository.Delete(EntityID);
}
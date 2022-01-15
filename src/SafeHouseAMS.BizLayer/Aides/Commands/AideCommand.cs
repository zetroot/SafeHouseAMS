using System;

namespace SafeHouseAMS.BizLayer.Aides.Commands;

/// <summary>
/// базовый класс команд управления помощниками пострадавших
/// </summary>
public abstract class AideCommand : BaseCommand<IAidesRepository>
{
    /// <summary>
    /// default ctor
    /// </summary>
    /// <param name="entityID">идентификатор записи о помощнике</param>
    protected AideCommand(Guid entityID) : base(entityID)
    {
    }
}

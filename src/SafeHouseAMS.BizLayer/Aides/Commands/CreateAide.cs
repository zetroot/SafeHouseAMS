using System;
using System.Threading.Tasks;

namespace SafeHouseAMS.BizLayer.Aides.Commands;

/// <summary>
/// команда создания новой записи о помощнике для пострадавшего
/// </summary>
public class CreateAide : AideCommand
{
    /// <summary>
    /// идентификатор пострадавшего к которому относится создаваемая запись
    /// </summary>
    public Guid SurvivorId { get; }

    /// <summary>
    /// тип помощника
    /// </summary>
    public AideType Type { get; }

    /// <summary>
    /// Имя помощника
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="entityID">идентификатор создаваемой записи</param>
    /// <param name="survivorId">идентификатор пострадавшего для которого создаётся запись</param>
    /// <param name="type">тип помощника</param>
    /// <param name="name">имя помощника</param>
    public CreateAide(Guid entityID, Guid survivorId, AideType type, string name) : base(entityID)
    {
        SurvivorId = survivorId;
        Type = type;
        Name = name;
    }

    internal override Task ApplyOn(IAidesRepository repository) =>
        repository.Create(EntityID, false, DateTime.Now, DateTime.Now, SurvivorId, Type, Name);
}
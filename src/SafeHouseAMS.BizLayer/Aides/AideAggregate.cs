using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.Aides.Commands;

namespace SafeHouseAMS.BizLayer.Aides;

/// <inheritdoc />
public class AideAggregate : IAideAggregate
{
    private readonly IAidesRepository _repository;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="repository">repository</param>
    public AideAggregate(IAidesRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public Task<SurvivorsAide?> GetSingleAsync(Guid id, CancellationToken cancellationToken) =>
        _repository.GetSingle(id, cancellationToken);

    /// <inheritdoc />
    public Task ApplyCommand(AideCommand command, CancellationToken cancellationToken) => command.ApplyOn(_repository);

    /// <inheritdoc />
    public IAsyncEnumerable<SurvivorsAide> GetAllBySurvivor(Guid survivorId, CancellationToken cancellationToken) =>
        _repository.GetAllBySurvivor(survivorId, cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<string> GetCompletions(CancellationToken cancellationToken) =>
        _repository.GetAllCompletions(cancellationToken);
}

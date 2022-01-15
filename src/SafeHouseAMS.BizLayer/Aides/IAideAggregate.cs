using System;
using System.Collections.Generic;
using System.Threading;
using SafeHouseAMS.BizLayer.Aides.Commands;

namespace SafeHouseAMS.BizLayer.Aides;

/// <summary>
/// интерфейс агрегата учета помощников
/// </summary>
public interface IAideAggregate : IDomainAggregate<SurvivorsAide, AideCommand>
{
    /// <summary>
    /// получить все записи о помощниках по пострадавшему
    /// </summary>
    /// <param name="survivorId">идентификатор пострадавшего</param>
    /// <param name="cancellationToken">токен отмены</param>
    /// <returns>асинхронная последовательность записей о помощниках</returns>
    IAsyncEnumerable<SurvivorsAide> GetAllBySurvivor(Guid survivorId, CancellationToken cancellationToken);

    /// <summary>
    /// получить список для автозаполненя
    /// </summary>
    /// <param name="cancellationToken">токен отмены</param>
    /// <returns>асинхронная последовательность списка автозаполнения</returns>
    IAsyncEnumerable<string> GetCompletions(CancellationToken cancellationToken);
}

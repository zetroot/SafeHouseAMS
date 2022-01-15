using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SafeHouseAMS.BizLayer.Aides;

/// <summary>
/// интерфейс репозитория помощников
/// </summary>
public interface IAidesRepository
{
    /// <summary>
    /// Получить весь список для автоматического завершения ввода
    /// </summary>
    /// <param name="cancellationToken">токен отмены перечисления</param>
    /// <returns>асинхронная последовательность строк завершения ввода</returns>
    IAsyncEnumerable<string> GetAllCompletions(CancellationToken cancellationToken);

    /// <summary>
    /// получить все записи по определённому пострадавшему
    /// </summary>
    /// <param name="survivorId">идентификатор пострадавшего</param>
    /// <param name="cancellationToken">токен отмены последовательности</param>
    /// <returns>асинхронная последовательность записей</returns>
    IAsyncEnumerable<SurvivorsAide> GetAllBySurvivor(Guid survivorId, CancellationToken cancellationToken);

    /// <summary>
    /// создать новую запись
    /// </summary>
    /// <param name="id">идентификатор создаваемой записи</param>
    /// <param name="isDeleted">признак удалённой записи</param>
    /// <param name="created">дата создания</param>
    /// <param name="lastEdit">дата последнего редактирования</param>
    /// <param name="survivorID">идентификатор пострадавшего</param>
    /// <param name="type">тип помощника</param>
    /// <param name="aideName">имя помощника</param>
    Task Create(Guid id, bool isDeleted, DateTime created, DateTime lastEdit,
        Guid survivorID, AideType type, string aideName);

    /// <summary>
    /// удалить запись о помощнике
    /// </summary>
    /// <param name="id">идентификатор записи</param>
    Task Delete(Guid id);

    /// <summary>
    /// получить запись по идентификатору
    /// </summary>
    /// <param name="id">идентификатор записи</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <returns>запись, если таковую удалось найти</returns>
    Task<SurvivorsAide?> GetSingle(Guid id, CancellationToken cancellationToken);

}

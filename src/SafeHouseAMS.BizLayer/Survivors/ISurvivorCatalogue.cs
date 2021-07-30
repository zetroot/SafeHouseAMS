using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.Survivors.Commands;

namespace SafeHouseAMS.BizLayer.Survivors
{
    /// <summary>
    /// Интерфейс агрегата бизнес-логики реестра пострадавих 
    /// </summary>
    public interface ISurvivorCatalogue : IDomainAggregate<Survivor, SurvivorCommand>
    {
        /// <summary>
        /// Получить список пострадавших
        /// </summary>
        /// <param name="skip">Пропустить первых записей</param>
        /// <param name="take">Ограничить размер выдачи. Если null - то не ограничивать</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns></returns>
        IAsyncEnumerable<Survivor> GetCollection(int skip, int? take, CancellationToken cancellationToken);

        /// <summary>
        /// Получить общее количество записей в справочнике
        /// </summary>
        /// <returns>общее количество пострадавших</returns>
        Task<int> GetTotalCount();
    }
}
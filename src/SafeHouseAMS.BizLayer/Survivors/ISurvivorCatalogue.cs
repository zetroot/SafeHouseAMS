using System.Collections.Generic;
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
        /// <returns></returns>
        IAsyncEnumerable<Survivor> GetCollection();
    }
}
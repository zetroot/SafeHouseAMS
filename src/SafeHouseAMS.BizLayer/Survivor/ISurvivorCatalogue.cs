using System.Collections.Generic;
using SafeHouseAMS.BizLayer.Survivor.Commands;

namespace SafeHouseAMS.BizLayer.Survivor
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
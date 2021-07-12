using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.Abstractions.Models;

namespace SafeHouseAMS.BizLayer.Abstractions.Services
{
    /// <summary>
    /// Интерфейс каталога пострадавших
    /// </summary>
    public interface ISurvivorCatalogue
    {
        IAsyncEnumerable<Survivor> GetCollection();
        Task<Survivor> Add(Survivor adding);
        Task<Survivor> Update(Survivor updating);
        Task Delete(Guid id);
    }
}
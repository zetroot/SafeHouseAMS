using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SafeHouseCRM.BizLayer.Abstractions.Models;

namespace SafeHouseCRM.BizLayer.Abstractions.Services
{
    /// <summary>
    /// Интерфейс каталога пострадавших
    /// </summary>
    public interface IPersonCatalogue
    {
        IAsyncEnumerable<Person> GetCollection();
        Task<Person> Add(Person adding);
        Task<Person> Update(Person updating);
        Task Delete(Guid id);
    }
}
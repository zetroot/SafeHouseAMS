using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.Abstractions.Models;
using SafeHouseAMS.BizLayer.Abstractions.Services;

namespace SafeHouseAMS.WasmApp.Services
{
    /// <summary>
    /// каталог пострадавших - демо
    /// </summary>
    public class InMemorySurvivorCatalogue : ISurvivorCatalogue
    {
        private readonly List<Survivor> _store = new()
        {
            new()
            {
                ID = Guid.NewGuid(), Name = "Boris", Surname = "Britva", BirthDate = DateTime.Today, IsDeleted = false
            },
            new()
            {
                ID = Guid.NewGuid(), Name = "Boris", Surname = "Hren popadesh", BirthDate = DateTime.Today, IsDeleted = true
            },
            new()
            {
                ID = Guid.NewGuid(), Name = "Boris", Surname = "Kot", BirthDate = DateTime.Today, IsDeleted = false
            }
        };
        
        public async IAsyncEnumerable<Survivor> GetCollection()
        {
            foreach (var person in _store)
            {
                await Task.Yield();
                yield return person;
            }
        }

        public Task<Survivor> Add(Survivor adding)
        {
            adding.ID = Guid.NewGuid();
            _store.Add(adding);
            return Task.FromResult(adding);
        }

        public Task<Survivor> Update(Survivor updating)
        {
            if (_store.Any(x => x.ID == updating.ID))
            {
                var found = _store.Single(x => x.ID == updating.ID);
                found.Name = updating.Name;
                found.BirthDate = updating.BirthDate;
                found.Surname = updating.Surname;
                return Task.FromResult(found);
            }

            throw new InvalidOperationException();
        }

        public Task Delete(Guid id)
        {
            if (_store.FirstOrDefault(x => x.ID == id) is { } deleting)
            {
                deleting.IsDeleted = true;
                return Task.CompletedTask;
            }

            throw new InvalidOperationException();
        }
    }
}
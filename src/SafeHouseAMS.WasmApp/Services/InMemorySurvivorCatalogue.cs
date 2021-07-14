using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.Survivor;

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
                ID = Guid.NewGuid(), Name = "Boris", BirthDateAccurate = new DateTime(1980, 05, 09),
                Num = 5
            },
            new()
            {
                ID = Guid.NewGuid(), Name = "Boris", BirthDateCalculated = new DateTime(1970, 01, 01),
                Num = 7
            },
            new() {ID = Guid.NewGuid(), Name = "Boris", Num = 9}
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
            adding.Num = _store.Select(x => x.Num).Max() + 1;
            _store.Add(adding);
            return Task.FromResult(adding);
        }

        public Task<Survivor> Update(Survivor updating)
        {
            if (!_store.Any(x => x.ID == updating.ID)) 
                throw new InvalidOperationException();
            
            var found = _store.Single(x => x.ID == updating.ID);
            found.Name = updating.Name;
            found.BirthDateAccurate = updating.BirthDateAccurate;
            found.BirthDateCalculated = updating.BirthDateCalculated;
            return Task.FromResult(found);
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

        public Task<Survivor> GetSingleAsync(Guid id)
        {
            var item = _store.SingleOrDefault(x => x.ID == id);
            if (item == null)
                throw new InvalidOperationException();
            return Task.FromResult(item);
        }
    }
}
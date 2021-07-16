using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.WasmApp.Services
{
    public class InMemorySurvivorsRepository : ISurvivorRepository
    {
        private readonly List<Survivor> _store;

        public InMemorySurvivorsRepository()
        {
            _store = Enumerable
                .Range(42, 200)
                .Select(x =>
                {
                    var lastEdit = DateTimeOffset.Now - TimeSpan.FromHours(Faker.RandomNumber.Next(24, 24000));
                    var created = lastEdit - TimeSpan.FromHours(Faker.RandomNumber.Next(24, 24000));
                    var dob = DateTime.Today - TimeSpan.FromDays(Faker.RandomNumber.Next(7000, 19000));
                    return new Survivor(Guid.NewGuid(), Faker.Boolean.Random(), created, lastEdit,
                        Faker.Name.FullName(), x, Faker.Enum.Random<SexEnum>(), Faker.Boolean.Random() ? "unknown" : null
                        , dob, null);
                })
                .OrderByDescending(x => x.LastEdit)
                .ToList();
        }

        public Task Create(Guid id, bool isDeleted, DateTimeOffset created, DateTimeOffset lastEdit, string name, SexEnum sex,
            string? otherSex, DateTimeOffset? accurateDob, DateTimeOffset? calculatedDob)
        {
            if (_store.Any(x => x.ID == id)) throw new InvalidOperationException();
            var nextNum = _store.Max(x => x.Num) + 1;
            _store.Add(new(id, isDeleted,created,lastEdit,name, nextNum, sex, otherSex, accurateDob, calculatedDob));
            return Task.CompletedTask;
        }

        public Task<Survivor> GetSingleAsync(Guid id, CancellationToken cancellationToken)
        {
            var found = _store.SingleOrDefault(x => x.ID == id);
            return Task.FromResult(found ?? throw new InvalidOperationException());
        }

        public async IAsyncEnumerable<Survivor> GetCollection(int skip, int? take)
        {
            await Task.Yield();
            var slice = take.HasValue ? _store.Skip(skip).Take(take.Value) : _store.Skip(skip);
            foreach (var survivor in slice)
            {
                yield return survivor;
            }
        }

        public Task<int> GetTotalCount() => Task.FromResult(_store.Count);
    }
}
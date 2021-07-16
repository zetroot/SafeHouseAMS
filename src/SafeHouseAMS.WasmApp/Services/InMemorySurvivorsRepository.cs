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
        // {
        //     new(Guid.NewGuid(), false, DateTimeOffset.Now, DateTimeOffset.Now, "Boris Britva",
        //         3,
        //         SexEnum.Male, null, 
        //         new(1970, 01 ,01, 0,0,0, TimeSpan.FromHours(3)), null),
        //     
        //     new(Guid.NewGuid(), false, DateTimeOffset.Now, DateTimeOffset.Now, "Кошка Блоха",
        //         5,
        //         SexEnum.Female, null, 
        //         null, new(2007, 06 ,15, 0,0,0, TimeSpan.FromHours(3))),
        //     
        //     new(Guid.NewGuid(), false, DateTimeOffset.Now, DateTimeOffset.Now, "Паук Например",
        //         6,
        //         SexEnum.Other, null, 
        //         null, null),
        //     
        //     new(Guid.NewGuid(), false, DateTimeOffset.Now, DateTimeOffset.Now, "Неустановленное лицо",
        //         42,
        //         SexEnum.Other, "не установлен", 
        //         null, null),
        // };

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
                        Faker.Name.FullName(), x, Faker.Enum.Random<SexEnum>(), Faker.Lorem.GetFirstWord(), dob, null);
                })
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

        public async IAsyncEnumerable<Survivor> GetCollection()
        {
            await Task.Yield();
            foreach (var survivor in _store)
            {
                yield return survivor;
            }
        }
    }
}
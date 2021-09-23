using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.DataLayer.Repositories
{
    internal class SurvivorsRepository : ISurvivorRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public SurvivorsRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Create(Guid id, bool isDeleted, DateTime created, DateTime lastEdit,
            string name, SexEnum sex, string? otherSex, DateTime? accurateDob, DateTime? calculatedDob)
        {
            int maxNum = 0;
            if(await _dataContext.Survivors.AnyAsync())
                maxNum = await _dataContext.Survivors.MaxAsync(x => x.Num);

            await _dataContext.Survivors.AddAsync(new(){
                ID = id, IsDeleted = isDeleted, Created = created, LastEdit = lastEdit, Num = maxNum+1,
                Name = name, Sex = (int) sex, OtherSex = otherSex, BirthDateAccurate = accurateDob, BirthDateCalculated = calculatedDob});
            await _dataContext.SaveChangesAsync();
        }
        public async Task<Survivor?> GetSingleAsync(Guid id, CancellationToken cancellationToken)
        {
            var survivor = await _dataContext.Survivors.SingleOrDefaultAsync(x => !x.IsDeleted && x.ID == id, cancellationToken);
            return _mapper.Map<Survivor>(survivor);
        }

        public IAsyncEnumerable<Survivor> GetCollection(int skip, int? take, CancellationToken cancellationToken)
        {
            if (take.HasValue)
                return GetCollectionLimited(skip, take.Value, cancellationToken);
            else
                return GetCollectionUnlimited(skip, cancellationToken);
        }
        private async IAsyncEnumerable<Survivor> GetCollectionUnlimited(int skip, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var filteredRecortds = _dataContext.Survivors
                .OrderByDescending(x => x.LastEdit)
                .Where(x => !x.IsDeleted)
                .Skip(skip)
                .AsAsyncEnumerable();
            await foreach (var survivor in filteredRecortds.WithCancellation(cancellationToken))
            {
                yield return _mapper.Map<Survivor>(survivor);
            }
        }

        private async IAsyncEnumerable<Survivor> GetCollectionLimited(int skip, int take, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var filteredRecortds = _dataContext.Survivors
                .OrderByDescending(x => x.LastEdit)
                .Where(x => !x.IsDeleted)
                .Skip(skip)
                .Take(take)
                .AsAsyncEnumerable();
            await foreach (var survivor in filteredRecortds.WithCancellation(cancellationToken))
            {
                yield return _mapper.Map<Survivor>(survivor);
            }
        }

        public Task<int> GetTotalCount()
        {
            return _dataContext.Survivors.CountAsync(x => !x.IsDeleted);
        }
    }
}

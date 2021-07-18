using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.InquirySources;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.WasmApp.Services
{
    public class InMemoryDocumentsRepository : ILifeSituationDocumentsRepository
    {
        private readonly List<LifeSituationDocument> _documentsStore = new();
        private readonly List<BaseRecord> _recordsStore = new();
        private readonly ISurvivorRepository _survivorRepository;
        public InMemoryDocumentsRepository(ISurvivorRepository survivorRepository)
        {
            _survivorRepository = survivorRepository ?? throw new ArgumentNullException(nameof(survivorRepository));
        }
        public Task<LifeSituationDocument> GetSingleAsync(Guid id, CancellationToken cancellationToken) => 
            Task.FromResult(_documentsStore.Single(x => x.ID == id));
        
        public async IAsyncEnumerable<LifeSituationDocument> GetAllBySurvivor(Guid survivorId, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await Task.Yield();
            foreach (var document in _documentsStore)
            {
                yield return document;
            }
        }
        
        public async Task CreateInquiry(Guid documentId, bool isDeleted, DateTimeOffset created, DateTimeOffset lastEdit, Guid survivorID, DateTimeOffset documentDate, bool isJuvenile, IEnumerable<IInquirySource> inquirySources)
        {
            var survivor = await _survivorRepository.GetSingleAsync(survivorID, CancellationToken.None);
            _documentsStore.Add(new Inquiry(documentId, isDeleted, created, lastEdit, documentDate, survivor, isJuvenile, inquirySources, new(default, ""), null, null, null, null, null, null));
        }
        public Task AddRecord<T>(Guid documentId, T record) where T : BaseRecord
        {
            _recordsStore.Add(record);
            return Task.CompletedTask;
        }
    }
}
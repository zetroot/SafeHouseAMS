using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.InquirySources;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

namespace SafeHouseAMS.DataLayer.Repositories
{
    internal class LifeSituationDocumentsRepository : ILifeSituationDocumentsRepository
    {
        private readonly DataContext _context;
        public async Task<LifeSituationDocument> GetSingleAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LifeSituationDocument> GetAllBySurvivor(Guid survivorId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task CreateInquiry(Guid documentId, bool isDeleted, DateTime created, DateTime lastEdit, Guid survivorID, DateTime documentDate, bool isJuvenile, IEnumerable<IInquirySource> inquirySources)
        {
            throw new NotImplementedException();
        }
        public async Task AddRecord(Guid documentId, BaseRecord record)
        {
            throw new NotImplementedException();
        }
    }
}
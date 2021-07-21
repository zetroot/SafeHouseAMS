using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.InquirySources;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

namespace SafeHouseAMS.DataLayer.Repositories
{
    internal class LifeSituationDocumentsRepository : ILifeSituationDocumentsRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public LifeSituationDocumentsRepository(DataContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<LifeSituationDocument> GetSingleAsync(Guid id, CancellationToken cancellationToken)
        {
            var doc = await _context.LifeSituationDocuments
                .SingleAsync(x => !x.IsDeleted && x.ID == id, cancellationToken);
            return _mapper.Map<LifeSituationDocument>(doc);
        }
        public IAsyncEnumerable<LifeSituationDocument> GetAllBySurvivor(Guid survivorId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task CreateInquiry(Guid documentId, bool isDeleted, DateTime created, DateTime lastEdit, Guid survivorID, DateTime documentDate, bool isJuvenile, IEnumerable<IInquirySource> inquirySources)
        {
            throw new NotImplementedException();
        }
        public Task AddRecord(Guid documentId, BaseRecord record)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.InquirySources;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.DataLayer.Models.LifeSituations;

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
        public async IAsyncEnumerable<LifeSituationDocument> GetAllBySurvivor(Guid survivorId, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var documents = _context.LifeSituationDocuments
                .Where(x => !x.IsDeleted && x.SurvivorID == survivorId).AsAsyncEnumerable();
            await foreach (var doc in documents.WithCancellation(cancellationToken))
                yield return _mapper.Map<LifeSituationDocument>(doc);
        }
        
        public async Task CreateInquiry(Guid documentId, bool isDeleted, DateTime created, DateTime lastEdit,
            Guid survivorID, DateTime documentDate,
            bool isJuvenile, IEnumerable<IInquirySource> inquirySources)
        {
            var creatingDocument = new InquiryDAL
            {
                ID = documentId, IsDeleted = isDeleted, Created = created, LastEdit = lastEdit,
                SurvivorID = survivorID, DocumentDate = documentDate,
                IsJuvenile = isJuvenile
            };
            foreach (var source in inquirySources)
            {
                switch (source)
                {
                    case SelfInquiry selfInquiry:
                        creatingDocument.IsSelfInquiry = true;
                        creatingDocument.SelfInquirySourcesMask = (int) selfInquiry.Channel;
                        break;
                    case ForwardedBySurvivor forwardedBySurvivor:
                        creatingDocument.IsForwardedBySurvivor = true;
                        creatingDocument.ForwardedBySurvivor = forwardedBySurvivor.ForwardedBy;
                        break;
                    case ForwardedByPerson forwardedByPerson:
                        creatingDocument.IsForwardedByPerson = true;
                        creatingDocument.ForwardedByPerson = forwardedByPerson.ForwardedBy;
                        break;
                    case ForwardedByOrganization forwardedByOrganization:
                        creatingDocument.IsForwardedByOrganization = true;
                        creatingDocument.ForwardedByOrgannization = forwardedByOrganization.ForwardedBy;
                        break;
                }
            }
            await _context.LifeSituationDocuments.AddAsync(creatingDocument);
            await _context.SaveChangesAsync();
        }
        
        public async Task AddRecord(Guid documentId, BaseRecord record)
        {
            BaseRecordDAL addingRecord = record switch
            {
                ChildrenRecord _ => new ChildrenRecordDAL(),
                CitizenshipRecord _ => new CitizenshipRecordDAL(),
                DomicileRecord _ => new DomicileRecordDAL(),
                EducationLevelRecord _ => new EducationLevelRecordDAL(),
                SpecialityRecord _ => new SpecialityRecordDAL(),
                _ => throw new ArgumentException("Не реализовано сохранение записи такого типа")
            };

            addingRecord.ID = record.ID;
            addingRecord.DocumentID = documentId;
            addingRecord.Content = JsonSerializer.Serialize(record);

            await _context.Records.AddAsync(addingRecord);
            await _context.SaveChangesAsync();
        }
    }
}
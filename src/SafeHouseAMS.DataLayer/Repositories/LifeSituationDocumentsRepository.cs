using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.InquirySources;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.BizLayer.LifeSituations.Vulnerabilities;
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
                .Include(x => (x as InquiryDAL)!.Citizenship)
                .SingleAsync(x => !x.IsDeleted && x.ID == id, cancellationToken);
            return _mapper.Map<LifeSituationDocument>(doc);
        }
        public async IAsyncEnumerable<LifeSituationDocument> GetAllBySurvivor(Guid survivorId, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var documents = _context.LifeSituationDocuments
                .Include(x => (x as InquiryDAL)!.Citizenship)
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
                ChildrenRecord x => new ChildrenRecordDAL{ID = record.ID, DocumentID = documentId, Content = JsonSerializer.Serialize(x)},
                CitizenshipRecord x => new CitizenshipRecordDAL{ID = record.ID, DocumentID = documentId, Content = JsonSerializer.Serialize(x)},
                DomicileRecord x => new DomicileRecordDAL{ID = record.ID, DocumentID = documentId, Content = JsonSerializer.Serialize(x)},
                EducationLevelRecord x => new EducationLevelRecordDAL{ID = record.ID, DocumentID = documentId, Content = JsonSerializer.Serialize(x)},
                SpecialityRecord x => new SpecialityRecordDAL{ID = record.ID, DocumentID = documentId, Content = JsonSerializer.Serialize(x)},
                _ => throw new ArgumentException("Не реализовано сохранение записи такого типа")
            };
            
            await _context.Records.AddAsync(addingRecord);
            await _context.SaveChangesAsync();
        }
        
        public async IAsyncEnumerable<string> GetCitizenshipsCompletions([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var sourceRecords = _context.Records.OfType<CitizenshipRecordDAL>().AsAsyncEnumerable();
            var temp = new List<string>(50);
            await foreach(var record in sourceRecords.WithCancellation(cancellationToken))
                temp.Add(_mapper.Map<CitizenshipRecord>(record).Citizenship);

            var sortedList = temp
                .Distinct()
                .Select(x => (counts: temp.Count(y => y == x), citizenship: x))
                .OrderByDescending(x => x.counts)
                .Select(x => x.citizenship);

            foreach (var item in sortedList)
                yield return item;
        }
        public async Task SetWorkingExperience(Guid inquiryId, string workingExperience)
        {
            var document = await _context.LifeSituationDocuments.SingleAsync(x => x.ID == inquiryId);
            if (document is InquiryDAL inquiry)
            {
                inquiry.WorkingExperience = workingExperience;
                await _context.SaveChangesAsync();
            }
        }
        public Task SetAddiction(Guid inquiryId, string addictionKind)
        {
            throw new NotImplementedException();
        }
        public Task ClearAddiction(Guid inquiryId)
        {
            throw new NotImplementedException();
        }
        public Task SetHomeless(Guid inquiryId)
        {
            throw new NotImplementedException();
        }
        public Task ClearHomeless(Guid inquiryId)
        {
            throw new NotImplementedException();
        }
        public Task SetMigration(Guid inquiryId)
        {
            throw new NotImplementedException();
        }
        public Task ClearMigration(Guid inquiryId)
        {
            throw new NotImplementedException();
        }
        public Task SetChildhoodViolence(Guid inquiryId)
        {
            throw new NotImplementedException();
        }
        public Task ClearChildhoodViolence(Guid inquiryId)
        {
            throw new NotImplementedException();
        }
        public Task SetOrphanageExperience(Guid inquiryId)
        {
            throw new NotImplementedException();
        }
        public Task ClearOrphanageExperience(Guid inquiryId)
        {
            throw new NotImplementedException();
        }
        public Task SetOther(Guid inquiryId, string details)
        {
            throw new NotImplementedException();
        }
        public Task ClearOther(Guid inquiryId)
        {
            throw new NotImplementedException();
        }
        public Task SetHealthStatusVulnerability(Guid inquiryId, HealthStatus.HealthStatusVulnerabilityType healthStatus, string? details)
        {
            throw new NotImplementedException();
        }
        public Task ClearHealthStatusVulnerability(Guid inquiryId)
        {
            throw new NotImplementedException();
        }
    }
}
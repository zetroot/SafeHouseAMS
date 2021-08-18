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
                .Include(x => x.AllRecords)
                .Include(x => x.Survivor)
                .SingleAsync(x => !x.IsDeleted && x.ID == id, cancellationToken);
            return _mapper.Map<LifeSituationDocument>(doc);
        }
        public async IAsyncEnumerable<LifeSituationDocument> GetAllBySurvivor(Guid survivorId, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var documents = _context.LifeSituationDocuments
                .Include(x => x.AllRecords)
                .Include(x => x.Survivor)
                .Where(x => !x.IsDeleted && x.SurvivorID == survivorId)
                .OrderByDescending(x => x.DocumentDate)
                .AsSplitQuery()
                .AsAsyncEnumerable();
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
                RegistrationStatusRecord x => new RegistrationStatusRecordDAL{ID = record.ID, DocumentID = documentId, Content = JsonSerializer.Serialize(x)},
                MigrationStatusRecord x => new MigrationStatusRecordDAL{ID = record.ID, DocumentID = documentId, Content = JsonSerializer.Serialize(x)},
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
        public async Task SetAddiction(Guid inquiryId, string addictionKind)
        {
            var document = await _context.LifeSituationDocuments.SingleAsync(x => x.ID == inquiryId);
            if (document is InquiryDAL inquiry)
            {
                inquiry.HasAddiction = true;
                inquiry.AddictionKind = addictionKind;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearAddiction(Guid inquiryId)
        {
            var document = await _context.LifeSituationDocuments.SingleAsync(x => x.ID == inquiryId);
            if (document is InquiryDAL inquiry)
            {
                inquiry.HasAddiction = false;
                inquiry.AddictionKind = null;
                await _context.SaveChangesAsync();
            }
        }

        public async Task SetHomeless(Guid inquiryId)
        {
            var document = await _context.LifeSituationDocuments.SingleAsync(x => x.ID == inquiryId);
            if (document is InquiryDAL inquiry)
            {
                inquiry.Homelessness = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearHomeless(Guid inquiryId)
        {
            var document = await _context.LifeSituationDocuments.SingleAsync(x => x.ID == inquiryId);
            if (document is InquiryDAL inquiry)
            {
                inquiry.Homelessness = false;
                await _context.SaveChangesAsync();
            }
        }
        public async Task SetMigration(Guid inquiryId)
        {
            var document = await _context.LifeSituationDocuments.SingleAsync(x => x.ID == inquiryId);
            if (document is InquiryDAL inquiry)
            {
                inquiry.Migration = true;
                await _context.SaveChangesAsync();
            }
        }
        public async Task ClearMigration(Guid inquiryId)
        {
            var document = await _context.LifeSituationDocuments.SingleAsync(x => x.ID == inquiryId);
            if (document is InquiryDAL inquiry)
            {
                inquiry.Migration = false;
                await _context.SaveChangesAsync();
            }
        }
        public async Task SetChildhoodViolence(Guid inquiryId)
        {
            var document = await _context.LifeSituationDocuments.SingleAsync(x => x.ID == inquiryId);
            if (document is InquiryDAL inquiry)
            {
                inquiry.ChildhoodViolence = true;
                await _context.SaveChangesAsync();
            }
        }
        public async Task ClearChildhoodViolence(Guid inquiryId)
        {
            var document = await _context.LifeSituationDocuments.SingleAsync(x => x.ID == inquiryId);
            if (document is InquiryDAL inquiry)
            {
                inquiry.ChildhoodViolence = false;
                await _context.SaveChangesAsync();
            }
        }
        public async Task SetOrphanageExperience(Guid inquiryId)
        {
            var document = await _context.LifeSituationDocuments.SingleAsync(x => x.ID == inquiryId);
            if (document is InquiryDAL inquiry)
            {
                inquiry.OrphanageExperience = true;
                await _context.SaveChangesAsync();
            }
        }
        public async Task ClearOrphanageExperience(Guid inquiryId)
        {
            var document = await _context.LifeSituationDocuments.SingleAsync(x => x.ID == inquiryId);
            if (document is InquiryDAL inquiry)
            {
                inquiry.OrphanageExperience = false;
                await _context.SaveChangesAsync();
            }
        }
        public async Task SetOther(Guid inquiryId, string details)
        {
            var document = await _context.LifeSituationDocuments.SingleAsync(x => x.ID == inquiryId);
            if (document is InquiryDAL inquiry)
            {
                inquiry.HasOtherVulnerability = true;
                inquiry.OtherVulnerabilityDetails = details;
                await _context.SaveChangesAsync();
            }
        }
        public async Task ClearOther(Guid inquiryId)
        {
            var document = await _context.LifeSituationDocuments.SingleAsync(x => x.ID == inquiryId);
            if (document is InquiryDAL inquiry)
            {
                inquiry.HasOtherVulnerability = false;
                inquiry.OtherVulnerabilityDetails = null;
                await _context.SaveChangesAsync();
            }
        }
        public async Task SetHealthStatusVulnerability(Guid inquiryId, HealthStatus.HealthStatusVulnerabilityType healthStatus, string? details)
        {
            var document = await _context.LifeSituationDocuments.SingleAsync(x => x.ID == inquiryId);
            if (document is InquiryDAL inquiry)
            {
                inquiry.HealthStatusVulnerabilityMask = (int)healthStatus;
                inquiry.OtherHealthStatusVulnerabilityDetail = details;
                await _context.SaveChangesAsync();
            }
        }
        public async Task ClearHealthStatusVulnerability(Guid inquiryId)
        {
            var document = await _context.LifeSituationDocuments.SingleAsync(x => x.ID == inquiryId);
            if (document is InquiryDAL inquiry)
            {
                inquiry.HealthStatusVulnerabilityMask = 0;
                inquiry.OtherHealthStatusVulnerabilityDetail = null;
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateRecordUpdateDocument(Guid docId, bool isDeleted, DateTime created, DateTime lastEdit,
            Guid survivorID, DateTime documentDate, Type recordType)
        {
            LifeSituationDocumentDAL doc = recordType switch
            {
                {} type when type == typeof(ChildrenRecord) => new ChildrenUpdateDAL(),
                {} type when type == typeof(CitizenshipRecord) => new CitizenshipChangeDAL(),
                {} type when type == typeof(DomicileRecord) => new DomicileUpdateDAL(),
                {} type when type == typeof(EducationLevelRecord) => new EducationLevelUpdateDAL(),
                {} type when type == typeof(MigrationStatusRecord) => new MigrationStatusUpdateDAL(),
                {} type when type == typeof(RegistrationStatusRecord) => new RegistrationStatusUpdateDAL(),
                {} type when type == typeof(SpecialityRecord) => new SpecialitiesUpdateDAL(),
                _ => throw new ArgumentException()
            };
            doc.ID = docId;
            doc.IsDeleted = isDeleted;
            doc.Created = created;
            doc.LastEdit = lastEdit;
            doc.DocumentDate = documentDate;
            doc.SurvivorID = survivorID;

            await _context.LifeSituationDocuments.AddAsync(doc);
            await _context.SaveChangesAsync();
        }

        private async Task<(TBiz?, bool)> CountSingleRecords<TDAL, TBiz>(Guid surId, CancellationToken ct)
            where TDAL : BaseRecordDAL
            where TBiz : BaseRecord
        {
            var sortedRecords = _context.Records
                .Include(x => x.Document)
                .Where(x => x.Document.SurvivorID == surId)
                .Where(x => !x.Document.IsDeleted)
                .OrderByDescending(x => x.Document.DocumentDate)
                .OfType<TDAL>();

            var actualRecord = await sortedRecords.FirstOrDefaultAsync(ct);
            var recordDocsCnt = await sortedRecords.Select(x => x.DocumentID).Distinct().CountAsync(ct);

            return (_mapper.Map<TBiz>(actualRecord), recordDocsCnt > 1);
        }

        private async Task<(List<TBiz>, bool)> CountMultiRecords<TDAL,TBiz>(Guid surId, CancellationToken ct)
            where TDAL : BaseRecordDAL
            where TBiz : BaseRecord
        {
            var sortedRecords = _context.Records
                .Include(x => x.Document)
                .Where(x => x.Document.SurvivorID == surId)
                .Where(x => !x.Document.IsDeleted)
                .OrderByDescending(x => x.Document.DocumentDate)
                .OfType<TDAL>();

            var actualRecords = await sortedRecords.ToListAsync(ct);
            var recordDocsCnt = await sortedRecords.Select(x => x.DocumentID).Distinct().CountAsync(ct);

            return (actualRecords.Select(_mapper.Map<TBiz>).ToList(), recordDocsCnt > 1);
        }

        public async Task<SurvivorStateReport> GetSurvivorReport(Guid surId, CancellationToken ct)
        {
            var (childrenRecord, hasChangedChildren) =
                await CountSingleRecords<ChildrenRecordDAL, ChildrenRecord>(surId, ct);
            var (citizenshipRecord, hasChangedCitizenship) =
                await CountSingleRecords<CitizenshipRecordDAL, CitizenshipRecord>(surId, ct);
            var (domicileRecord, hasChangedDomicile) =
                await CountSingleRecords<DomicileRecordDAL, DomicileRecord>(surId, ct);
            var (migrationRecord, hasChangedMigration) =
                await CountSingleRecords<MigrationStatusRecordDAL, MigrationStatusRecord>(surId, ct);
            var (registrationRecord, hasChangedRegistration) =
                await CountSingleRecords<RegistrationStatusRecordDAL, RegistrationStatusRecord>(surId, ct);

            var (eduRecords, hasChangedEdu) =
                await CountMultiRecords<EducationLevelRecordDAL, EducationLevelRecord>(surId, ct);
            var (specialityRecords, hasChangedSpecialities) =
                await CountMultiRecords<SpecialityRecordDAL, SpecialityRecord>(surId, ct);

            return new(surId,
            childrenRecord, hasChangedChildren,
            citizenshipRecord, hasChangedCitizenship,
            domicileRecord, hasChangedDomicile,
            eduRecords, hasChangedEdu,
            migrationRecord, hasChangedMigration,
            registrationRecord, hasChangedRegistration,
            specialityRecords, hasChangedSpecialities);
        }

        public IAsyncEnumerable<RecordHistoryItem> GetRecordHistory<T>(Guid survivorId, CancellationToken cancellationToken)
            where T : BaseRecord
        {
            var allRecs = _context.Records
                .Include(x => x.Document)
                .Where(x => x.Document.SurvivorID == survivorId);
            IQueryable<BaseRecordDAL> typedRecs;
            if (typeof(T) == typeof(ChildrenRecord)) typedRecs = allRecs.OfType<ChildrenRecordDAL>();
            else if (typeof(T) == typeof(CitizenshipRecord)) typedRecs = allRecs.OfType<CitizenshipRecordDAL>();
            else if (typeof(T) == typeof(DomicileRecord)) typedRecs = allRecs.OfType<DomicileRecordDAL>();
            else if (typeof(T) == typeof(EducationLevelRecord)) typedRecs = allRecs.OfType<EducationLevelRecordDAL>();
            else if (typeof(T) == typeof(MigrationStatusRecord)) typedRecs = allRecs.OfType<MigrationStatusRecordDAL>();
            else if (typeof(T) == typeof(RegistrationStatusRecord)) typedRecs = allRecs.OfType<RegistrationStatusRecordDAL>();
            else if (typeof(T) == typeof(SpecialityRecord)) typedRecs = allRecs.OfType<SpecialityRecordDAL>();
            else typedRecs = allRecs;

            return typedRecs.Select(x => new RecordHistoryItem(x.Document.DocumentDate, x.DocumentID)).AsAsyncEnumerable();
        }
    }
}

using System;
using AutoMapper;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.Documents;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class LifeSituationDocumentsMappingProfile : Profile
    {
        public LifeSituationDocumentsMappingProfile()
        {
            CreateMap<LifeSituationDocument, SafeHouseAMS.Transport.Protos.Models.LifeSituations.LifeSituationDocument>(MemberList.None)
                .ConstructUsing((src, ctx) =>
                {
                    var result = new Protos.Models.LifeSituations.LifeSituationDocument();
                    switch (src)
                    {
                        case Inquiry inquiry:
                            result.Inquiry = ctx.Mapper.Map<Protos.Models.LifeSituations.Inquiry>(inquiry);
                            break;
                        case SingleRecordUpdate<CitizenshipRecord> doc:
                            result.CitizenshipUpdate =
                                ctx.Mapper.Map<Protos.Models.LifeSituations.CitizenshipUpdate>(doc);
                            break;
                        case SingleRecordUpdate<ChildrenRecord> doc:
                            result.ChildrenUpdate = ctx.Mapper.Map<Protos.Models.LifeSituations.ChildrenUpdate>(doc);
                            break;
                        case SingleRecordUpdate<DomicileRecord> doc:
                            result.DomicileUpdate = ctx.Mapper.Map<Protos.Models.LifeSituations.DomicileUpdate>(doc);
                            break;
                        case SingleRecordUpdate<MigrationStatusRecord> doc:
                            result.MigrationStatusUpdate =
                                ctx.Mapper.Map<Protos.Models.LifeSituations.MigrationStatusUpdate>(doc);
                            break;
                        case SingleRecordUpdate<RegistrationStatusRecord> doc:
                            result.RegistrationStatusUpdate =
                                ctx.Mapper.Map<Protos.Models.LifeSituations.RegistrationStatusUpdate>(doc);
                            break;
                        case MultiRecordsUpdate<EducationLevelRecord> doc:
                            result.EducationUpdate = ctx.Mapper.Map<Protos.Models.LifeSituations.EducationUpdate>(doc);
                            break;
                        case MultiRecordsUpdate<SpecialityRecord> doc:
                            result.SpecialitiesUpdate =
                                ctx.Mapper.Map<Protos.Models.LifeSituations.SpecialitiesUpdate>(doc);
                            break;
                    }
                    return result;
                });

            CreateMap<SafeHouseAMS.Transport.Protos.Models.LifeSituations.LifeSituationDocument, LifeSituationDocument>(MemberList.None)
                .ConstructUsing((src, ctx) => src.DocumentCase switch
                {
                    Protos.Models.LifeSituations.LifeSituationDocument.DocumentOneofCase.Inquiry =>
                        ctx.Mapper.Map<Inquiry>(src.Inquiry),

                    Protos.Models.LifeSituations.LifeSituationDocument.DocumentOneofCase.ChildrenUpdate =>
                        ctx.Mapper.Map<SingleRecordUpdate<ChildrenRecord>>(src.ChildrenUpdate),
                    Protos.Models.LifeSituations.LifeSituationDocument.DocumentOneofCase.CitizenshipUpdate =>
                        ctx.Mapper.Map<SingleRecordUpdate<CitizenshipRecord>>(src.CitizenshipUpdate),
                    Protos.Models.LifeSituations.LifeSituationDocument.DocumentOneofCase.DomicileUpdate =>
                        ctx.Mapper.Map<SingleRecordUpdate<DomicileRecord>>(src.DomicileUpdate),
                    Protos.Models.LifeSituations.LifeSituationDocument.DocumentOneofCase.EducationUpdate =>
                        ctx.Mapper.Map<MultiRecordsUpdate<EducationLevelRecord>>(src.EducationUpdate),
                    Protos.Models.LifeSituations.LifeSituationDocument.DocumentOneofCase.MigrationStatusUpdate =>
                        ctx.Mapper.Map<SingleRecordUpdate<MigrationStatusRecord>>(src.MigrationStatusUpdate),
                    Protos.Models.LifeSituations.LifeSituationDocument.DocumentOneofCase.RegistrationStatusUpdate =>
                        ctx.Mapper.Map<SingleRecordUpdate<RegistrationStatusRecord>>(src.RegistrationStatusUpdate),
                    Protos.Models.LifeSituations.LifeSituationDocument.DocumentOneofCase.SpecialitiesUpdate =>
                        ctx.Mapper.Map<MultiRecordsUpdate<SpecialityRecord>>(src.SpecialitiesUpdate),

                    _ => throw new InvalidOperationException()
                });
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.InquirySources;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.BizLayer.LifeSituations.Vulnerabilities;
using SafeHouseAMS.BizLayer.Survivors;
using SafeHouseAMS.DataLayer.Models.LifeSituations;

namespace SafeHouseAMS.DataLayer.MapperProfiles
{
    internal class LifeSituationsDocumentMapping : Profile
    {
        public LifeSituationsDocumentMapping()
        {
            MapBase();
            MapInquiries();
            MapCitizenshipChange();
        }

        private void MapCitizenshipChange()
        {
            CreateMap<CitizenshipChangeDAL, CitizenshipChange>()
                .IncludeBase<LifeSituationDocumentDAL, LifeSituationDocument>();
        }

        private void MapInquiries()
        {
            CreateMap<InquiryDAL, Inquiry>()
                .IncludeBase<LifeSituationDocumentDAL, LifeSituationDocument>()
                .ConstructUsing((src, ctx) =>
                {
                    var survivor = ctx.Mapper.Map<Survivor>(src.Survivor);
                    var inqSrcs = new List<IInquirySource>();
                    if (src.IsSelfInquiry && src.SelfInquirySourcesMask.HasValue)
                        inqSrcs.Add(new SelfInquiry((SelfInquiry.InquiryChannel) src.SelfInquirySourcesMask.Value));
                    if (src.IsForwardedBySurvivor)
                        inqSrcs.Add(new ForwardedBySurvivor(src.ForwardedBySurvivor ?? ""));
                    if (src.IsForwardedByPerson)
                        inqSrcs.Add(new ForwardedByPerson(src.ForwardedByPerson ?? ""));
                    if (src.IsForwardedByOrganization)
                        inqSrcs.Add(new ForwardedByOrganization(src.ForwardedByOrgannization ?? ""));

                    var citizenship = ctx.Mapper.Map<CitizenshipRecord>(src.Citizenship);
                    var domicile = ctx.Mapper.Map<DomicileRecord>(src.Domicile);
                    var hasChildren = ctx.Mapper.Map<ChildrenRecord>(src.HasChildren);
                    var educations = src.EducationLevel.Select(ctx.Mapper.Map<EducationLevelRecord>);
                    var specialities = src.Specialities.Select(ctx.Mapper.Map<SpecialityRecord>);

                    var migrationStatus = ctx.Mapper.Map<MigrationStatusRecord>(src.MigrationStatus);
                    var registrationStatus = ctx.Mapper.Map<RegistrationStatusRecord>(src.RegistrationStatus);

                    var vulns = new List<Vulnerability>();
                    if(src.HasAddiction) vulns.Add(new Addiction(src.AddictionKind ?? ""));
                    if(src.ChildhoodViolence) vulns.Add(new ChildhoodViolence());
                    if(src.Homelessness) vulns.Add(new Homelessness());
                    if(src.Migration) vulns.Add(new Migration());
                    if(src.OrphanageExperience) vulns.Add(new OrphanageExperience());
                    if(src.HasOtherVulnerability) vulns.Add(new Other(src.OtherVulnerabilityDetails??""));
                    if(src.HealthStatusVulnerabilityMask != 0)
                        vulns.Add(new HealthStatus((HealthStatus.HealthStatusVulnerabilityType)src.HealthStatusVulnerabilityMask, src.OtherHealthStatusVulnerabilityDetail));

                    return new Inquiry(src.ID, src.IsDeleted, src.Created, src.LastEdit,
                    src.DocumentDate, survivor,
                    src.IsJuvenile, inqSrcs, citizenship, domicile, hasChildren,
                    educations, specialities, src.WorkingExperience, vulns, migrationStatus, registrationStatus);
                });
        }
        private void MapBase()
        {
            CreateMap<LifeSituationDocumentDAL, LifeSituationDocument>();
        }
    }
}

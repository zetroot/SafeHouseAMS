using System;
using System.Collections.Generic;
using System.Text.Json;
using AutoMapper;
using FluentAssertions;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.Documents;
using SafeHouseAMS.BizLayer.LifeSituations.InquirySources;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.BizLayer.LifeSituations.Vulnerabilities;
using SafeHouseAMS.DataLayer.MapperProfiles;
using SafeHouseAMS.DataLayer.Models.LifeSituations;
using SafeHouseAMS.DataLayer.Models.Survivors;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.DataLayer.MapperProfiles
{
    public class LifeSituationsMappingProfileTests
    {
        private Mapper BuildMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(LifeSituationsDocumentMapping)));
            return new(cfg);
        }

        [Fact,UnitTest]
        public void Mapper_MapsInquiry_Dal2Bl()
        {
            //arrange
            var survivorDal = new SurvivorDAL{ID = Guid.NewGuid(), IsDeleted = true, Name = "name", Sex = 0};
            var citizenshipRecord = new CitizenshipRecord(Guid.NewGuid(), "citizenship");
            var citizenshipRecordDAL = new CitizenshipRecordDAL
            {
                ID = citizenshipRecord.ID, Content = JsonSerializer.Serialize(citizenshipRecord)
            };

            var domicile = new DomicileRecord(Guid.NewGuid(), "place", DomicileRecord.PlaceKind.OwnHome, "",
            false, true, false, null, false, null,
            false, null, true, "other");
            var domicileRecordDAL = new DomicileRecordDAL
            {
                ID = domicile.ID, Content = JsonSerializer.Serialize(domicile)
            };

            var childrenRecord = new ChildrenRecord(Guid.NewGuid(), true, "children details");
            var childrenRecordDAL = new ChildrenRecordDAL
            {
                ID = childrenRecord.ID, Content = JsonSerializer.Serialize(childrenRecord)
            };
            var eduRec1 = new EducationLevelRecord(Guid.NewGuid(), EducationLevelRecord.EduLevel.High, "high");
            var eduRec2 = new EducationLevelRecord(Guid.NewGuid(), EducationLevelRecord.EduLevel.Courses, "course");
            var eduRec1Dal = new EducationLevelRecordDAL {ID = eduRec1.ID, Content = JsonSerializer.Serialize(eduRec1)};
            var eduRec2Dal = new EducationLevelRecordDAL {ID = eduRec2.ID, Content = JsonSerializer.Serialize(eduRec2)};

            var specRec1 = new SpecialityRecord(Guid.NewGuid(),"spec1");
            var specRec2 = new SpecialityRecord(Guid.NewGuid(), "spec2");
            var specRec1Dal = new SpecialityRecordDAL() {ID = specRec1.ID, Content = JsonSerializer.Serialize(specRec1)};
            var specRec2Dal = new SpecialityRecordDAL() {ID = specRec2.ID, Content = JsonSerializer.Serialize(specRec2)};

            var created = new DateTime(1999, 05, 09);
            var lastEdit = new DateTime(2010, 05, 09);
            var documentDate = new DateTime(1998, 08, 02);

            var inquiryChannel = SelfInquiry.InquiryChannel.Whatsapp | SelfInquiry.InquiryChannel.Telegramm;
            const string workingExperience = "workingExp";
            const string forwardedBySurvivor = "survivor";
            const string forwardedByPerson = "person";
            const string forwardedByOrgannization = "organization";
            var healthStatusVulnerabilityType = HealthStatus.HealthStatusVulnerabilityType.HepatitisB | HealthStatus.HealthStatusVulnerabilityType.HepatitisC | HealthStatus.HealthStatusVulnerabilityType.Other;
            const string otherVulnerabilityDetails = "other vulnerability";
            const string addictionKind = "addiction";
            const string otherHealthStatusVulnerabilityDetail = "other health";

            var inquiryDal = new InquiryDAL
            {
                ID = Guid.NewGuid(),
                IsDeleted = true,
                Created = created,
                LastEdit = lastEdit,
                DocumentDate = documentDate,
                Survivor = survivorDal,
                SurvivorID = survivorDal.ID,
                IsJuvenile = true,
                IsSelfInquiry = true,
                SelfInquirySourcesMask = (int)inquiryChannel,
                IsForwardedBySurvivor = true, ForwardedBySurvivor = forwardedBySurvivor,
                IsForwardedByPerson = true, ForwardedByPerson = forwardedByPerson,
                IsForwardedByOrganization = true, ForwardedByOrgannization = forwardedByOrgannization,
                AllRecords = new BaseRecordDAL[]{citizenshipRecordDAL, domicileRecordDAL, childrenRecordDAL, eduRec1Dal, eduRec2Dal, specRec1Dal, specRec2Dal },
                WorkingExperience = workingExperience,
                HasAddiction = true, AddictionKind = addictionKind,
                ChildhoodViolence = true, Homelessness = true, Migration = true, OrphanageExperience = true,
                HasOtherVulnerability = true, OtherVulnerabilityDetails = otherVulnerabilityDetails,
                HealthStatusVulnerabilityMask = (int)healthStatusVulnerabilityType,
                OtherHealthStatusVulnerabilityDetail = otherHealthStatusVulnerabilityDetail
            };
            var sut = BuildMapper();

            //act
            var result = sut.Map<Inquiry>(inquiryDal);

            //assert
            result.ID.Should().Be(inquiryDal.ID);
            result.IsDeleted.Should().BeTrue();
            result.Created.Should().Be(created);
            result.LastEdit.Should().Be(lastEdit);
            result.DocumentDate.Should().Be(documentDate);

            result.Survivor.Should().NotBeNull();
            result.Survivor.ID.Should().Be(survivorDal.ID);

            result.IsJuvenile.Should().BeTrue();
            result.InquirySources.Should().HaveCount(4)
                .And.ContainEquivalentOf(new SelfInquiry(inquiryChannel))
                .And.ContainEquivalentOf(new ForwardedBySurvivor(forwardedBySurvivor))
                .And.ContainEquivalentOf(new ForwardedByPerson(forwardedByPerson))
                .And.ContainEquivalentOf(new ForwardedByOrganization(forwardedByOrgannization));

            result.Citizenship.Should().BeEquivalentTo(citizenshipRecord);
            result.Domicile.Should().BeEquivalentTo(domicile);
            result.HasChildren.Should().BeEquivalentTo(childrenRecord);
            result.EducationLevel.Should().HaveCount(2)
                .And.ContainEquivalentOf(eduRec1)
                .And.ContainEquivalentOf(eduRec2);
            result.Specialities.Should().HaveCount(2)
                .And.ContainEquivalentOf(specRec1)
                .And.ContainEquivalentOf(specRec2);

            result.WorkingExperience.Should().Be(workingExperience);

            result.VulnerabilityFactors.Should().HaveCount(7)
                .And.ContainEquivalentOf(new Addiction(addictionKind))
                .And.ContainSingle(x => x is ChildhoodViolence)
                .And.ContainSingle(x => x is Homelessness)
                .And.ContainSingle(x => x is Migration)
                .And.ContainSingle(x => x is OrphanageExperience)
                .And.ContainEquivalentOf(new Other(otherVulnerabilityDetails))
                .And.ContainEquivalentOf(new HealthStatus(healthStatusVulnerabilityType, otherHealthStatusVulnerabilityDetail));

        }


        [Fact, UnitTest]
        public void Mapper_MapsCitizenshipChange_Dal2Bl()
        {
            //arrange
            var survivorDal = new SurvivorDAL{ID = Guid.NewGuid(), IsDeleted = true, Name = "name", Sex = 0};
            const string citizenship = "citizenship";
            var citizenshipRec = new CitizenshipRecord(Guid.NewGuid(), citizenship);
            var citizenshipRecDal = new CitizenshipRecordDAL
            {
                ID = citizenshipRec.ID,
                Content = JsonSerializer.Serialize(citizenshipRec)
            };

            var src = new CitizenshipChangeDAL
            {
                ID = Guid.NewGuid(),
                Created = DateTime.Now - TimeSpan.FromDays(7),
                LastEdit = DateTime.Now - TimeSpan.FromDays(3),
                DocumentDate = DateTime.Now - TimeSpan.FromDays(5),
                IsDeleted = false,
                Survivor = survivorDal,
                SurvivorID = survivorDal.ID,
                AllRecords = new List<BaseRecordDAL> { citizenshipRecDal }
            };

            var sut = BuildMapper();

            //act
            var result = sut.Map<LifeSituationDocument>(src);

            //assert
            result.Should().BeOfType<SingleRecordUpdate<CitizenshipRecord>>();
            var document = (result as SingleRecordUpdate<CitizenshipRecord>)!;

            document.ID.Should().Be(src.ID);
            document.Created.Should().Be(src.Created);
            document.LastEdit.Should().Be(src.LastEdit);
            document.DocumentDate.Should().Be(src.DocumentDate);
            document.IsDeleted.Should().Be(src.IsDeleted);
            document.Survivor.Should().NotBeNull();
            document.Survivor.ID.Should().Be(survivorDal.ID);
            document.Record.Should().NotBeNull();
            document.Record?.ID.Should().Be(citizenshipRec.ID);
            document.Record?.Citizenship.Should().Be(citizenship);
        }

        [Fact, UnitTest]
        public void Mapper_MapsEducationLevelUpdate_Dal2Bl()
        {
            //arrange
            var survivorDal = new SurvivorDAL{ID = Guid.NewGuid(), IsDeleted = true, Name = "name", Sex = 0};
            const string details = "details";
            var eduRec1 = new EducationLevelRecord(Guid.NewGuid(), EducationLevelRecord.EduLevel.Courses, details);
            var eduRec2 = new EducationLevelRecord(Guid.NewGuid(), EducationLevelRecord.EduLevel.Courses, details);
            var eduRecDal1 = new EducationLevelRecordDAL()
            {
                ID = eduRec1.ID,
                Content = JsonSerializer.Serialize(eduRec1)
            };
            var eduRecDal2 = new EducationLevelRecordDAL()
            {
                ID = eduRec2.ID,
                Content = JsonSerializer.Serialize(eduRec2)
            };

            var src = new EducationLevelUpdateDAL()
            {
                ID = Guid.NewGuid(),
                Created = DateTime.Now - TimeSpan.FromDays(7),
                LastEdit = DateTime.Now - TimeSpan.FromDays(3),
                DocumentDate = DateTime.Now - TimeSpan.FromDays(5),
                IsDeleted = false,
                Survivor = survivorDal,
                SurvivorID = survivorDal.ID,
                AllRecords = new List<BaseRecordDAL> { eduRecDal1, eduRecDal2 }
            };

            var sut = BuildMapper();

            //act
            var result = sut.Map<LifeSituationDocument>(src);

            //assert
            result.Should().BeOfType<MultiRecordsUpdate<EducationLevelRecord>>();
            var document = (result as MultiRecordsUpdate<EducationLevelRecord>)!;

            document.ID.Should().Be(src.ID);
            document.Created.Should().Be(src.Created);
            document.LastEdit.Should().Be(src.LastEdit);
            document.DocumentDate.Should().Be(src.DocumentDate);
            document.IsDeleted.Should().Be(src.IsDeleted);
            document.Survivor.Should().NotBeNull();
            document.Survivor.ID.Should().Be(survivorDal.ID);
            document.Records.Should().NotBeNull()
                .And.ContainEquivalentOf(eduRec1)
                .And.ContainEquivalentOf(eduRec2);
        }
    }
}

using System;
using System.Text.Json;
using AutoMapper;
using FluentAssertions;
using SafeHouseAMS.BizLayer.LifeSituations;
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

            var domicile = new DomicileRecord(Guid.NewGuid(), "place", DomicileRecord.PlaceKind.OwnHome,
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
                Citizenship = citizenshipRecordDAL,
                Domicile = domicileRecordDAL,
                HasChildren = childrenRecordDAL,
                EducationLevel = new (){eduRec1Dal, eduRec2Dal},
                Specialities = new(){specRec1Dal, specRec2Dal},
                WorkingExperience = workingExperience,
                HasAddiction = true, AddictionKind = addictionKind,
                ChildhoodViolence = true, Homelessness = true, Mirgation = true, OrphanageExperience = true,
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
                .And.Contain(new SelfInquiry(inquiryChannel))
                .And.Contain(new ForwardedBySurvivor(forwardedBySurvivor))
                .And.Contain(new ForwardedByPerson(forwardedByPerson))
                .And.Contain(new ForwardedByOrganization(forwardedByOrgannization));
            
            result.Citizenship.Should().Be(citizenshipRecord);
            result.Domicile.Should().Be(domicile);
            result.HasChildren.Should().Be(childrenRecord);
            result.EducationLevel.Should().HaveCount(2)
                .And.Contain(eduRec1)
                .And.Contain(eduRec2);
            result.Specialities.Should().HaveCount(2)
                .And.Contain(specRec1)
                .And.Contain(specRec2);

            result.WorkingExperience.Should().Be(workingExperience);

            result.VulnerabilityFactors.Should().HaveCount(7)
                .And.Contain(new Addiction(addictionKind))
                .And.Contain(new ChildhoodViolence())
                .And.Contain(new Homelessness())
                .And.Contain(new Migration())
                .And.Contain(new OrphanageExperience())
                .And.Contain(new Other(otherVulnerabilityDetails))
                .And.Contain(new HealthStatus(healthStatusVulnerabilityType, otherHealthStatusVulnerabilityDetail));

        }
    }
}
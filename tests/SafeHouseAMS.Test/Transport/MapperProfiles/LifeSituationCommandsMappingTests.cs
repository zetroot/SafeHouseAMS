using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.BizLayer.LifeSituations.Commands;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.Transport.MapperProfiles;

namespace SafeHouseAMS.Test.Transport.MapperProfiles
{
    public class LifeSituationCommandsMappingTests
    {
        private Mapper BuildMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(LifeSituationsCommandsMappingProfile).Assembly));
            return new(cfg);
        }

        [Property]
        public void AddEducationLevel_RoundTrip_DoesNotChanges()
        {
            Arb.Register<NotNullStringsGenerators>();

            var mapper = BuildMapper();
            Prop.ForAll<AddEducationLevel>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.Commands.AddEducationLevel>(src);
                var result = mapper.Map<AddEducationLevel>(dto);

                result.Should().BeEquivalentTo(src);
            }).QuickCheckThrowOnFailure();
        }

        [Property]
        public void AddSpeciality_RoundTrip_DoesNotChanges()
        {
            Arb.Register<NotNullStringsGenerators>();

            var mapper = BuildMapper();
            Prop.ForAll<AddSpeciality>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.Commands.AddSpeciality>(src);
                var result = mapper.Map<AddSpeciality>(dto);

                result.Should().BeEquivalentTo(src);
            }).QuickCheckThrowOnFailure();
        }

        [Property]
        public void CreateInquiry_RoundTrip_DoesNotChanges()
        {
            Arb.Register<NotNullStringsGenerators>();
            Arb.Register<DateTimeGenerators>();
            Arb.Register<InquiryGenerators>();

            var mapper = BuildMapper();
            Prop.ForAll<CreateInquiry>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.Commands.CreateInquiry>(src);
                var result = mapper.Map<CreateInquiry>(dto);

                result.Should().BeEquivalentTo(src);
            }).QuickCheckThrowOnFailure();
        }

        [Property]
        public void SetChildren_RoundTrip_DoesNotChanges()
        {
            Arb.Register<NotNullStringsGenerators>();

            var mapper = BuildMapper();
            Prop.ForAll<SetChildren>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.Commands.SetChildren>(src);
                var result = mapper.Map<SetChildren>(dto);

                result.Should().BeEquivalentTo(src);
            }).QuickCheckThrowOnFailure();
        }

        [Property]
        public void SetDomicile_RoundTrip_DoesNotChanges()
        {
            Arb.Register<NotNullStringsGenerators>();

            var mapper = BuildMapper();
            Prop.ForAll<SetDomicile>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.Commands.SetDomicile>(src);
                var result = mapper.Map<SetDomicile>(dto);

                result.Should().BeEquivalentTo(src);
            }).QuickCheckThrowOnFailure();
        }


        [Property]
        public void SetVulnerabilities_RoundTrip_DoesNotChanges()
        {
            Arb.Register<NotNullStringsGenerators>();
            Arb.Register<InquiryGenerators>();

            var mapper = BuildMapper();
            Prop.ForAll<SetVulnerabilities>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.Commands.SetVulnerabilities>(src);
                var result = mapper.Map<SetVulnerabilities>(dto);

                result.Should().BeEquivalentTo(src);
            }).QuickCheckThrowOnFailure();
        }

        [Property]
        public void SetWorkingExperience_RoundTrip_DoesNotChanges()
        {
            Arb.Register<NotNullStringsGenerators>();

            var mapper = BuildMapper();
            Prop.ForAll<SetWorkingExperience>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.Commands.SetWorkingExperience>(src);
                var result = mapper.Map<SetWorkingExperience>(dto);

                result.Should().BeEquivalentTo(src);
            }).QuickCheckThrowOnFailure();
        }

        [Property]
        public void SetMigrationStatus_RoundTrip_DoesNotChanges()
        {
            Arb.Register<NotNullStringsGenerators>();

            var mapper = BuildMapper();
            Prop.ForAll<SetMigrationStatus>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.Commands.SetMigrationStatus>(src);
                var result = mapper.Map<SetMigrationStatus>(dto);

                result.Should().BeEquivalentTo(src);
            }).QuickCheckThrowOnFailure();
        }

        [Property]
        public void SetRegistrationStatus_RoundTrip_DoesNotChanges()
        {
            Arb.Register<NotNullStringsGenerators>();

            var mapper = BuildMapper();
            Prop.ForAll<SetRegistrationStatus>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.Commands.SetRegistrationStatus>(src);
                var result = mapper.Map<SetRegistrationStatus>(dto);

                result.Should().BeEquivalentTo(src);
            }).QuickCheckThrowOnFailure();
        }

        [Property]
        public void LifeSituationDocumentCommand_RoundTrip_DoesNotChanges()
        {
            Arb.Register<NotNullStringsGenerators>();
            Arb.Register<InquiryGenerators>();

            var genList = new List<Gen<LifeSituationDocumentCommand>>
            {
                Arb.From<AddEducationLevel>().Generator.Select(x => x as LifeSituationDocumentCommand),
                Arb.From<SetMigrationStatus>().Generator.Select(x => x as LifeSituationDocumentCommand),
                Arb.From<SetRegistrationStatus>().Generator.Select(x => x as LifeSituationDocumentCommand),
                Arb.From<AddSpeciality>().Generator.Select(x => x as LifeSituationDocumentCommand),
                Arb.From<CreateInquiry>().Generator.Select(x => x as LifeSituationDocumentCommand),
                Arb.From<SetCitizenship>().Generator.Select(x => x as LifeSituationDocumentCommand),
                Arb.From<SetChildren>().Generator.Select(x => x as LifeSituationDocumentCommand),
                Arb.From<SetDomicile>().Generator.Select(x => x as LifeSituationDocumentCommand),
                Arb.From<SetVulnerabilities>().Generator.Select(x => x as LifeSituationDocumentCommand),
                Arb.From<SetWorkingExperience>().Generator.Select(x => x as LifeSituationDocumentCommand),
                Arb.From<DeleteDocument>().Generator.Select(x => x as LifeSituationDocumentCommand),

                Arb.From<CreateRecordUpdateDocument<ChildrenRecord>>().Generator.Select(x => x as LifeSituationDocumentCommand),
                Arb.From<CreateRecordUpdateDocument<CitizenshipRecord>>().Generator.Select(x => x as LifeSituationDocumentCommand),
                Arb.From<CreateRecordUpdateDocument<DomicileRecord>>().Generator.Select(x => x as LifeSituationDocumentCommand),
                Arb.From<CreateRecordUpdateDocument<EducationLevelRecord>>().Generator.Select(x => x as LifeSituationDocumentCommand),
                Arb.From<CreateRecordUpdateDocument<MigrationStatusRecord>>().Generator.Select(x => x as LifeSituationDocumentCommand),
                Arb.From<CreateRecordUpdateDocument<RegistrationStatusRecord>>().Generator.Select(x => x as LifeSituationDocumentCommand),
                Arb.From<CreateRecordUpdateDocument<SpecialityRecord>>().Generator.Select(x => x as LifeSituationDocumentCommand),
            };


            var mapper = BuildMapper();
            Prop.ForAll(Gen.OneOf(genList).ToArbitrary(),
            src =>
                {
                    var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand>(src);
                    var result = mapper.Map<LifeSituationDocumentCommand>(dto);

                    result.Should().BeEquivalentTo(src);
                })
                .QuickCheckThrowOnFailure();
        }

    }
}

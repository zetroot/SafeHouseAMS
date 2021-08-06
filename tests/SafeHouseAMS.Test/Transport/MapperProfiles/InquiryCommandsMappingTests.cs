using AutoMapper;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.BizLayer.LifeSituations.Commands;
using SafeHouseAMS.Transport.MapperProfiles;

namespace SafeHouseAMS.Test.Transport.MapperProfiles
{
    public class InquiryCommandsMappingTests
    {
        private Mapper BuildMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(InquiryCommandsMappingProfile).Assembly));
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
            Arb.Register<LocalDateTimeGenerators>();
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
            Arb.Register<InquiryGenerators>();

            var mapper = BuildMapper();
            Prop.ForAll<SetWorkingExperience>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.Commands.SetWorkingExperience>(src);
                var result = mapper.Map<SetWorkingExperience>(dto);

                result.Should().BeEquivalentTo(src);
            }).QuickCheckThrowOnFailure();
        }
    }
}

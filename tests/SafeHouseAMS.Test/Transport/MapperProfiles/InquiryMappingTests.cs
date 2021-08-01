using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.InquirySources;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.BizLayer.LifeSituations.Vulnerabilities;
using SafeHouseAMS.Transport.MapperProfiles;

namespace SafeHouseAMS.Test.Transport.MapperProfiles
{
    public class InquiryMappingTests
    {
        private Mapper BuildMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(InquiryMappingProfile).Assembly));
            return new(cfg);
        }

        [Property]
        public void Inquiry_RoundTrip_DoesNotChanges()
        {
            Arb.Register<NotNullStringsGenerators>();
            Arb.Register<LocalDateTimeGenerators>();
            Arb.Register<InquiryGenerators>();

            var mapper = BuildMapper();
            Prop.ForAll<Inquiry>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.Inquiry>(src);
                var result = mapper.Map<Inquiry>(dto);

                result.Should().BeEquivalentTo(src, opt => opt.RespectingRuntimeTypes());
            }).VerboseCheckThrowOnFailure();
        }
    }

    public class InquiryGenerators
    {
        private static Gen<Vulnerability?> addictionGen = Arb.From<Addiction>().Generator.Select(x => x as Vulnerability).Zip(Arb.From<bool>().Generator).Select(x => x.Item2? x.Item1 : null);
        private static Gen<Vulnerability?> childhoodViolenceGen = Arb.From<ChildhoodViolence>().Generator.Select(x => x as Vulnerability).Zip(Arb.From<bool>().Generator).Select(x => x.Item2? x.Item1 : null);
        private static Gen<Vulnerability?> homelessGen = Arb.From<Homelessness>().Generator.Select(x => x as Vulnerability).Zip(Arb.From<bool>().Generator).Select(x => x.Item2? x.Item1 : null);
        private static Gen<Vulnerability?> migrationGen = Arb.From<Migration>().Generator.Select(x => x as Vulnerability).Zip(Arb.From<bool>().Generator).Select(x => x.Item2? x.Item1 : null);
        private static Gen<Vulnerability?> orphExpGen = Arb.From<OrphanageExperience>().Generator.Select(x => x as Vulnerability).Zip(Arb.From<bool>().Generator).Select(x => x.Item2? x.Item1 : null);
        private static Gen<Vulnerability?> otherGen = Arb.From<Other>().Generator.Select(x => x as Vulnerability).Zip(Arb.From<bool>().Generator).Select(x => x.Item2? x.Item1 : null);
        private static Gen<Vulnerability?> healthGen = Arb.From<HealthStatus>().Generator.Select(x => x as Vulnerability).Zip(Arb.From<bool>().Generator).Select(x => x.Item2? x.Item1 : null);

        private static Gen<IInquirySource> selfInquriyGen = Arb.From<SelfInquiry>().Generator.Select(x => x as IInquirySource);
        private static Gen<IInquirySource> forwardSurvivorGen = Arb.From<ForwardedBySurvivor>().Generator.Select(x => x as IInquirySource);
        private static Gen<IInquirySource> forwardPersonGen = Arb.From<ForwardedByPerson>().Generator.Select(x => x as IInquirySource);
        private static Gen<IInquirySource> forwardOrganizationGen = Arb.From<ForwardedByOrganization>().Generator.Select(x => x as IInquirySource);

        public static Arbitrary<IEnumerable<IInquirySource>> InquirySourcesArb => Gen
            .OneOf(selfInquriyGen, forwardSurvivorGen, forwardPersonGen, forwardOrganizationGen).ListOf().Select(x => x.AsEnumerable()).ToArbitrary();
        
        public static Arbitrary<IEnumerable<Vulnerability>> Vulnerabilities => Gen
            .Sequence(addictionGen, childhoodViolenceGen, homelessGen, migrationGen, orphExpGen, otherGen, healthGen)
            .Select(x => x.Where(y => y is not null).Select(y => y!))
            .ToArbitrary();

        public static Arbitrary<IEnumerable<EducationLevelRecord>> EducationLevels => Arb.From<EducationLevelRecord>().Generator.ListOf().Select(x => x.AsEnumerable()).ToArbitrary();
        public static Arbitrary<IEnumerable<SpecialityRecord>> Specialities => Arb.From<SpecialityRecord>().Generator.ListOf().Select(x => x.AsEnumerable()).ToArbitrary();
        public static Arbitrary<IEnumerable<DomicileRecord>> Domiciles => Arb.From<DomicileRecord>().Generator.ListOf().Select(x => x.AsEnumerable()).ToArbitrary();
        public static Arbitrary<IEnumerable<ChildrenRecord>> Childrens => Arb.From<ChildrenRecord>().Generator.ListOf().Select(x => x.AsEnumerable()).ToArbitrary();
        public static Arbitrary<IEnumerable<CitizenshipRecord>> Citizenship => Arb.From<CitizenshipRecord>().Generator.ListOf().Select(x => x.AsEnumerable()).ToArbitrary();
    }
}
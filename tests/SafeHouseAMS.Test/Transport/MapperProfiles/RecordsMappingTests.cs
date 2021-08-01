using System.Linq;
using AutoMapper;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.BizLayer.LifeSituations.InquirySources;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.Transport.MapperProfiles;

namespace SafeHouseAMS.Test.Transport.MapperProfiles
{
    public class RecordsMappingTests
    {
        private Mapper BuildMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(RecordsMappingProfile).Assembly));
            return new(cfg);
        }

        [Property]
        public void ChildrenRecord_RoundTrip_DoesNotChanges()
        {
            var mapper = BuildMapper();
            Arb.Register<NotNullStringsGenerators>();
            Prop.ForAll<ChildrenRecord>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.Records.ChildrenRecord>(src);
                var result = mapper.Map<ChildrenRecord>(dto);
                
                result.Should().BeEquivalentTo(src);
            }).VerboseCheckThrowOnFailure();
        }
        
        [Property]
        public void CitizenshipRecord_RoundTrip_DoesNotChanges()
        {
            var mapper = BuildMapper();
            Arb.Register<NotNullStringsGenerators>();
            Prop.ForAll<CitizenshipRecord>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.Records.CitizenshipRecord>(src);
                var result = mapper.Map<CitizenshipRecord>(dto);
                
                result.Should().BeEquivalentTo(src);
            }).VerboseCheckThrowOnFailure();
        }
        
        [Property]
        public void DomicileRecord_RoundTrip_DoesNotChanges()
        {
            var mapper = BuildMapper();
            Arb.Register<NotNullStringsGenerators>();
            Prop.ForAll<DomicileRecord>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.Records.DomicileRecord>(src);
                var result = mapper.Map<DomicileRecord>(dto);
                
                result.Should().BeEquivalentTo(src);
            }).VerboseCheckThrowOnFailure();
        }
        
        [Property]
        public void EducationLevelRecord_RoundTrip_DoesNotChanges()
        {
            var mapper = BuildMapper();
            Arb.Register<NotNullStringsGenerators>();
            Prop.ForAll<EducationLevelRecord>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.Records.EducationLevelRecord>(src);
                var result = mapper.Map<EducationLevelRecord>(dto);
                
                result.Should().BeEquivalentTo(src);
            }).VerboseCheckThrowOnFailure();
        }

        [Property]
        public void SpecialityRecord_RoundTrip_DoesNotChanges()
        {
            var mapper = BuildMapper();
            Arb.Register<NotNullStringsGenerators>();
            Prop.ForAll<SpecialityRecord>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.Records.SpecialityRecord>(src);
                var result = mapper.Map<SpecialityRecord>(dto);
                
                result.Should().BeEquivalentTo(src);
            }).VerboseCheckThrowOnFailure();
        }
    }
}
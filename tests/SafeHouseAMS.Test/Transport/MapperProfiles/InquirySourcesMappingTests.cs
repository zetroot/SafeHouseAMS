using System.Linq;
using AutoMapper;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.BizLayer.LifeSituations.InquirySources;
using SafeHouseAMS.Transport.MapperProfiles;

namespace SafeHouseAMS.Test.Transport.MapperProfiles
{
    public class InquirySourcesMappingTests
    {
        private Mapper BuildMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(InquiryMappingProfile).Assembly));
            return new(cfg);
        }

        [Property]
        public void SelfInquiry_RoundTrip_DoesNotChanges()
        {
            var mapper = BuildMapper();
            Prop.ForAll<SelfInquiry>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.SelfInquiry>(src);
                var result = mapper.Map<SelfInquiry>(dto);
                
                result.Should().BeEquivalentTo(src);
            }).VerboseCheckThrowOnFailure();
        }
        
        [Property]
        public void ForwardedBySurvivor_RoundTrip_DoesNotChanges()
        {
            var mapper = BuildMapper();
            Arb.Register<NotNullStringsGenerators>();
            Prop.ForAll<ForwardedBySurvivor>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.ForwardedBySurvivor>(src);
                var result = mapper.Map<ForwardedBySurvivor>(dto);
                
                result.Should().BeEquivalentTo(src);
            }).VerboseCheckThrowOnFailure();
        }
        
        [Property]
        public void ForwardedByPerson_RoundTrip_DoesNotChanges()
        {
            var mapper = BuildMapper();
            Arb.Register<NotNullStringsGenerators>();
            Prop.ForAll<ForwardedByPerson>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.ForwardedByPerson>(src);
                var result = mapper.Map<ForwardedByPerson>(dto);
                
                result.Should().BeEquivalentTo(src);
            }).VerboseCheckThrowOnFailure();
        }
        
        [Property]
        public void ForwardedByOrganization_RoundTrip_DoesNotChanges()
        {
            var mapper = BuildMapper();
            Arb.Register<NotNullStringsGenerators>();
            Prop.ForAll<ForwardedByOrganization>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.ForwardedByOrganization>(src);
                var result = mapper.Map<ForwardedByOrganization>(dto);
                
                result.Should().BeEquivalentTo(src);
            }).VerboseCheckThrowOnFailure();
        }

        [Property]
        public void IInquirySourceCollection_RoundTrip_DoesNotChanges()
        {
            Arb.Register<NotNullStringsGenerators>();
            var selfInquriyGen = Arb.From<SelfInquiry>().Generator.Select(x => x as IInquirySource);
            var forwardSurvivorGen = Arb.From<ForwardedBySurvivor>().Generator.Select(x => x as IInquirySource);
            var forwardPersonGen = Arb.From<ForwardedByPerson>().Generator.Select(x => x as IInquirySource);
            var forwardOrganizationGen = Arb.From<ForwardedByOrganization>().Generator.Select(x => x as IInquirySource);

            var inquirySourcesArb = Gen.OneOf(selfInquriyGen, forwardSurvivorGen, forwardPersonGen, forwardOrganizationGen).ListOf().ToArbitrary();
            
            var mapper = BuildMapper();
            

            Prop.ForAll(inquirySourcesArb, src =>
            {
                var dto = src.Select(mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.InquirySource>);
                var result = dto.Select(mapper.Map<IInquirySource>);

                result.Should().BeEquivalentTo(src, opt => opt.RespectingRuntimeTypes());
            }).VerboseCheckThrowOnFailure();
        } 
    }
}
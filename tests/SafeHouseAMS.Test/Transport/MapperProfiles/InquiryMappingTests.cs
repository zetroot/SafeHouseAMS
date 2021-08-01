using AutoMapper;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.BizLayer.LifeSituations;
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

}
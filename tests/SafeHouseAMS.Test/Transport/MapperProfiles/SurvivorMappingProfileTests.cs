using AutoMapper;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.BizLayer.Survivors;
using SafeHouseAMS.Transport.MapperProfiles;

namespace SafeHouseAMS.Test.Transport.MapperProfiles
{
    public class SurvivorMappingProfileTests
    {
        private Mapper BuildMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(SurvivorMappingProfile).Assembly));
            return new(cfg);
        }

        [Property]
        public void RoundMapping_ReturnsSameSurvivor()
        {
            var mapper = BuildMapper();
            Arb.Register<LocalDateTimeGenerators>();
            Arb.Register<NotNullStringsGenerators>();
            Prop.ForAll<Survivor>(src =>
            {
                var survivorDto = mapper.Map<Backend.Protos.Models.Survivors.Survivor>(src);
                var result = mapper.Map<Survivor>(survivorDto);
                
                result.Should().BeEquivalentTo(src);
            }).VerboseCheckThrowOnFailure();
        }
        
        public class NotNullStringsGenerators
        {
            public static Arbitrary<string> String() =>
                Arb.From(Arb.Default.NonEmptyString().Generator.Select(x => x.Item));
        }
    }

}
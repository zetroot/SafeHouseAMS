using AutoMapper;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.BizLayer.Survivors;
using SafeHouseAMS.BizLayer.Survivors.Commands;
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
                var survivorDto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.Survivors.Survivor>(src);
                var result = mapper.Map<Survivor>(survivorDto);
                
                result.Should().BeEquivalentTo(src);
            }).VerboseCheckThrowOnFailure();
        }
        
        [Property]
        public void RoundMapping_ReturnsSameCreateSurvivorCommand()
        {
            var mapper = BuildMapper();
            Arb.Register<LocalDateTimeGenerators>();
            Arb.Register<NotNullStringsGenerators>();
            Prop.ForAll<CreateSurvivor>(src =>
            {
                var commandDto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.Survivors.CreateSurvivor>(src);
                var result = mapper.Map<CreateSurvivor>(commandDto);
                
                result.Should().BeEquivalentTo(src);
            }).VerboseCheckThrowOnFailure();
        }
        
    }

}
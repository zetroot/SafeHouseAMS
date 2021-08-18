using AutoMapper;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.Transport.MapperProfiles;

namespace SafeHouseAMS.Test.Transport.MapperProfiles
{
    public class RecordHistoryItemMappingProfileTests
    {
        private Mapper BuildMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(RecordHistoryItemMappingProfile).Assembly));
            return new(cfg);
        }

        [Property]
        public void RoundTrip_DoesnotChangesItem()
        {
            var mapper = BuildMapper();
            Arb.Register<LocalDateTimeGenerators>();
            Prop.ForAll<RecordHistoryItem>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.RecordHistoryItem>(src);
                var result = mapper.Map<RecordHistoryItem>(dto);

                result.Should().BeEquivalentTo(src);
            }).QuickCheckThrowOnFailure();
        }
    }
}

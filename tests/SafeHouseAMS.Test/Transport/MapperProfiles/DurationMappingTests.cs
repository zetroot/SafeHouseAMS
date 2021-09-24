using System;
using AutoMapper;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Google.Protobuf.WellKnownTypes;
using SafeHouseAMS.Transport.MapperProfiles;

namespace SafeHouseAMS.Test.Transport.MapperProfiles
{
    public class DurationMappingTests
    {
        private Mapper BuildMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(DurationMappingProfile)));
            return new(cfg);
        }

        [Property]
        public void RoundMapping_ReturnsOriginalTimeSpan()
        {
            var mapper = BuildMapper();

            var validTimestamps = Arb.From<TimeSpan>().Generator
                .Where(x =>
                    x > TimeSpan.FromSeconds(Duration.MinSeconds) &&
                    x < TimeSpan.FromSeconds(Duration.MaxSeconds))
                .ToArbitrary();
            Prop
                .ForAll( validTimestamps, src =>
                {
                    var dto = mapper.Map<Duration>(src);
                    var result = mapper.Map<TimeSpan>(dto);

                    result.Should().Be(src);
                })
                .QuickCheckThrowOnFailure();
        }
    }
}
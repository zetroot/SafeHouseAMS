using System;
using AutoMapper;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Google.Protobuf.WellKnownTypes;
using SafeHouseAMS.Transport.MapperProfiles;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.Transport.MapperProfiles
{
    public class TimestampMappingTests
    {
        private Mapper BuildMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(TimestampMappingProfile)));
            return new(cfg);
        }

        [Property]
        public void RoundMappig_ReturnsOriginalDateTime()
        {
            var mapper = BuildMapper();
            Arb.Register<LocalDateTimeGenerators>();
            Prop.ForAll<DateTime>(d =>
                {
                    var src = d.ToLocalTime();
                    var timestamp = mapper.Map<Timestamp>(src);
                    var resultedDt = mapper.Map<DateTime>(timestamp);
                    resultedDt.Should().Be(src);

                })
                .QuickCheckThrowOnFailure();
        }

        [Fact, UnitTest]
        public void Mapper_WhenTimeStampIsNull_MapsNullToDateTime()
        {
            //arrange
            Timestamp src = null!;
            //act
            var result = BuildMapper().Map<DateTime?>(src);
            //assert
            result.Should().BeNull();
        }

        [Fact, UnitTest]
        public void Mapper_WhenDateTimeIsNull_MapsNullToTimestamp()
        {
            //arrange
            DateTime? src = null;
            //act
            var result = BuildMapper().Map<Timestamp>(src);
            //assert
            result.Should().BeNull();
        }
    }

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

            Prop
                .ForAll<TimeSpan>(src =>
                {
                    var dto = Duration.FromTimeSpan(src); // mapper.Map<Duration>(src);
                    var result = dto.ToTimeSpan(); // mapper.Map<TimeSpan>(dto);

                    result.Should().Be(src);
                })
                .QuickCheckThrowOnFailure();
        }
    }
}

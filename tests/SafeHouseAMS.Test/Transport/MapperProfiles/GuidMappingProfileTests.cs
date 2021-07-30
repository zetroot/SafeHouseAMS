using System;
using AutoMapper;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.Backend.Protos.Models.Common;
using SafeHouseAMS.Transport.MapperProfiles;

namespace SafeHouseAMS.Test.Transport.MapperProfiles
{
    public class GuidMappingProfileTests
    {
        private Mapper BuildMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(TimestampMappingProfile)));
            return new(cfg);
        }

        [Property]
        public void RoundMapping_ReturnsSameValue()
        {
            var mapper = BuildMapper();
            Prop.ForAll<Guid>(g =>
            {
                var uuid = mapper.Map<UUID>(g);
                var roundGuid = mapper.Map<Guid>(g);
                roundGuid.Should().Be(g);
            }).VerboseCheckThrowOnFailure();
        }
    }
}
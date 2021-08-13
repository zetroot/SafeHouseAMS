using System;
using AutoMapper;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.Transport.MapperProfiles;
using SafeHouseAMS.Transport.Protos.Models.Common;

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
                var roundGuid = mapper.Map<Guid>(uuid);
                roundGuid.Should().Be(g);
            }).QuickCheckThrowOnFailure();
        }
    }
}

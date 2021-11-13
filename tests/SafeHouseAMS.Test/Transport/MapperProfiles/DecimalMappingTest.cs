using System;
using AutoMapper;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.Transport.MapperProfiles;
using SafeHouseAMS.Transport.Protos.Models.Common;
using Decimal=SafeHouseAMS.Transport.Protos.Models.Common.Decimal;

namespace SafeHouseAMS.Test.Transport.MapperProfiles;

public class DecimalMappingTest
{
    private Mapper BuildMapper()
    {
        var cfg = new MapperConfiguration(c => c.AddMaps(typeof(DecimalMappingProfile)));
        return new(cfg);
    }

    [Property]
    public void Decimal_RoundTrip_Unchanged()
    {
        var mapper = BuildMapper();
        Prop.ForAll<decimal>(x =>
        {
            var uuid = mapper.Map<Decimal>(x);
            var roundGuid = mapper.Map<decimal>(uuid);
            roundGuid.Should().Be(x);
        }).QuickCheckThrowOnFailure();
    }
}

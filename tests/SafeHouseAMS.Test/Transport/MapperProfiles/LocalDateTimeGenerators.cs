using System;
using FsCheck;

namespace SafeHouseAMS.Test.Transport.MapperProfiles
{
    public class LocalDateTimeGenerators 
    {
        public static Arbitrary<DateTime> DateTimeLocal() => 
            Arb.From(Gen.OneOf(Gen.Choose(int.MinValue, -1), Gen.Choose(1, int.MaxValue))
                .Select(x => DateTime.Now + TimeSpan.FromMilliseconds(x)));
    }
}
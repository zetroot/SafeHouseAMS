using System;
using FsCheck;
using Google.Protobuf.WellKnownTypes;

namespace SafeHouseAMS.Test
{
    public class DateTimeGenerators
    {
        public static Arbitrary<TimeSpan> ReducedTimeSpan() =>
            Gen.Choose(1, int.MaxValue).Select(x => TimeSpan.FromSeconds(x)).ToArbitrary();

        public static Arbitrary<DateTime> DateTimeLocal() =>
            Arb.From(Gen.OneOf(Gen.Choose(int.MinValue, -1), Gen.Choose(1, int.MaxValue))
                .Select(x => DateTime.Now + TimeSpan.FromMilliseconds(x)));

        // public static Arbitrary<DateTime> DateTimesLocal() =>
        //     Arb.From<DateTime>().Generator.Where(x => x.Kind == DateTimeKind.Local).ToArbitrary();
    }

    public class ValidTimeSpanGenerator
    {

    }
}

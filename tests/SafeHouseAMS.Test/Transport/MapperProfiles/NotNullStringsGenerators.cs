using FsCheck;

namespace SafeHouseAMS.Test.Transport.MapperProfiles
{
    public class NotNullStringsGenerators
    {
        public static Arbitrary<string> String() =>
            Arb.From(Arb.Default.NonEmptyString().Generator.Select(x => x.Item));
    }
}
using System.Collections.Generic;
using System.Linq;
using FsCheck;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

namespace SafeHouseAMS.Test.Transport.MapperProfiles
{
    public class RecordListGenerators
    {
        public static Arbitrary<IEnumerable<SpecialityRecord>> SpecList =>
            Arb.From(
            Arb.From<SpecialityRecord>().Generator
                .ListOf().Select(x => x.AsEnumerable())
            );

        public static Arbitrary<IEnumerable<EducationLevelRecord>> EduList =>
            Arb.From(
            Arb.From<EducationLevelRecord>().Generator
                .ListOf().Select(x => x.AsEnumerable())
            );
    }
}
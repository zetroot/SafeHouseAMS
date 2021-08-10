using System;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.Documents;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.BizLayer.Survivors;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.BizLayer.LifeSituations
{
    public class SignleRecordUpdateTests
    {
        [Fact, UnitTest]
        public void Ctor_WhenCitizenshipIsNull_Throws() =>
            Assert.Throws<ArgumentNullException>(() =>
                new SingleRecordUpdate<BaseRecord>(default, default, default, default,
                default,
                new Survivor(default, default, default, default,
                "name", default, default, default,
                default, default),
                null!));
    }
}

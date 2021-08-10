using System;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.Survivors;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.BizLayer.LifeSituations
{
    public class SpecialitiesChangeTests
    {
        [Fact, UnitTest]
        public void Ctor_WhenSpecialitiesRecordIsNull_Throws() =>
            Assert.Throws<ArgumentNullException>(() =>
                new SpecialitiesChange(default, default, default, default,
                default,
                new Survivor(default, default, default, default,
                "name", default, default, default,
                default, default),
                null!));
    }
}
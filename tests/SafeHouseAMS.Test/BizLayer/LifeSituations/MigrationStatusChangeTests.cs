using System;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.Survivors;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.BizLayer.LifeSituations
{
    public class MigrationStatusChangeTests
    {
        [Fact, UnitTest]
        public void Ctor_WhenMigrationStatusRecordIsNull_Throws() =>
            Assert.Throws<ArgumentNullException>(() =>
                new MigrationStatusChange(default, default, default, default,
                default,
                new Survivor(default, default, default, default,
                "name", default, default, default,
                default, default),
                null!));
    }
}
using System;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.BizLayer.LifeSituations.Records
{
    public class RegistrationStatusTests
    {
        [Fact, UnitTest]
        public void Ctor_WhenDetailsIsNull_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new RegistrationStatusRecord(default, null!));

        [Property]
        public void Ctor_WhenCalled_SetsProps()
        {
            Arb.Register<NotNullStringsGenerators>();
            Prop.ForAll<Guid, string>((id, details) =>
            {
                var sut = new RegistrationStatusRecord(id, details);
                sut.ID.Should().Be(id);
                sut.Details.Should().Be(details);
            }).QuickCheckThrowOnFailure();
        }
    }
}
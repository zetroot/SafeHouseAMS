using System;
using FluentAssertions;
using SafeHouseAMS.BizLayer.Survivors;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.BizLayer.Survivors
{
    public class SurvivorTests
    {
        [Fact, UnitTest]
        public void Ctor_Always_SetsProps()
        {
            //arrange
            var id = Guid.NewGuid();
            var createdAt = DateTime.Now - TimeSpan.FromHours(1);
            var lastEdit = DateTime.Now;
            const string name = "NAME";
            var num = 42;
            var sex = SexEnum.Other;
            const string otherSex = "OTHER";
            var dobAccurate = new DateTime(1970, 04, 12, 0, 0, 0);
            var dobCalc = new DateTime(1990, 12, 27, 0, 0, 0);

            //act
            var sut = new Survivor(id, false, createdAt, lastEdit, name, num, sex, otherSex, dobAccurate, dobCalc);

            //assert
            sut.ID.Should().Be(id);
            sut.IsDeleted.Should().BeFalse();
            sut.Created.Should().Be(createdAt);
            sut.LastEdit.Should().Be(lastEdit);
            sut.Name.Should().Be(name);
            sut.Num.Should().Be(num);
            sut.Sex.Should().Be(sex);
            sut.OtherSex.Should().Be(otherSex);
            sut.BirthDateAccurate.Should().Be(dobAccurate);
            sut.BirthDateCalculated.Should().Be(dobCalc);
        }

        [Theory, UnitTest]
        [InlineData(SexEnum.Female, null, "женский")]
        [InlineData(SexEnum.Female, "не уточнил_а", "женский")]
        [InlineData(SexEnum.Male, null, "мужской")]
        [InlineData(SexEnum.Male, "не уточнил_а", "мужской")]
        [InlineData(SexEnum.Other, null, "другой")]
        [InlineData(SexEnum.Other, "не уточнил_а", "не уточнил_а")]
        public void SexDisplay_Always_DisplaysCorrectSex(SexEnum sex, string? otherSex, string displaySex)
        {
            //arrange
            var sut = new Survivor(default, false, default, default,
                "name", 42,
                sex, otherSex,
                default,default);

            //assert
            sut.SexDisplay.Should().Be(displaySex);
        }

        [Fact, UnitTest]
        public void Age_WhenAccurateDobIsNotNull_NotNull()
        {
            //arrange
            var sut = new Survivor(default, false, default, default,
                "name", 42,
                SexEnum.Female, null,
                new (),null);

            //assert
            sut.Age.Should().NotBeNull();
        }

        [Fact, UnitTest]
        public void Age_WhenCalculatedDobIsNotNull_NotNull()
        {
            //arrange
            var sut = new Survivor(default, false, default, default,
                "name", 42,
                SexEnum.Female, null,
                null,new());

            //assert
            sut.Age.Should().NotBeNull();
        }

        [Fact, UnitTest]
        public void Age_WhenAllDobsAreNull_IsNull()
        {
            //arrange
            var sut = new Survivor(default, false, default, default,
                "name", 42,
                SexEnum.Female, null,
                null,null);

            //assert
            sut.Age.Should().BeNull();
        }
    }
}

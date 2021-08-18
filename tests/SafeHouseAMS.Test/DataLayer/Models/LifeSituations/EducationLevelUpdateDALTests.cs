using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.DataLayer.Models.LifeSituations;

namespace SafeHouseAMS.Test.DataLayer.Models.LifeSituations
{
    public class EducationLevelUpdateDALTests
    {
        [Property]
        public void Record_Always_ContainsOneOrZeroRecords()
        {
            Arb.Register<NotNullStringsGenerators>();
            Arb.Register<DALRecordGenerators>();

            Prop.ForAll<IEnumerable<BaseRecordDAL>>(records =>
                {
                    var recList = records.ToList();
                    var sut = new EducationLevelUpdateDAL()
                    {
                        AllRecords = recList
                    };
                    sut.Records.Should().NotBeNull();
                    if (recList.OfType<EducationLevelRecordDAL>().Any())
                        sut.Records.Should().NotBeEmpty();
                    else
                        sut.Records.Should().BeEmpty();

                })
                .QuickCheckThrowOnFailure();
        }
    }
}
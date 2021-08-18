using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.DataLayer.Models.LifeSituations;

namespace SafeHouseAMS.Test.DataLayer.Models.LifeSituations
{
    public class SpecialitiesUpdateDALTests
    {
        [Property]
        public void Record_Always_ContainsOneOrZeroRecords()
        {
            Arb.Register<NotNullStringsGenerators>();
            Arb.Register<DALRecordGenerators>();

            Prop.ForAll<IEnumerable<BaseRecordDAL>>(records =>
                {
                    var recList = records.ToList();
                    var sut = new SpecialitiesUpdateDAL()
                    {
                        AllRecords = recList
                    };
                    sut.Records.Should().NotBeNull();
                    if (recList.OfType<SpecialityRecordDAL>().Any())
                        sut.Records.Should().NotBeEmpty();
                    else
                        sut.Records.Should().BeEmpty();

                })
                .QuickCheckThrowOnFailure();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.DataLayer.Models.LifeSituations;
using Xunit;

namespace SafeHouseAMS.Test.DataLayer.Models.LifeSituations
{
    public class CitizenshipChangeDALTests
    {
        [Property]
        public void Record_Always_ContainsOneOrZeroRecords()
        {
            Arb.Register<NotNullStringsGenerators>();
            Arb.Register<DALRecordGenerators>();

            Prop.ForAll<IEnumerable<BaseRecordDAL>>(records =>
                {
                    var recList = records.ToList();
                    var sut = new CitizenshipChangeDAL()
                    {
                        AllRecords = recList
                    };
                    if (recList.OfType<CitizenshipRecordDAL>().Count() > 1)
                        Assert.Throws<InvalidOperationException>(() => _ = sut.Record);
                    else if (recList.OfType<CitizenshipRecordDAL>().Any())
                        sut.Record.Should().NotBeNull();
                    else
                        sut.Record.Should().BeNull();

                })
                .QuickCheckThrowOnFailure();
        }
    }
}
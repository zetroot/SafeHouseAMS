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
    public class ChildrenUpdateDALTests
    {
        [Property]
        public void Record_Always_ContainsOneOrZeroRecords()
        {
            Arb.Register<NotNullStringsGenerators>();
            Arb.Register<DALRecordGenerators>();

            Prop.ForAll<IEnumerable<BaseRecordDAL>>(records =>
                {
                    var recList = records.ToList();
                    var sut = new ChildrenUpdateDAL
                    {
                        AllRecords = recList
                    };
                    if (recList.OfType<ChildrenRecordDAL>().Count() > 1)
                        Assert.Throws<InvalidOperationException>(() => _ = sut.Record);
                    else if (recList.OfType<ChildrenRecordDAL>().Any())
                        sut.Record.Should().NotBeNull();
                    else
                        sut.Record.Should().BeNull();

                })
                .QuickCheckThrowOnFailure();
        }
    }

}

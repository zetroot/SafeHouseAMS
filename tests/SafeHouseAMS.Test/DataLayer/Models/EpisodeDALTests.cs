using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.BizLayer.ExploitationEpisodes;
using SafeHouseAMS.DataLayer.Models.ExploitationEpisodes;

namespace SafeHouseAMS.Test.DataLayer.Models
{
    public class EpisodeDALTests
    {
        [Property]
        public void EpisodeDAL_WhenUpdatedWithContactReson_ReturnsUpdatedContactReason()
        {
            var sut = new EpisodeDAL();

            Arb.Register<NotNullStringsGenerators>();

            Prop.ForAll<ContactReason>(cr =>
            {
                sut.UpdateContactReason(cr);
                var resulted = sut.BuildContactReason();
                resulted.Should().BeEquivalentTo(cr);
            }).QuickCheckThrowOnFailure();
        }
    }
}

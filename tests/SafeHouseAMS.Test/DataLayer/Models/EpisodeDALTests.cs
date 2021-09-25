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

        [Property]
        public void EpisodeDAL_WhenUpdatedWithControlMethods_ReturnsUpdatedControlMethods()
        {
            var sut = new EpisodeDAL();

            Prop.ForAll<ControlMethods>(cm =>
            {
                sut.UpdateControlMethods(cm);
                var resulted = sut.BuildControlMethods();
                resulted.Should().BeEquivalentTo(cm);
            }).QuickCheckThrowOnFailure();
        }
    }
}

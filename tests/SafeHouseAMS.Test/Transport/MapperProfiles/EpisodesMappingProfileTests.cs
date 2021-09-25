using System;
using AutoMapper;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.BizLayer.ExploitationEpisodes;
using SafeHouseAMS.BizLayer.Survivors;
using SafeHouseAMS.Transport.MapperProfiles;
using Xunit;

namespace SafeHouseAMS.Test.Transport.MapperProfiles
{
    public class EpisodesMappingProfileTests
    {
        private Mapper BuildMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(EpisodesMappingProfile).Assembly));
            return new(cfg);
        }

        [Property]
        public void RoundTripMapping_ReturnsSameEpisode()
        {
            Arb.Register<DateTimeGenerators>();
            Arb.Register<NotNullStringsGenerators>();

            var mapper = BuildMapper();

            Prop.ForAll<Episode>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.ExploitationEpisodes.Episode>(src);
                var result = mapper.Map<Episode>(dto);

                 result.Should()
                     .BeEquivalentTo(src, opts => opts.IncludingProperties());
            }).QuickCheckThrowOnFailure();
        }

        [Property]
        public void RoundTripMapping_ReturnsSameControlMethods()
        {
            Arb.Register<NotNullStringsGenerators>();
            var mapper = BuildMapper();

            Prop.ForAll<ControlMethods>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.ExploitationEpisodes.ControlMethods>(src);
                var result = mapper.Map<ControlMethods>(dto);

                 result.Should().BeEquivalentTo(src);
            }).QuickCheckThrowOnFailure();
        }

        [Property]
        public void RoundTripMapping_ReturnsSameContactReason()
        {
            Arb.Register<NotNullStringsGenerators>();
            var mapper = BuildMapper();

            Prop.ForAll<ContactReason>(src =>
            {
                var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.ExploitationEpisodes.ContactReason>(src);
                var result = mapper.Map<ContactReason>(dto);

                result.Should().BeEquivalentTo(src);
            }).QuickCheckThrowOnFailure();
        }
    }

}

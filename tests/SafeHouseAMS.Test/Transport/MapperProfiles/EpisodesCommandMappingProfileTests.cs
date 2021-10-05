using AutoMapper;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.BizLayer.ExploitationEpisodes.Commands;
using SafeHouseAMS.Transport.MapperProfiles;

namespace SafeHouseAMS.Test.Transport.MapperProfiles
{
    public class EpisodesCommandMappingProfileTests
    {
        private Mapper BuildMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(EpisodeCommandsMappingProfile).Assembly));
            return new(cfg);
        }

        [Property]
        public void CreateEpisode_OnRoundTripMapping_DoesNotChanges()
        {
            Arb.Register<DateTimeGenerators>();
            Arb.Register<NotNullStringsGenerators>();

            var mapper = BuildMapper();
            Prop.ForAll<CreateEpisode>(src =>
            {
                var dto =
                    mapper.Map<SafeHouseAMS.Transport.Protos.Models.ExploitationEpisodes.Commands.CreateEpisode>(src);
                var result = mapper.Map<CreateEpisode>(dto);

                result.Should().BeEquivalentTo(src);
            }).QuickCheckThrowOnFailure();
        }

        [Property]
        public void UpdateEpisode_OnRoundTripMapping_DoesNotChanges()
        {
            Arb.Register<DateTimeGenerators>();
            Arb.Register<NotNullStringsGenerators>();

            var mapper = BuildMapper();
            Prop.ForAll<UpdateEpisode>(src =>
            {
                var dto =
                    mapper.Map<SafeHouseAMS.Transport.Protos.Models.ExploitationEpisodes.Commands.UpdateEpisode>(src);
                var result = mapper.Map<UpdateEpisode>(dto);

                result.Should().BeEquivalentTo(src);
            }).QuickCheckThrowOnFailure();
        }

        [Property]
        public void EpisodeCommand_OnRoundTripMapping_DoesNotChanges()
        {
            Arb.Register<DateTimeGenerators>();
            Arb.Register<NotNullStringsGenerators>();

            var commandsArb = Gen
                .OneOf(
                    Arb.From<CreateEpisode>().Generator.Select(x => x as EpisodeCommand),
                    Arb.From<UpdateEpisode>().Generator.Select(x => x as EpisodeCommand)
                )
                .ToArbitrary();

            var mapper = BuildMapper();
            Prop.ForAll(commandsArb, src =>
            {
                var dto =
                    mapper.Map<SafeHouseAMS.Transport.Protos.Models.ExploitationEpisodes.Commands.EpisodeCommand>(src);
                var result = mapper.Map<EpisodeCommand>(dto);

                result.Should().BeOfType(src.GetType());
                result.Should().BeEquivalentTo(src, opt => opt.RespectingRuntimeTypes());
            }).QuickCheckThrowOnFailure();
        }
    }
}

using System;
using AutoMapper;
using SafeHouseAMS.BizLayer.ExploitationEpisodes.Commands;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class EpisodeCommandsMappingProfile : Profile
    {
        public EpisodeCommandsMappingProfile()
        {
            MapCreateCommand();

            MapCommandWrapper();
        }
        private void MapCommandWrapper()
        {
            CreateMap<EpisodeCommand, Protos.Models.ExploitationEpisodes.Commands.EpisodeCommand>()
                .ConstructUsing((src, ctx) =>
                {
                    var result = new Protos.Models.ExploitationEpisodes.Commands.EpisodeCommand();
                    switch (src)
                    {
                        case CreateEpisode create:
                            result.Create =
                                ctx.Mapper.Map<Protos.Models.ExploitationEpisodes.Commands.CreateEpisode>(create);
                            break;
                        default:
                            throw new ArgumentException();
                    }
                    return result;
                });

            CreateMap<Protos.Models.ExploitationEpisodes.Commands.EpisodeCommand, EpisodeCommand>()
                .ConstructUsing((src, ctx) => src.CommandCase switch
                {
                    Protos.Models.ExploitationEpisodes.Commands.EpisodeCommand.CommandOneofCase.Create =>
                        ctx.Mapper.Map<CreateEpisode>(src.Create),
                    _ => throw new ArgumentException()
                });
        }

        private void MapCreateCommand()
        {
            CreateMap<CreateEpisode, Protos.Models.ExploitationEpisodes.Commands.CreateEpisode>();
            CreateMap<Protos.Models.ExploitationEpisodes.Commands.CreateEpisode, CreateEpisode>();
        }
    }
}

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
            MapUpdateCommand();
            MapDeleteCommand();

            MapCommandWrapper();
        }

        private void MapCommandWrapper()
        {
            CreateMap<EpisodeCommand, Protos.Models.ExploitationEpisodes.Commands.EpisodeCommand>(MemberList.None)
                .ConstructUsing((src, ctx) =>
                {
                    var result = new Protos.Models.ExploitationEpisodes.Commands.EpisodeCommand();
                    switch (src)
                    {
                        case CreateEpisode create:
                            result.Create =
                                ctx.Mapper.Map<Protos.Models.ExploitationEpisodes.Commands.CreateEpisode>(create);
                            break;
                        case UpdateEpisode update:
                            result.Update =
                                ctx.Mapper.Map<Protos.Models.ExploitationEpisodes.Commands.UpdateEpisode>(update);
                            break;
                        case DeleteEpisode delete:
                            result.Delete =
                                ctx.Mapper.Map<Protos.Models.ExploitationEpisodes.Commands.DeleteEpisode>(delete);
                            break;
                        default:
                            throw new ArgumentException();
                    }
                    return result;
                });

            CreateMap<Protos.Models.ExploitationEpisodes.Commands.EpisodeCommand, EpisodeCommand>(MemberList.None)
                .ConstructUsing((src, ctx) => src.CommandCase switch
                {
                    Protos.Models.ExploitationEpisodes.Commands.EpisodeCommand.CommandOneofCase.Create =>
                        ctx.Mapper.Map<CreateEpisode>(src.Create),
                    Protos.Models.ExploitationEpisodes.Commands.EpisodeCommand.CommandOneofCase.Update =>
                        ctx.Mapper.Map<UpdateEpisode>(src.Update),
                    Protos.Models.ExploitationEpisodes.Commands.EpisodeCommand.CommandOneofCase.Delete =>
                        ctx.Mapper.Map<DeleteEpisode>(src.Delete),
                    _ => throw new ArgumentException()
                });
        }

        private void MapCreateCommand()
        {
            CreateMap<CreateEpisode, Protos.Models.ExploitationEpisodes.Commands.CreateEpisode>();
            CreateMap<Protos.Models.ExploitationEpisodes.Commands.CreateEpisode, CreateEpisode>();
        }

        private void MapUpdateCommand()
        {
            CreateMap<UpdateEpisode, Protos.Models.ExploitationEpisodes.Commands.UpdateEpisode>();
            CreateMap<Protos.Models.ExploitationEpisodes.Commands.UpdateEpisode, UpdateEpisode>();
        }

        private void MapDeleteCommand()
        {
            CreateMap<DeleteEpisode, Protos.Models.ExploitationEpisodes.Commands.DeleteEpisode>();
            CreateMap<Protos.Models.ExploitationEpisodes.Commands.DeleteEpisode, DeleteEpisode>();
        }

    }
}

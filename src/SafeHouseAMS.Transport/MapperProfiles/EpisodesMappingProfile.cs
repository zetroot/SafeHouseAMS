using System;
using AutoMapper;
using SafeHouseAMS.BizLayer.ExploitationEpisodes;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class EpisodesMappingProfile : Profile
    {
        public EpisodesMappingProfile()
        {
            MapControlMethods();
            MapDetailedContactReasons();
            MapDetailedContactReasons<CseType>();
            MapDetailedContactReasons<ForcedLabourType>();
            MapDetailedContactReasons<ForcedMarriageKind>();
            MapDetailedContactReasons<CriminalActivityType>();
            MapContactReasons();
            MapEpisodes();
        }
        private void MapControlMethods()
        {
            CreateMap<ControlMethods, Protos.Models.ExploitationEpisodes.ControlMethods>();
            CreateMap<Protos.Models.ExploitationEpisodes.ControlMethods, ControlMethods>();
        }
        private void MapDetailedContactReasons()
        {
            CreateMap<DetailedContactReason, Protos.Models.ExploitationEpisodes.DetailedContactReason>();
            CreateMap<Protos.Models.ExploitationEpisodes.DetailedContactReason, DetailedContactReason>();
        }

        private void MapDetailedContactReasons<T>() where T : Enum
        {
            CreateMap<DetailedContactReason<T>, Protos.Models.ExploitationEpisodes.DetailedContactReasonTyped>();
            CreateMap<Protos.Models.ExploitationEpisodes.DetailedContactReasonTyped, DetailedContactReason<T>>();
        }

        private void MapContactReasons()
        {
            CreateMap<ContactReason, Protos.Models.ExploitationEpisodes.ContactReason>();
            CreateMap<Protos.Models.ExploitationEpisodes.ContactReason, ContactReason>();
        }
        private void MapEpisodes()
        {
            CreateMap<Episode, Protos.Models.ExploitationEpisodes.Episode>();
            CreateMap<Protos.Models.ExploitationEpisodes.Episode, Episode>();
        }
    }
}

using AutoMapper;
using SafeHouseAMS.BizLayer.ExploitationEpisodes;
using SafeHouseAMS.DataLayer.Models.ExploitationEpisodes;

namespace SafeHouseAMS.DataLayer.MapperProfiles
{
    internal class EpisodesMappingProfile : Profile
    {
        public EpisodesMappingProfile()
        {
            MapEpisodes();
        }
        private void MapEpisodes()
        {
            CreateMap<EpisodeDAL, Episode>()
                .ForMember(d => d.ControlMethods, opt => opt.Ignore())
                .ForCtorParam(nameof(Episode.ContactReason), opt => opt.MapFrom(src => src.BuildContactReason()))
                .ForCtorParam(nameof(Episode.ControlMethods), opt => opt.MapFrom(src => src.BuildControlMethods()));
        }
    }
}

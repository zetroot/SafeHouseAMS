using AutoMapper;
using SafeHouseAMS.BizLayer.AssistanceRequests;
using SafeHouseAMS.DataLayer.Models.AssistanceRequests;

namespace SafeHouseAMS.DataLayer.MapperProfiles
{
    internal class AssistanceRequestsMappingProfile : Profile
    {
        public AssistanceRequestsMappingProfile()
        {
            MapRequests();
            MapActs();
        }

        private void MapActs()
        {
            CreateMap<AssistanceActDAL, AssistanceAct>();
        }

        private void MapRequests()
        {
            CreateMap<AssistanceRequestDAL, AssistanceRequest>();
        }
    }
}

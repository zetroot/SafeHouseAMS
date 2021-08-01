using AutoMapper;
using SafeHouseAMS.BizLayer.LifeSituations.Commands;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class InquiryCommandsMappingProfile : Profile
    {
        public InquiryCommandsMappingProfile()
        {
            MapAddEducationLevelCommand();
            MapAddSpeciality();
            MapCreateInquiry();
            MapSetChildren();
            MapSetDomicile();
            MapSetVulnerabilities();
            MapSetWorkingExperience();
        }
        private void MapSetWorkingExperience()
        {
            CreateMap<SetWorkingExperience, Protos.Models.LifeSituations.Commands.SetWorkingExperience>();
            CreateMap<Protos.Models.LifeSituations.Commands.SetWorkingExperience, SetWorkingExperience>();
        }
        private void MapSetVulnerabilities()
        {
            CreateMap<SetVulnerabilities, Protos.Models.LifeSituations.Commands.SetVulnerabilities>();
            CreateMap<Protos.Models.LifeSituations.Commands.SetVulnerabilities, SetVulnerabilities>();
        }
        private void MapSetDomicile()
        {
            CreateMap<SetDomicile, Protos.Models.LifeSituations.Commands.SetDomicile>();
            CreateMap<Protos.Models.LifeSituations.Commands.SetDomicile, SetDomicile>();
        }
        private void MapSetChildren()
        {
            CreateMap<SetChildren, Protos.Models.LifeSituations.Commands.SetChildren>();
            CreateMap<Protos.Models.LifeSituations.Commands.SetChildren, SetChildren>();
        }
        private void MapCreateInquiry()
        {
            CreateMap<CreateInquiry, Protos.Models.LifeSituations.Commands.CreateInquiry>();
            CreateMap<Protos.Models.LifeSituations.Commands.CreateInquiry, CreateInquiry>();
        }
        private void MapAddSpeciality()
        {
            CreateMap<AddSpeciality, Protos.Models.LifeSituations.Commands.AddSpeciality>();
            CreateMap<Protos.Models.LifeSituations.Commands.AddSpeciality, AddSpeciality>();
        }
        private void MapAddEducationLevelCommand()
        {
            CreateMap<AddEducationLevel, Protos.Models.LifeSituations.Commands.AddEducationLevel>();
            CreateMap<Protos.Models.LifeSituations.Commands.AddEducationLevel, AddEducationLevel>();
        }
    }
}
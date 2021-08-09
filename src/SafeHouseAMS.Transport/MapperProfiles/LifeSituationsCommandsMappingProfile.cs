using System;
using AutoMapper;
using SafeHouseAMS.BizLayer.LifeSituations.Commands;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class LifeSituationsCommandsMappingProfile : Profile
    {
        public LifeSituationsCommandsMappingProfile()
        {
            MapAddEducationLevelCommand();
            MapAddSpeciality();
            MapCreateInquiry();
            MapSetChildren();
            MapSetDomicile();
            MapSetVulnerabilities();
            MapSetWorkingExperience();
            MapAddMigrationStatus();
            MapAddRegistrationStatus();
            MapCreateCitizenshipChange();
            MapCommandsWrapper();
        }
        private void MapCreateCitizenshipChange()
        {
            CreateMap<CreateCitizenshipChange, Protos.Models.LifeSituations.Commands.CreateCitizenshipChange>();
            CreateMap<Protos.Models.LifeSituations.Commands.CreateCitizenshipChange, CreateCitizenshipChange>();
        }

        private void MapAddRegistrationStatus()
        {
            CreateMap<AddRegistrationStatus, Protos.Models.LifeSituations.Commands.AddRegistrationStatus>();
            CreateMap<Protos.Models.LifeSituations.Commands.AddRegistrationStatus, AddRegistrationStatus>();
        }
        private void MapAddMigrationStatus()
        {
            CreateMap<AddMigrationStatus, Protos.Models.LifeSituations.Commands.AddMigrationStatus>();
            CreateMap<Protos.Models.LifeSituations.Commands.AddMigrationStatus, AddMigrationStatus>();
        }

        private void MapCommandsWrapper()
        {
            CreateMap<LifeSituationDocumentCommand, Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand>()
                .ConstructUsing((src, ctx) =>
                {
                    var result = new Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand();
                    switch (src)
                    {
                        case AddEducationLevel addEducationLevel:
                            result.AddEducationLevel = ctx.Mapper.Map<Protos.Models.LifeSituations.Commands.AddEducationLevel>(addEducationLevel);
                            break;
                        case AddSpeciality addSpeciality:
                            result.AddSpeciality = ctx.Mapper.Map<Protos.Models.LifeSituations.Commands.AddSpeciality>(addSpeciality);
                            break;
                        case CreateInquiry createInquiry:
                            result.CreateInquiry = ctx.Mapper.Map<Protos.Models.LifeSituations.Commands.CreateInquiry>(createInquiry);
                            break;
                        case SetChildren setChildren:
                            result.SetChildren = ctx.Mapper.Map<Protos.Models.LifeSituations.Commands.SetChildren>(setChildren);
                            break;
                        case SetDomicile setDomicile:
                            result.SetDomicile = ctx.Mapper.Map<Protos.Models.LifeSituations.Commands.SetDomicile>(setDomicile);
                            break;
                        case SetVulnerabilities setVulnerabilities:
                            result.SetVulnerabilities = ctx.Mapper.Map<Protos.Models.LifeSituations.Commands.SetVulnerabilities>(setVulnerabilities);
                            break;
                        case SetWorkingExperience setWorkingExperience:
                            result.SetWorkingExperience = ctx.Mapper.Map<Protos.Models.LifeSituations.Commands.SetWorkingExperience>(setWorkingExperience);
                            break;
                        case AddMigrationStatus addMigrationStatus:
                            result.AddMigrationStatus =
                                ctx.Mapper.Map<Protos.Models.LifeSituations.Commands.AddMigrationStatus>(addMigrationStatus);
                            break;
                        case AddRegistrationStatus addRegistrationStatus:
                            result.AddRegistrationStatus =
                                ctx.Mapper.Map<Protos.Models.LifeSituations.Commands.AddRegistrationStatus>(addRegistrationStatus);
                            break;
                        case CreateCitizenshipChange createCitizenshipChange:
                            result.CreateCitizenshipChange =
                                ctx.Mapper.Map<Protos.Models.LifeSituations.Commands.CreateCitizenshipChange>(createCitizenshipChange);
                             break;
                    }
                    return result;
                });

            CreateMap<Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand, LifeSituationDocumentCommand>()
                .ConstructUsing((src, ctx) => src.CommandCase switch
                {
                    Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand.CommandOneofCase.AddEducationLevel => ctx.Mapper.Map<AddEducationLevel>(src.AddEducationLevel),
                    Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand.CommandOneofCase.AddSpeciality => ctx.Mapper.Map<AddSpeciality>(src.AddSpeciality),
                    Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand.CommandOneofCase.CreateInquiry => ctx.Mapper.Map<CreateInquiry>(src.CreateInquiry),
                    Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand.CommandOneofCase.SetChildren => ctx.Mapper.Map<SetChildren>(src.SetChildren),
                    Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand.CommandOneofCase.SetDomicile => ctx.Mapper.Map<SetDomicile>(src.SetDomicile),
                    Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand.CommandOneofCase.SetVulnerabilities => ctx.Mapper.Map<SetVulnerabilities>(src.SetVulnerabilities),
                    Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand.CommandOneofCase.SetWorkingExperience => ctx.Mapper.Map<SetWorkingExperience>(src.SetWorkingExperience),
                    Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand.CommandOneofCase.AddMigrationStatus => ctx.Mapper.Map<AddMigrationStatus>(src.AddMigrationStatus),
                    Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand.CommandOneofCase.AddRegistrationStatus => ctx.Mapper.Map<AddRegistrationStatus>(src.AddRegistrationStatus),
                    Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand.CommandOneofCase.CreateCitizenshipChange => ctx.Mapper.Map<CreateCitizenshipChange>(src.CreateCitizenshipChange),
                    _ => throw new InvalidOperationException()
                });
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

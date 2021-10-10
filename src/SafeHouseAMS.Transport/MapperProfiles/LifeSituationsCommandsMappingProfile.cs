using System;
using AutoMapper;
using SafeHouseAMS.BizLayer.LifeSituations.Commands;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

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
            MapSetCitizenship();
            MapSetDomicile();
            MapSetVulnerabilities();
            MapSetWorkingExperience();
            MapAddMigrationStatus();
            MapAddRegistrationStatus();
            MapCreateCreateRecordUpdateCommand();
            MapDeleteDocumentCommand();
            MapCommandsWrapper();
        }

        private void MapCreateCreateRecordUpdateCommand()
        {
            CreateMap<CreateRecordUpdateDocument<ChildrenRecord>,
                    Protos.Models.LifeSituations.Commands.CreateRecordUpdateDocument>()
                .AfterMap((_, dst) => dst.Type = Protos.Models.LifeSituations.Commands.RecordType.Children);

            CreateMap<CreateRecordUpdateDocument<CitizenshipRecord>,
                    Protos.Models.LifeSituations.Commands.CreateRecordUpdateDocument>()
                .AfterMap((_, dst) => dst.Type = Protos.Models.LifeSituations.Commands.RecordType.Citizenship);

            CreateMap<CreateRecordUpdateDocument<DomicileRecord>,
                    Protos.Models.LifeSituations.Commands.CreateRecordUpdateDocument>()
                .AfterMap((_, dst) => dst.Type = Protos.Models.LifeSituations.Commands.RecordType.Domicile);

            CreateMap<CreateRecordUpdateDocument<EducationLevelRecord>,
                    Protos.Models.LifeSituations.Commands.CreateRecordUpdateDocument>()
                .AfterMap((_, dst) => dst.Type = Protos.Models.LifeSituations.Commands.RecordType.Education);

            CreateMap<CreateRecordUpdateDocument<MigrationStatusRecord>,
                    Protos.Models.LifeSituations.Commands.CreateRecordUpdateDocument>()
                .AfterMap((_, dst) => dst.Type = Protos.Models.LifeSituations.Commands.RecordType.Migration);

            CreateMap<CreateRecordUpdateDocument<RegistrationStatusRecord>,
                    Protos.Models.LifeSituations.Commands.CreateRecordUpdateDocument>()
                .AfterMap((_, dst) => dst.Type = Protos.Models.LifeSituations.Commands.RecordType.Registration);

            CreateMap<CreateRecordUpdateDocument<SpecialityRecord>,
                    Protos.Models.LifeSituations.Commands.CreateRecordUpdateDocument>()
                .AfterMap((_, dst) => dst.Type = Protos.Models.LifeSituations.Commands.RecordType.Speciality);


            CreateMap<Protos.Models.LifeSituations.Commands.CreateRecordUpdateDocument, CreateDocument>()
                .ConstructUsing((src, ctx) =>
                {
                    var entityId = ctx.Mapper.Map<Guid>(src.EntityID);
                    var docDate = ctx.Mapper.Map<DateTime>(src.DocumentDate);
                    var survId = ctx.Mapper.Map<Guid>(src.SurvivorId);
                    return src.Type switch
                    {
                        Protos.Models.LifeSituations.Commands.RecordType.Citizenship =>
                            new CreateRecordUpdateDocument<CitizenshipRecord>(entityId, survId, docDate),
                        Protos.Models.LifeSituations.Commands.RecordType.Children =>
                            new CreateRecordUpdateDocument<ChildrenRecord>(entityId, survId, docDate),
                        Protos.Models.LifeSituations.Commands.RecordType.Domicile =>
                            new CreateRecordUpdateDocument<DomicileRecord>(entityId, survId, docDate),
                        Protos.Models.LifeSituations.Commands.RecordType.Education =>
                            new CreateRecordUpdateDocument<EducationLevelRecord>(entityId, survId, docDate),
                        Protos.Models.LifeSituations.Commands.RecordType.Migration =>
                            new CreateRecordUpdateDocument<MigrationStatusRecord>(entityId, survId, docDate),
                        Protos.Models.LifeSituations.Commands.RecordType.Registration =>
                            new CreateRecordUpdateDocument<RegistrationStatusRecord>(entityId, survId, docDate),
                        Protos.Models.LifeSituations.Commands.RecordType.Speciality =>
                            new CreateRecordUpdateDocument<SpecialityRecord>(entityId, survId, docDate),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                });
        }

        private void MapAddRegistrationStatus()
        {
            CreateMap<SetRegistrationStatus, Protos.Models.LifeSituations.Commands.SetRegistrationStatus>();
            CreateMap<Protos.Models.LifeSituations.Commands.SetRegistrationStatus, SetRegistrationStatus>();
        }
        private void MapAddMigrationStatus()
        {
            CreateMap<SetMigrationStatus, Protos.Models.LifeSituations.Commands.SetMigrationStatus>();
            CreateMap<Protos.Models.LifeSituations.Commands.SetMigrationStatus, SetMigrationStatus>();
        }

        private void MapCommandsWrapper()
        {
            CreateMap<LifeSituationDocumentCommand, Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand>()
                .ConstructUsing((src, ctx) =>
                {
                    var result = new Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand();
                    switch (src)
                    {
                        case SetCitizenship setCitizenship:
                            result.SetCitizenship =
                                ctx.Mapper.Map<Protos.Models.LifeSituations.Commands.SetCitizenship>(setCitizenship);
                            break;
                        case AddEducationLevel addEducationLevel:
                            result.AddEducationLevel =
                                ctx.Mapper.Map<Protos.Models.LifeSituations.Commands.AddEducationLevel>(addEducationLevel);
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
                        case SetMigrationStatus addMigrationStatus:
                            result.SetMigrationStatus =
                                ctx.Mapper.Map<Protos.Models.LifeSituations.Commands.SetMigrationStatus>(addMigrationStatus);
                            break;
                        case SetRegistrationStatus addRegistrationStatus:
                            result.SetRegistrationStatus =
                                ctx.Mapper.Map<Protos.Models.LifeSituations.Commands.SetRegistrationStatus>(addRegistrationStatus);
                            break;
                        case CreateDocument createDocument:
                            result.CreateRecordUpdateDocument =
                                ctx.Mapper.Map<Protos.Models.LifeSituations.Commands.CreateRecordUpdateDocument>(createDocument);
                             break;
                        case DeleteDocument deleteDocument:
                            result.DeleteDocument =
                                ctx.Mapper.Map<Protos.Models.LifeSituations.Commands.DeleteDocument>(deleteDocument);
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
                    Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand.CommandOneofCase.SetCitizenship => ctx.Mapper.Map<SetCitizenship>(src.SetCitizenship),
                    Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand.CommandOneofCase.SetDomicile => ctx.Mapper.Map<SetDomicile>(src.SetDomicile),
                    Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand.CommandOneofCase.SetVulnerabilities => ctx.Mapper.Map<SetVulnerabilities>(src.SetVulnerabilities),
                    Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand.CommandOneofCase.SetWorkingExperience => ctx.Mapper.Map<SetWorkingExperience>(src.SetWorkingExperience),
                    Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand.CommandOneofCase.SetMigrationStatus => ctx.Mapper.Map<SetMigrationStatus>(src.SetMigrationStatus),
                    Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand.CommandOneofCase.SetRegistrationStatus => ctx.Mapper.Map<SetRegistrationStatus>(src.SetRegistrationStatus),
                    Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand.CommandOneofCase.CreateRecordUpdateDocument => ctx.Mapper.Map<CreateDocument>(src.CreateRecordUpdateDocument),
                    Protos.Models.LifeSituations.Commands.LifeSituationDocumentCommand.CommandOneofCase.DeleteDocument => ctx.Mapper.Map<DeleteDocument>(src.DeleteDocument),
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
        private void MapSetCitizenship()
        {
            CreateMap<SetCitizenship, Protos.Models.LifeSituations.Commands.SetCitizenship>();
            CreateMap<Protos.Models.LifeSituations.Commands.SetCitizenship, SetCitizenship>();
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
        private void MapDeleteDocumentCommand()
        {
            CreateMap<DeleteDocument, Protos.Models.LifeSituations.Commands.DeleteDocument>();
            CreateMap<Protos.Models.LifeSituations.Commands.DeleteDocument, DeleteDocument>();
        }
    }
}

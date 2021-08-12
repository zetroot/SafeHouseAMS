using AutoMapper;
using SafeHouseAMS.BizLayer.LifeSituations.Documents;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class RecordUpdateDocumentsMappingProfile : Profile
    {
        public RecordUpdateDocumentsMappingProfile()
        {
            MapCitizenshipUpdate();
            MapChildrenUpdate();
            MapDomicileUpdate();
            MapMigrationStatusUpdate();
            MapRegistrationStatusUpdate();

            MapEducationUopdate();
            MapSpecialitiesUpdate();
        }
        private void MapSpecialitiesUpdate()
        {
            CreateMap<MultiRecordsUpdate<SpecialityRecord>, Protos.Models.LifeSituations.SpecialitiesUpdate>();
            CreateMap<Protos.Models.LifeSituations.SpecialitiesUpdate, MultiRecordsUpdate<SpecialityRecord>>();
        }

        private void MapEducationUopdate()
        {
            CreateMap<MultiRecordsUpdate<EducationLevelRecord>, Protos.Models.LifeSituations.EducationUpdate>();
            CreateMap<Protos.Models.LifeSituations.EducationUpdate, MultiRecordsUpdate<EducationLevelRecord>>();
        }

        private void MapRegistrationStatusUpdate()
        {
            CreateMap<SingleRecordUpdate<RegistrationStatusRecord>, Protos.Models.LifeSituations.RegistrationStatusUpdate>();
            CreateMap<Protos.Models.LifeSituations.RegistrationStatusUpdate, SingleRecordUpdate<RegistrationStatusRecord>>();
        }

        private void MapMigrationStatusUpdate()
        {
            CreateMap<SingleRecordUpdate<MigrationStatusRecord>, Protos.Models.LifeSituations.MigrationStatusUpdate>();
            CreateMap<Protos.Models.LifeSituations.MigrationStatusUpdate, SingleRecordUpdate<MigrationStatusRecord>>();
        }

        private void MapDomicileUpdate()
        {
            CreateMap<SingleRecordUpdate<DomicileRecord>, Protos.Models.LifeSituations.DomicileUpdate>();
            CreateMap<Protos.Models.LifeSituations.DomicileUpdate, SingleRecordUpdate<DomicileRecord>>();
        }

        private void MapChildrenUpdate()
        {
            CreateMap<SingleRecordUpdate<ChildrenRecord>, Protos.Models.LifeSituations.ChildrenUpdate>();
            CreateMap<Protos.Models.LifeSituations.ChildrenUpdate, SingleRecordUpdate<ChildrenRecord>>();
        }

        private void MapCitizenshipUpdate()
        {
            CreateMap<SingleRecordUpdate<CitizenshipRecord>, Protos.Models.LifeSituations.CitizenshipUpdate>();
            CreateMap<Protos.Models.LifeSituations.CitizenshipUpdate, SingleRecordUpdate<CitizenshipRecord>>();
        }
    }
}

using AutoMapper;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class RecordsMappingProfile : Profile
    {
        public RecordsMappingProfile()
        {
            MapChildrenRecord();
            MapCitizenshipRecord();
            MapDomicileRecord();
            MapEducationLevelRecord();
            MapSpecialityRecord();
        }
        private void MapSpecialityRecord()
        {
            CreateMap<SpecialityRecord, Protos.Models.LifeSituations.Records.SpecialityRecord>();
            CreateMap<Protos.Models.LifeSituations.Records.SpecialityRecord, SpecialityRecord>();
        }
        private void MapEducationLevelRecord()
        {
            CreateMap<EducationLevelRecord, Protos.Models.LifeSituations.Records.EducationLevelRecord>();
            CreateMap<Protos.Models.LifeSituations.Records.EducationLevelRecord, EducationLevelRecord>();
        }
        private void MapDomicileRecord()
        {
            CreateMap<DomicileRecord, Protos.Models.LifeSituations.Records.DomicileRecord>();
            CreateMap<Protos.Models.LifeSituations.Records.DomicileRecord, DomicileRecord>();
        }
        private void MapCitizenshipRecord()
        {
            CreateMap<CitizenshipRecord, Protos.Models.LifeSituations.Records.CitizenshipRecord>();
            CreateMap<Protos.Models.LifeSituations.Records.CitizenshipRecord, CitizenshipRecord>();
        }
        private void MapChildrenRecord()
        {
            CreateMap<ChildrenRecord, Protos.Models.LifeSituations.Records.ChildrenRecord>();
            CreateMap<Protos.Models.LifeSituations.Records.ChildrenRecord, ChildrenRecord>();
        }
    }
}
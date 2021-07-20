using System.Runtime.Serialization;
using System.Text.Json;
using AutoMapper;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.DataLayer.Models.LifeSituations;

namespace SafeHouseAMS.DataLayer.MapperProfiles
{
    internal class RecordsMappingProfile : Profile
    {
        public RecordsMappingProfile()
        {
            MapBaseRecords();
            MapChildrenRecord();
            MapCitizenshipRecords();
            MapDomicileRecords();
        }
        private void MapDomicileRecords()
        {
            CreateMap<DomicileRecordDAL, DomicileRecord>()
                .IncludeBase<BaseRecordDAL, BaseRecord>()
                .ConstructUsing((src,_) => JsonSerializer.Deserialize<DomicileRecord>(src.Content) ?? throw new SerializationException());
        }
        private void MapCitizenshipRecords()
        {
            CreateMap<CitizenshipRecordDAL, CitizenshipRecord>()
                .IncludeBase<BaseRecordDAL, BaseRecord>()
                .ConstructUsing((src,_) => JsonSerializer.Deserialize<CitizenshipRecord>(src.Content) ?? throw new SerializationException());
        }
        private void MapChildrenRecord()
        {
            CreateMap<ChildrenRecordDAL, ChildrenRecord>()
                .IncludeBase<BaseRecordDAL, BaseRecord>()
                .ConstructUsing((src,_) => JsonSerializer.Deserialize<ChildrenRecord>(src.Content) ?? throw new SerializationException());
        }
        private void MapBaseRecords()
        {
            CreateMap<BaseRecordDAL, BaseRecord>();
        }
    }
}
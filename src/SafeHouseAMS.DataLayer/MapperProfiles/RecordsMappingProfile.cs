﻿using System.Runtime.Serialization;
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
            MapEducationLevelRecords();
            MapSpecialityRecords();
            MapMigrationStatusRecords();
            MapRegistartionStatusRecords();
        }
        private void MapRegistartionStatusRecords()
        {
            CreateMap<RegistrationStatusRecordDAL, RegistrationStatusRecord>()
                .IncludeBase<BaseRecordDAL, BaseRecord>()
                .ConstructUsing((src, _) =>
                    JsonSerializer.Deserialize<RegistrationStatusRecord>(src.Content) ??
                    throw new SerializationException());
        }
        private void MapMigrationStatusRecords()
        {
            CreateMap<MigrationStatusRecordDAL, MigrationStatusRecord>()
                .IncludeBase<BaseRecordDAL, BaseRecord>()
                .ConstructUsing((src, _) =>
                    JsonSerializer.Deserialize<MigrationStatusRecord>(src.Content) ??
                    throw new SerializationException());
        }
        private void MapSpecialityRecords()
        {
            CreateMap<SpecialityRecordDAL, SpecialityRecord>()
                .IncludeBase<BaseRecordDAL, BaseRecord>()
                .ConstructUsing((src,_) => JsonSerializer.Deserialize<SpecialityRecord>(src.Content) ?? throw new SerializationException());
        }
        private void MapEducationLevelRecords()
        {
            CreateMap<EducationLevelRecordDAL, EducationLevelRecord>()
                .IncludeBase<BaseRecordDAL, BaseRecord>()
                .ConstructUsing((src,_) => JsonSerializer.Deserialize<EducationLevelRecord>(src.Content) ?? throw new SerializationException());
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

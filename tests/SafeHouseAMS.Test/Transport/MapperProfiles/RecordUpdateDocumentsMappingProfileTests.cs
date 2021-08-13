using AutoMapper;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.BizLayer.LifeSituations.Documents;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.Transport.MapperProfiles;
using SafeHouseAMS.Transport.Protos.Models.LifeSituations;

namespace SafeHouseAMS.Test.Transport.MapperProfiles
{
    public class RecordUpdateDocumentsMappingProfileTests
    {
        private Mapper BuildMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(RecordUpdateDocumentsMappingProfile).Assembly));
            return new(cfg);
        }

        [Property]
        public void CitizenshipUpdate_RoundTrip_DoesnotChange()
        {
            var mapper = BuildMapper();
            Arb.Register<NotNullStringsGenerators>();
            Arb.Register<LocalDateTimeGenerators>();
            Prop.ForAll(Arb.From<SingleRecordUpdate<CitizenshipRecord>>(), src =>
                {
                    var dto = mapper.Map<CitizenshipUpdate>(src);
                    var result = mapper.Map<SingleRecordUpdate<CitizenshipRecord>>(dto);

                    result.Should().BeEquivalentTo(src);
                })
                .QuickCheckThrowOnFailure();
        }

        [Property]
        public void ChildrenUpdate_RoundTrip_DoesnotChange()
        {
            var mapper = BuildMapper();
            Arb.Register<NotNullStringsGenerators>();
            Arb.Register<LocalDateTimeGenerators>();
            Prop.ForAll(Arb.From<SingleRecordUpdate<ChildrenRecord>>(), src =>
                {
                    var dto = mapper.Map<ChildrenUpdate>(src);
                    var result = mapper.Map<SingleRecordUpdate<ChildrenRecord>>(dto);

                    result.Should().BeEquivalentTo(src);
                })
                .QuickCheckThrowOnFailure();
        }

        [Property]
        public void DomicileUpdate_RoundTrip_DoesnotChange()
        {
            var mapper = BuildMapper();
            Arb.Register<NotNullStringsGenerators>();
            Arb.Register<LocalDateTimeGenerators>();
            Prop.ForAll(Arb.From<SingleRecordUpdate<DomicileRecord>>(), src =>
                {
                    var dto = mapper.Map<DomicileUpdate>(src);
                    var result = mapper.Map<SingleRecordUpdate<DomicileRecord>>(dto);

                    result.Should().BeEquivalentTo(src);
                })
                .QuickCheckThrowOnFailure();
        }

        [Property]
        public void MigrationStatusUpdate_RoundTrip_DoesnotChange()
        {
            var mapper = BuildMapper();
            Arb.Register<NotNullStringsGenerators>();
            Arb.Register<LocalDateTimeGenerators>();
            Prop.ForAll(Arb.From<SingleRecordUpdate<MigrationStatusRecord>>(), src =>
                {
                    var dto = mapper.Map<MigrationStatusUpdate>(src);
                    var result = mapper.Map<SingleRecordUpdate<MigrationStatusRecord>>(dto);

                    result.Should().BeEquivalentTo(src);
                })
                .QuickCheckThrowOnFailure();
        }

        [Property]
        public void RegistrationStatusUpdate_RoundTrip_DoesnotChange()
        {
            var mapper = BuildMapper();
            Arb.Register<NotNullStringsGenerators>();
            Arb.Register<LocalDateTimeGenerators>();
            Prop.ForAll(Arb.From<SingleRecordUpdate<RegistrationStatusRecord>>(), src =>
                {
                    var dto = mapper.Map<RegistrationStatusUpdate>(src);
                    var result = mapper.Map<SingleRecordUpdate<RegistrationStatusRecord>>(dto);

                    result.Should().BeEquivalentTo(src);
                })
                .QuickCheckThrowOnFailure();
        }

        [Property]
        public void EducationUpdate_RoundTrip_DoesnotChange()
        {
            var mapper = BuildMapper();
            Arb.Register<NotNullStringsGenerators>();
            Arb.Register<LocalDateTimeGenerators>();
            Arb.Register<RecordListGenerators>();

            Prop.ForAll(Arb.From<MultiRecordsUpdate<EducationLevelRecord>>(), src =>
                {
                    var dto = mapper.Map<EducationUpdate>(src);
                    var result = mapper.Map<MultiRecordsUpdate<EducationLevelRecord>>(dto);

                    result.Should().BeEquivalentTo(src);
                })
                .QuickCheckThrowOnFailure();
        }

        [Property]
        public void SpecialitiesUpdate_RoundTrip_DoesnotChange()
        {
            var mapper = BuildMapper();
            Arb.Register<NotNullStringsGenerators>();
            Arb.Register<LocalDateTimeGenerators>();
            Arb.Register<RecordListGenerators>();

            Prop.ForAll(Arb.From<MultiRecordsUpdate<SpecialityRecord>>(), src =>
                {
                    var dto = mapper.Map<SpecialitiesUpdate>(src);
                    var result = mapper.Map<MultiRecordsUpdate<SpecialityRecord>>(dto);

                    result.Should().BeEquivalentTo(src);
                })
                .QuickCheckThrowOnFailure();
        }
    }
}

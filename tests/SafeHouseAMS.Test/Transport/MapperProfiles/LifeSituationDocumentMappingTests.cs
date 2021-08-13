using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.Documents;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.Transport.MapperProfiles;

namespace SafeHouseAMS.Test.Transport.MapperProfiles
{
    public class LifeSituationDocumentMappingTests
    {
        private Mapper BuildMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(LifeSituationsCommandsMappingProfile).Assembly));
            return new(cfg);
        }

        [Property]
        public void LifeSituationDocumentCommand_RoundTrip_DoesNotChanges()
        {
            Arb.Register<NotNullStringsGenerators>();
            Arb.Register<LocalDateTimeGenerators>();
            Arb.Register<InquiryGenerators>();
            Arb.Register<RecordListGenerators>();

            var genList = new List<Gen<LifeSituationDocument>>
            {
                Arb.From<SingleRecordUpdate<ChildrenRecord>>().Generator.Select(x => x as LifeSituationDocument),

                Arb.From<SingleRecordUpdate<CitizenshipRecord>>().Generator.Select(x => x as LifeSituationDocument),

                Arb.From<SingleRecordUpdate<DomicileRecord>>().Generator.Select(x => x as LifeSituationDocument),

                Arb.From<Inquiry>().Generator.Select(x => x as LifeSituationDocument),

                Arb.From<SingleRecordUpdate<MigrationStatusRecord>>().Generator.Select(x => x as LifeSituationDocument),

                Arb.From<SingleRecordUpdate<RegistrationStatusRecord>>().Generator.Select(x => x as LifeSituationDocument),

                Arb.From<MultiRecordsUpdate<EducationLevelRecord>>().Generator.Select(x => x as LifeSituationDocument),

                Arb.From<MultiRecordsUpdate<SpecialityRecord>>().Generator.Select(x => x as LifeSituationDocument)
            };


            var mapper = BuildMapper();
            Prop.ForAll(Gen.OneOf(genList).ToArbitrary(),
            src =>
                {
                    var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.LifeSituationDocument>(src);
                    var result = mapper.Map<LifeSituationDocument>(dto);

                    result.Should().BeEquivalentTo(src);
                })
                .QuickCheckThrowOnFailure();
        }

    }
}

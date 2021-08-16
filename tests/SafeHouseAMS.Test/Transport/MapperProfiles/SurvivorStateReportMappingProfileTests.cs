using AutoMapper;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.Transport.MapperProfiles;

namespace SafeHouseAMS.Test.Transport.MapperProfiles
{
    public class SurvivorStateReportMappingProfileTests
    {
        private Mapper BuildMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddMaps(typeof(SurvivorStateReportMappingProfile).Assembly));
            return new(cfg);
        }

        [Property]
        public void SurvivorStateReport_WhenRoundTripped_DoesNotChanges()
        {
            Arb.Register<NotNullStringsGenerators>();
            Arb.Register<RecordListGenerators>();
            var mapper = BuildMapper();

            Prop.ForAll<SurvivorStateReport>(src =>
                {
                    var dto =
                        mapper.Map<SafeHouseAMS.Transport.Protos.Models.LifeSituations.SurvivorStateReport>(src);
                    var result = mapper.Map<SurvivorStateReport>(dto);

                    result.SurvivorID.Should().Be(src.SurvivorID);

                    result.Citizenship.Should().BeEquivalentTo(src.Citizenship);
                    result.HasChangedCitizenship.Should().Be(src.HasChangedCitizenship);

                    result.Children.Should().BeEquivalentTo(src.Children);
                    result.HasChangedChildren.Should().Be(src.HasChangedChildren);

                    result.Registration.Should().BeEquivalentTo(src.Registration);
                    result.HasChangedRegistration.Should().Be(src.HasChangedRegistration);

                    result.Migration.Should().BeEquivalentTo(src.Migration);
                    result.HasChangedMigration.Should().Be(src.HasChangedMigration);

                    result.Domicile.Should().BeEquivalentTo(src.Domicile);
                    result.HasChangedDomicile.Should().Be(src.HasChangedDomicile);

                    result.Education.Should().BeEquivalentTo(src.Education);
                    result.HasChangedEducation.Should().Be(src.HasChangedEducation);

                    result.Specialities.Should().BeEquivalentTo(src.Specialities);
                    result.HasChangedSpecialities.Should().Be(src.HasChangedSpecialities);
                }).QuickCheckThrowOnFailure();
        }
    }
}

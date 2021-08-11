using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SafeHouseAMS.DataLayer.Models.LifeSituations;

namespace SafeHouseAMS.DataLayer.Configuration
{
    internal class LifeSituationsDocumentConfiguration : IEntityTypeConfiguration<LifeSituationDocumentDAL>
    {

        public void Configure(EntityTypeBuilder<LifeSituationDocumentDAL> builder)
        {
            builder.ToTable("LifeSituationDocuments").HasComment("Документы изменения жизненных ситуаций");

            builder.Property(x => x.ID).HasComment("Идентификатор записи - ПК");
            builder.Property(x => x.IsDeleted).HasComment("Признак удалённой записи");
            builder.Property(x => x.Created).HasComment("Дата-время создания записи");
            builder.Property(x => x.LastEdit).HasComment("Дата-время последнего редактирования записи");
            builder.Property(x => x.DocumentDate).HasComment("Дата документа");
            builder.Property(x => x.SurvivorID).HasComment("Внешний ключ - пострадавший к которому относится запись");

            builder.HasKey(x => x.ID);
            builder.HasIndex(x => x.IsDeleted).IsUnique(false);
            builder.HasIndex(x => x.Created).IsUnique(false);
            builder.HasIndex(x => x.LastEdit).IsUnique(false);
            builder.HasIndex(x => x.DocumentDate).IsUnique(false);

            builder
                .HasOne(x => x.Survivor)
                .WithMany()
                .HasForeignKey(x => x.SurvivorID)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder
                .HasDiscriminator<string>("DocumentType")
                .HasValue<InquiryDAL>("Inquiry")
                .HasValue<CitizenshipChangeDAL>("CitizenshipChange")
                .HasValue<ChildrenUpdateDAL>("ChildrenUpdate")
                .HasValue<DomicileUpdateDAL>("DomicileUpdate")
                .HasValue<EducationLevelUpdateDAL>("EducationUpdate")
                .HasValue<MigrationStatusUpdateDAL>("MigrationStatusUpdate")
                .HasValue<RegistrationStatusUpdateDAL>("RegistrationStatusUpdate")
                .HasValue<SpecialitiesUpdateDAL>("SpecialitiesUpdate");

        }
    }

    internal class InquiryDocumentConfiguration : IEntityTypeConfiguration<InquiryDAL>
    {

        public void Configure(EntityTypeBuilder<InquiryDAL> builder)
        {
            builder.HasBaseType<LifeSituationDocumentDAL>();

            builder.Property(x => x.IsJuvenile).HasComment("Несовершеннолетний в момент обращения");

            builder.Property(x => x.IsSelfInquiry).HasComment("Самообращение");
            builder.Property(x => x.SelfInquirySourcesMask).HasComment("Битовая маска каналов самообращения");
            builder.Property(x => x.IsForwardedBySurvivor).HasComment("Перенаправлен другим пострадавшим");
            builder.Property(x => x.ForwardedBySurvivor).HasComment("Детали направления другим пострадавшим");
            builder.Property(x => x.IsForwardedByPerson).HasComment("Перенаправлен другим человеком");
            builder.Property(x => x.ForwardedByPerson).HasComment("Детали направления другим человеком");
            builder.Property(x => x.IsForwardedByOrganization).HasComment("Направлен организацией");
            builder.Property(x => x.ForwardedByOrgannization).HasComment("Какая организация направила");

            builder.Property(x => x.WorkingExperience).HasComment("Описание опыта работы");
            builder.Ignore(x => x.Citizenship);
            builder.Ignore(x => x.Domicile);
            builder.Ignore(x => x.HasChildren);
            builder.Ignore(x => x.EducationLevel);
            builder.Ignore(x => x.Specialities);
            builder.Ignore(x => x.MigrationStatus);
            builder.Ignore(x => x.RegistrationStatus);

            builder.Property(x => x.HasAddiction).HasComment("Наличие зависимости");
            builder.Property(x => x.AddictionKind).HasComment("Тип зависимости");
            builder.Property(x => x.ChildhoodViolence).HasComment("Насилие в детстве");
            builder.Property(x => x.Homelessness).HasComment("Бездомность");
            builder.Property(x => x.Migration).HasComment("Мигрант_ка");
            builder.Property(x => x.OrphanageExperience).HasComment("Опыт интернатного учреждения");
            builder.Property(x => x.HasOtherVulnerability).HasComment("другие факторы уязвимости");
            builder.Property(x => x.OtherVulnerabilityDetails).HasComment("Описание других факторов уязвимости");
            builder.Property(x => x.HealthStatusVulnerabilityMask).HasComment("битовая маска уязвимости по здоровью");
            builder.Property(x => x.OtherHealthStatusVulnerabilityDetail).HasComment("Детали уязвимости по здоровью");

            builder.HasIndex(x => x.ForwardedByOrgannization).IsUnique(false);
        }
    }

    internal class CitizenshipChangeConfiguration : IEntityTypeConfiguration<CitizenshipChangeDAL>
    {
        public void Configure(EntityTypeBuilder<CitizenshipChangeDAL> builder)
        {
            builder.HasBaseType<LifeSituationDocumentDAL>();

            builder.Ignore(x => x.Record);
        }
    }

    internal class ChildrenUpdateConfiguration : IEntityTypeConfiguration<ChildrenUpdateDAL>
    {
        public void Configure(EntityTypeBuilder<ChildrenUpdateDAL> builder)
        {
            builder.HasBaseType<LifeSituationDocumentDAL>();

            builder.Ignore(x => x.Record);
        }
    }

    internal class DomicileUpdateConfiguration : IEntityTypeConfiguration<DomicileUpdateDAL>
    {
        public void Configure(EntityTypeBuilder<DomicileUpdateDAL> builder)
        {
            builder.HasBaseType<LifeSituationDocumentDAL>();

            builder.Ignore(x => x.Record);
        }
    }

    internal class EducationLevelUpdateConfiguration : IEntityTypeConfiguration<EducationLevelUpdateDAL>
    {
        public void Configure(EntityTypeBuilder<EducationLevelUpdateDAL> builder)
        {
            builder.HasBaseType<LifeSituationDocumentDAL>();

            builder.Ignore(x => x.Records);
        }
    }

    internal class MigrationStatusUpdateConfiguration : IEntityTypeConfiguration<MigrationStatusUpdateDAL>
    {
        public void Configure(EntityTypeBuilder<MigrationStatusUpdateDAL> builder)
        {
            builder.HasBaseType<LifeSituationDocumentDAL>();

            builder.Ignore(x => x.Record);
        }
    }

    internal class RegistrationStatusUpdateConfiguration : IEntityTypeConfiguration<RegistrationStatusUpdateDAL>
    {
        public void Configure(EntityTypeBuilder<RegistrationStatusUpdateDAL> builder)
        {
            builder.HasBaseType<LifeSituationDocumentDAL>();

            builder.Ignore(x => x.Record);
        }
    }

    internal class SpecialitiesUpdateConfiguration : IEntityTypeConfiguration<SpecialitiesUpdateDAL>
    {
        public void Configure(EntityTypeBuilder<SpecialitiesUpdateDAL> builder)
        {
            builder.HasBaseType<LifeSituationDocumentDAL>();

            builder.Ignore(x => x.Records);
        }
    }
}

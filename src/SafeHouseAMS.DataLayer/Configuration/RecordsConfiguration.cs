using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SafeHouseAMS.DataLayer.Models.LifeSituations;

namespace SafeHouseAMS.DataLayer.Configuration
{
    internal class RecordsConfiguration : IEntityTypeConfiguration<BaseRecordDAL>
    {

        public void Configure(EntityTypeBuilder<BaseRecordDAL> builder)
        {
            builder.ToTable("Records").HasComment("Записи изменения жизненных ситуаций");

            builder.Property(x => x.ID).HasComment("Идентификатор записи");
            builder.Property(x => x.DocumentID).HasComment("Ссылка на документ, породивший запись");
            builder.Property(x => x.Content).HasColumnType("jsonb").HasComment("Содержимое записи в виде json");

            builder.HasKey(x => x.ID);
            builder
                .HasOne(x => x.Document)
                .WithMany(x => x.Records)
                .HasForeignKey(x => x.DocumentID)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .HasDiscriminator<string>("RecordType")
                .HasValue<ChildrenRecordDAL>("HasChildren")
                .HasValue<CitizenshipRecordDAL>("Citizenship")
                .HasValue<DomicileRecordDAL>("Domicile")
                .HasValue<EducationLevelRecordDAL>("EducationLevel")
                .HasValue<SpecialityRecordDAL>("Speciality");
                
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SafeHouseAMS.DataLayer.Models;

namespace SafeHouseAMS.DataLayer.Configuration
{
    internal class SurvivorDALConfiguration : IEntityTypeConfiguration<SurvivorDAL>
    {
        public void Configure(EntityTypeBuilder<SurvivorDAL> builder)
        {
            builder.ToTable("Survivors").HasComment("Пострадавшие");

            builder.Property(x => x.ID).HasComment("Идентификатор записи");
            builder.Property(x => x.IsDeleted).HasComment("Признак удаленной записи");
            builder.Property(x => x.Created).HasComment("Дата создания");
            builder.Property(x => x.LastEdit).HasComment("Дата последнего редактирования");
            builder.Property(x => x.Num).ValueGeneratedOnAdd().HasComment("Номер карточки");
            builder.Property(x => x.Name).HasComment("Имя");
            builder.Property(x => x.Sex).HasComment("Пол");
            builder.Property(x => x.OtherSex).HasComment("Уточнение пола");
            builder.Property(x => x.BirthDateAccurate).HasComment("Точная дата рождения");
            builder.Property(x => x.BirthDateCalculated).HasComment("Вычисленная дата рождения");

            builder.HasKey(x => x.ID);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SafeHouseAMS.DataLayer.Models.Survivors;

namespace SafeHouseAMS.DataLayer.Configuration
{
    internal class SurvivorDALConfiguration : BaseDalEntityConfiguration<SurvivorDAL>
    {
        public override void Configure(EntityTypeBuilder<SurvivorDAL> builder)
        {
            base.Configure(builder);
            builder.ToTable("Survivors").HasComment("Пострадавшие");

            builder.Property(x => x.Num).ValueGeneratedOnAdd().HasComment("Номер карточки");
            builder.Property(x => x.Name).HasComment("Имя");
            builder.Property(x => x.Sex).HasComment("Пол");
            builder.Property(x => x.OtherSex).HasComment("Уточнение пола");
            builder.Property(x => x.BirthDateAccurate).HasComment("Точная дата рождения");
            builder.Property(x => x.BirthDateCalculated).HasComment("Вычисленная дата рождения");
        }
    }
}

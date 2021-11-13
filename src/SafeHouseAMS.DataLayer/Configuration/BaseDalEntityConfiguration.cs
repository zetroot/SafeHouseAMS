using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SafeHouseAMS.DataLayer.Models;

namespace SafeHouseAMS.DataLayer.Configuration
{
    internal abstract class BaseDalEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseDalModel
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.ID).HasComment("Идентификатор записи");
            builder.Property(x => x.IsDeleted).HasComment("Признак удаленной записи");
            builder.Property(x => x.Created).HasComment("Дата создания");
            builder.Property(x => x.LastEdit).HasComment("Дата последнего редактирования");

            builder.HasKey(x => x.ID);

            builder.HasIndex(x => x.IsDeleted).IsUnique(false);
            builder.HasIndex(x => x.Created).IsUnique(false);
            builder.HasIndex(x => x.LastEdit).IsUnique(false);
        }
    }
}

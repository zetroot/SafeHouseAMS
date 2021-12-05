using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SafeHouseAMS.DataLayer.Models.AssistanceRequests;

namespace SafeHouseAMS.DataLayer.Configuration
{
    internal class AssistanceRequestsConfiguration : BaseDalEntityConfiguration<AssistanceRequestDAL>
    {
        public override void Configure(EntityTypeBuilder<AssistanceRequestDAL> builder)
        {
            base.Configure(builder);
            builder.ToTable("AssistanceRequests").HasComment("Запросы помощи");

            builder.Property(x => x.SurvivorID).HasComment("Идентификатор пострадавшего");
            builder.Property(x => x.AssistanceKind).HasComment("Тип запрашиваемой помощи");
            builder.Property(x => x.Details).HasComment("Дополнительная информация по запросу");
            builder.Property(x => x.IsAccomplished).HasComment("Признак выполненного запроса");
            builder.Property(x => x.DocumentDate).HasComment("Дата запроса помощи").HasDefaultValueSql("now()");

            builder.HasIndex(x => x.IsAccomplished);
            builder.HasIndex(x => x.DocumentDate);

            builder.HasOne(x => x.Survivor).WithMany().HasForeignKey(x => x.SurvivorID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    internal class AssistanceActConfiguration : BaseDalEntityConfiguration<AssistanceActDAL>
    {
        public override void Configure(EntityTypeBuilder<AssistanceActDAL> builder)
        {
            base.Configure(builder);
            builder.ToTable("AssistanceActs").HasComment("Акты оказания помощи");

            builder.Property(x => x.RequestID).HasComment("Идентификатор запроса помощи");
            builder.Property(x => x.Details).HasComment("Дополнительная информация по этому акту помощи");
            builder.Property(x => x.WorkHours).HasComment("Потрачено часов");
            builder.Property(x => x.Money).HasComment("Потрачено денег");
            builder.Property(x => x.DocumentDate).HasComment("Дата совершения акта помощи").HasDefaultValueSql("now()");

            builder.HasIndex(x => x.DocumentDate);

            builder.HasOne(x => x.Request).WithMany(x => x.AssistanceActs).HasForeignKey(x => x.RequestID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

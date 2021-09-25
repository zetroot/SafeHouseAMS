using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SafeHouseAMS.DataLayer.Models.ExploitationEpisodes;

namespace SafeHouseAMS.DataLayer.Configuration
{
    internal class EpisodesDALConfiguration : IEntityTypeConfiguration<EpisodeDAL>
    {

        public void Configure(EntityTypeBuilder<EpisodeDAL> builder)
        {
            builder.ToTable("Episodes").HasComment("Эпизоды эксплуатации");

            builder.Property(x => x.ID).HasComment("Идентификатор записи");
            builder.Property(x => x.IsDeleted).HasComment("Признак удаленной записи");
            builder.Property(x => x.Created).HasComment("Дата создания");
            builder.Property(x => x.LastEdit).HasComment("Дата последнего редактирования");

            builder.Property(x => x.SurvivorID).HasComment("Идентификатор пострадавшего");
            builder.Property(x => x.Involvement).HasComment("Обращение по причине вовлечения");
            builder.Property(x => x.Cse).HasComment("КСЭ");
            builder.Property(x => x.CseType).HasComment("Тип КСЭ");
            builder.Property(x => x.ForcedLabour).HasComment("Принудительный труд");
            builder.Property(x => x.ForcedLabourType).HasComment("Тип принкдительного труда");
            builder.Property(x => x.ForcedMarriage).HasComment("Принудительный брак");
            builder.Property(x => x.ForcedMarriageKind).HasComment("Статус принудительного брака");
            builder.Property(x => x.Cre).HasComment("КРЭ");
            builder.Property(x => x.Begging).HasComment("Попрошайничество");
            builder.Property(x => x.ForcedCriminalActivity).HasComment("Принудительная криминальная деятельность");
            builder.Property(x => x.CriminalActivityType).HasComment("вид принудительной криминальной деятельности");
            builder.Property(x => x.OtherExploitationKind).HasComment("Другой вид эксплуатации");
            builder.Property(x => x.SexualViolence).HasComment("Сексуальное насилие");
            builder.Property(x => x.DomesticViolence).HasComment("Домашнее насилие");
            builder.Property(x => x.OtherViolenceKind).HasComment("Другой вид насилия");
            builder.Property(x => x.ContactReasonDescriptions).HasColumnType("jsonb").HasComment("Описания причины обращения");
            builder.Property(x => x.Place).HasComment("Место эксплуатации");
            builder.Property(x => x.InvolvementDescription).HasComment("Кем и как вовлекалась");
            builder.Property(x => x.WasJuvenile).HasComment("Несовершеннолетняя на момент вовлечения");
            builder.Property(x => x.Duration).HasComment("Длительность эксплуатации");
            builder.Property(x => x.ControlMethods).HasComment("Методы контроля");
            builder.Property(x => x.DebtKind).HasComment("Тип долговой кабалы");
            builder.Property(x => x.OtherControlMethodDetails).HasComment("Уточнение других методов контроля");
            builder.Property(x => x.EscapeStatus).HasComment("Статус освобождения");

            builder
                .HasOne(x => x.Survivor)
                .WithMany()
                .HasForeignKey(x => x.SurvivorID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(x => x.ID);
            builder.HasIndex(x => x.IsDeleted);
        }
    }
}

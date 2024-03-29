﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SafeHouseAMS.DataLayer;

#nullable disable

namespace SafeHouseAMS.DataLayer.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20211205170229_AssistanceDocumentDate")]
    partial class AssistanceDocumentDate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.AssistanceRequests.AssistanceActDAL", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasComment("Идентификатор записи");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата создания");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Дополнительная информация по этому акту помощи");

                    b.Property<DateTime>("DocumentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()")
                        .HasComment("Дата совершения акта помощи");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasComment("Признак удаленной записи");

                    b.Property<DateTime>("LastEdit")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата последнего редактирования");

                    b.Property<decimal>("Money")
                        .HasColumnType("numeric")
                        .HasComment("Потрачено денег");

                    b.Property<Guid>("RequestID")
                        .HasColumnType("uuid")
                        .HasComment("Идентификатор запроса помощи");

                    b.Property<decimal>("WorkHours")
                        .HasColumnType("numeric")
                        .HasComment("Потрачено часов");

                    b.HasKey("ID");

                    b.HasIndex("Created");

                    b.HasIndex("DocumentDate");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("LastEdit");

                    b.HasIndex("RequestID");

                    b.ToTable("AssistanceActs", "public");

                    b.HasComment("Акты оказания помощи");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.AssistanceRequests.AssistanceRequestDAL", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasComment("Идентификатор записи");

                    b.Property<int>("AssistanceKind")
                        .HasColumnType("integer")
                        .HasComment("Тип запрашиваемой помощи");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата создания");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Дополнительная информация по запросу");

                    b.Property<DateTime>("DocumentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()")
                        .HasComment("Дата запроса помощи");

                    b.Property<bool>("IsAccomplished")
                        .HasColumnType("boolean")
                        .HasComment("Признак выполненного запроса");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasComment("Признак удаленной записи");

                    b.Property<DateTime>("LastEdit")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата последнего редактирования");

                    b.Property<Guid>("SurvivorID")
                        .HasColumnType("uuid")
                        .HasComment("Идентификатор пострадавшего");

                    b.HasKey("ID");

                    b.HasIndex("Created");

                    b.HasIndex("DocumentDate");

                    b.HasIndex("IsAccomplished");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("LastEdit");

                    b.HasIndex("SurvivorID");

                    b.ToTable("AssistanceRequests", "public");

                    b.HasComment("Запросы помощи");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.ExploitationEpisodes.EpisodeDAL", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasComment("Идентификатор записи");

                    b.Property<bool>("Begging")
                        .HasColumnType("boolean")
                        .HasComment("Попрошайничество");

                    b.Property<string>("ContactReasonDescriptions")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasComment("Описания причины обращения");

                    b.Property<int>("ControlMethods")
                        .HasColumnType("integer")
                        .HasComment("Методы контроля");

                    b.Property<bool>("Cre")
                        .HasColumnType("boolean")
                        .HasComment("КРЭ");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата создания");

                    b.Property<int>("CriminalActivityType")
                        .HasColumnType("integer")
                        .HasComment("вид принудительной криминальной деятельности");

                    b.Property<bool>("Cse")
                        .HasColumnType("boolean")
                        .HasComment("КСЭ");

                    b.Property<int>("CseType")
                        .HasColumnType("integer")
                        .HasComment("Тип КСЭ");

                    b.Property<int?>("DebtKind")
                        .HasColumnType("integer")
                        .HasComment("Тип долговой кабалы");

                    b.Property<bool>("DomesticViolence")
                        .HasColumnType("boolean")
                        .HasComment("Домашнее насилие");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval")
                        .HasComment("Длительность эксплуатации");

                    b.Property<int>("EscapeStatus")
                        .HasColumnType("integer")
                        .HasComment("Статус освобождения");

                    b.Property<bool>("ForcedCriminalActivity")
                        .HasColumnType("boolean")
                        .HasComment("Принудительная криминальная деятельность");

                    b.Property<bool>("ForcedLabour")
                        .HasColumnType("boolean")
                        .HasComment("Принудительный труд");

                    b.Property<int>("ForcedLabourType")
                        .HasColumnType("integer")
                        .HasComment("Тип принкдительного труда");

                    b.Property<bool>("ForcedMarriage")
                        .HasColumnType("boolean")
                        .HasComment("Принудительный брак");

                    b.Property<int>("ForcedMarriageKind")
                        .HasColumnType("integer")
                        .HasComment("Статус принудительного брака");

                    b.Property<bool>("Involvement")
                        .HasColumnType("boolean")
                        .HasComment("Обращение по причине вовлечения");

                    b.Property<string>("InvolvementDescription")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Кем и как вовлекалась");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasComment("Признак удаленной записи");

                    b.Property<DateTime>("LastEdit")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата последнего редактирования");

                    b.Property<string>("OtherControlMethodDetails")
                        .HasColumnType("text")
                        .HasComment("Уточнение других методов контроля");

                    b.Property<bool>("OtherExploitationKind")
                        .HasColumnType("boolean")
                        .HasComment("Другой вид эксплуатации");

                    b.Property<bool>("OtherViolenceKind")
                        .HasColumnType("boolean")
                        .HasComment("Другой вид насилия");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Место эксплуатации");

                    b.Property<bool>("SexualViolence")
                        .HasColumnType("boolean")
                        .HasComment("Сексуальное насилие");

                    b.Property<Guid>("SurvivorID")
                        .HasColumnType("uuid")
                        .HasComment("Идентификатор пострадавшего");

                    b.Property<bool>("WasJuvenile")
                        .HasColumnType("boolean")
                        .HasComment("Несовершеннолетняя на момент вовлечения");

                    b.HasKey("ID");

                    b.HasIndex("Created");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("LastEdit");

                    b.HasIndex("SurvivorID");

                    b.ToTable("Episodes", "public");

                    b.HasComment("Эпизоды эксплуатации");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.LifeSituations.BaseRecordDAL", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasComment("Идентификатор записи");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasComment("Содержимое записи в виде json");

                    b.Property<Guid>("DocumentID")
                        .HasColumnType("uuid")
                        .HasComment("Ссылка на документ, породивший запись");

                    b.Property<string>("RecordType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("DocumentID");

                    b.ToTable("Records", "public");

                    b.HasDiscriminator<string>("RecordType").HasValue("BaseRecordDAL");

                    b.HasComment("Записи изменения жизненных ситуаций");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.LifeSituations.LifeSituationDocumentDAL", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasComment("Идентификатор записи");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата создания");

                    b.Property<DateTime>("DocumentDate")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата документа");

                    b.Property<string>("DocumentType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasComment("Признак удаленной записи");

                    b.Property<DateTime>("LastEdit")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата последнего редактирования");

                    b.Property<Guid>("SurvivorID")
                        .HasColumnType("uuid")
                        .HasComment("Внешний ключ - пострадавший к которому относится запись");

                    b.HasKey("ID");

                    b.HasIndex("Created");

                    b.HasIndex("DocumentDate");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("LastEdit");

                    b.HasIndex("SurvivorID");

                    b.ToTable("LifeSituationDocuments", "public");

                    b.HasDiscriminator<string>("DocumentType").HasValue("LifeSituationDocumentDAL");

                    b.HasComment("Документы изменения жизненных ситуаций");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.Survivors.SurvivorDAL", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasComment("Идентификатор записи");

                    b.Property<DateTime?>("BirthDateAccurate")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Точная дата рождения");

                    b.Property<DateTime?>("BirthDateCalculated")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Вычисленная дата рождения");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата создания");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasComment("Признак удаленной записи");

                    b.Property<DateTime>("LastEdit")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата последнего редактирования");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Имя");

                    b.Property<int>("Num")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasComment("Номер карточки");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Num"));

                    b.Property<string>("OtherSex")
                        .HasColumnType("text")
                        .HasComment("Уточнение пола");

                    b.Property<int>("Sex")
                        .HasColumnType("integer")
                        .HasComment("Пол");

                    b.HasKey("ID");

                    b.HasIndex("Created");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("LastEdit");

                    b.ToTable("Survivors", "public");

                    b.HasComment("Пострадавшие");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.LifeSituations.ChildrenRecordDAL", b =>
                {
                    b.HasBaseType("SafeHouseAMS.DataLayer.Models.LifeSituations.BaseRecordDAL");

                    b.HasDiscriminator().HasValue("HasChildren");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.LifeSituations.ChildrenUpdateDAL", b =>
                {
                    b.HasBaseType("SafeHouseAMS.DataLayer.Models.LifeSituations.LifeSituationDocumentDAL");

                    b.HasDiscriminator().HasValue("ChildrenUpdate");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.LifeSituations.CitizenshipChangeDAL", b =>
                {
                    b.HasBaseType("SafeHouseAMS.DataLayer.Models.LifeSituations.LifeSituationDocumentDAL");

                    b.HasDiscriminator().HasValue("CitizenshipChange");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.LifeSituations.CitizenshipRecordDAL", b =>
                {
                    b.HasBaseType("SafeHouseAMS.DataLayer.Models.LifeSituations.BaseRecordDAL");

                    b.HasDiscriminator().HasValue("Citizenship");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.LifeSituations.DomicileRecordDAL", b =>
                {
                    b.HasBaseType("SafeHouseAMS.DataLayer.Models.LifeSituations.BaseRecordDAL");

                    b.HasDiscriminator().HasValue("Domicile");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.LifeSituations.DomicileUpdateDAL", b =>
                {
                    b.HasBaseType("SafeHouseAMS.DataLayer.Models.LifeSituations.LifeSituationDocumentDAL");

                    b.HasDiscriminator().HasValue("DomicileUpdate");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.LifeSituations.EducationLevelRecordDAL", b =>
                {
                    b.HasBaseType("SafeHouseAMS.DataLayer.Models.LifeSituations.BaseRecordDAL");

                    b.HasDiscriminator().HasValue("EducationLevel");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.LifeSituations.EducationLevelUpdateDAL", b =>
                {
                    b.HasBaseType("SafeHouseAMS.DataLayer.Models.LifeSituations.LifeSituationDocumentDAL");

                    b.HasDiscriminator().HasValue("EducationUpdate");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.LifeSituations.InquiryDAL", b =>
                {
                    b.HasBaseType("SafeHouseAMS.DataLayer.Models.LifeSituations.LifeSituationDocumentDAL");

                    b.Property<string>("AddictionKind")
                        .HasColumnType("text")
                        .HasComment("Тип зависимости");

                    b.Property<bool>("ChildhoodViolence")
                        .HasColumnType("boolean")
                        .HasComment("Насилие в детстве");

                    b.Property<string>("ForwardedByOrgannization")
                        .HasColumnType("text")
                        .HasComment("Какая организация направила");

                    b.Property<string>("ForwardedByPerson")
                        .HasColumnType("text")
                        .HasComment("Детали направления другим человеком");

                    b.Property<string>("ForwardedBySurvivor")
                        .HasColumnType("text")
                        .HasComment("Детали направления другим пострадавшим");

                    b.Property<bool>("HasAddiction")
                        .HasColumnType("boolean")
                        .HasComment("Наличие зависимости");

                    b.Property<bool>("HasOtherVulnerability")
                        .HasColumnType("boolean")
                        .HasComment("другие факторы уязвимости");

                    b.Property<int>("HealthStatusVulnerabilityMask")
                        .HasColumnType("integer")
                        .HasComment("битовая маска уязвимости по здоровью");

                    b.Property<bool>("Homelessness")
                        .HasColumnType("boolean")
                        .HasComment("Бездомность");

                    b.Property<bool>("IsForwardedByOrganization")
                        .HasColumnType("boolean")
                        .HasComment("Направлен организацией");

                    b.Property<bool>("IsForwardedByPerson")
                        .HasColumnType("boolean")
                        .HasComment("Перенаправлен другим человеком");

                    b.Property<bool>("IsForwardedBySurvivor")
                        .HasColumnType("boolean")
                        .HasComment("Перенаправлен другим пострадавшим");

                    b.Property<bool>("IsJuvenile")
                        .HasColumnType("boolean")
                        .HasComment("Несовершеннолетний в момент обращения");

                    b.Property<bool>("IsSelfInquiry")
                        .HasColumnType("boolean")
                        .HasComment("Самообращение");

                    b.Property<bool>("Migration")
                        .HasColumnType("boolean")
                        .HasComment("Мигрант_ка");

                    b.Property<bool>("OrphanageExperience")
                        .HasColumnType("boolean")
                        .HasComment("Опыт интернатного учреждения");

                    b.Property<string>("OtherHealthStatusVulnerabilityDetail")
                        .HasColumnType("text")
                        .HasComment("Детали уязвимости по здоровью");

                    b.Property<string>("OtherVulnerabilityDetails")
                        .HasColumnType("text")
                        .HasComment("Описание других факторов уязвимости");

                    b.Property<int?>("SelfInquirySourcesMask")
                        .HasColumnType("integer")
                        .HasComment("Битовая маска каналов самообращения");

                    b.Property<string>("WorkingExperience")
                        .HasColumnType("text")
                        .HasComment("Описание опыта работы");

                    b.HasIndex("ForwardedByOrgannization");

                    b.HasDiscriminator().HasValue("Inquiry");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.LifeSituations.MigrationStatusRecordDAL", b =>
                {
                    b.HasBaseType("SafeHouseAMS.DataLayer.Models.LifeSituations.BaseRecordDAL");

                    b.HasDiscriminator().HasValue("MigrationStatus");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.LifeSituations.MigrationStatusUpdateDAL", b =>
                {
                    b.HasBaseType("SafeHouseAMS.DataLayer.Models.LifeSituations.LifeSituationDocumentDAL");

                    b.HasDiscriminator().HasValue("MigrationStatusUpdate");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.LifeSituations.RegistrationStatusRecordDAL", b =>
                {
                    b.HasBaseType("SafeHouseAMS.DataLayer.Models.LifeSituations.BaseRecordDAL");

                    b.HasDiscriminator().HasValue("RegistrationStatus");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.LifeSituations.RegistrationStatusUpdateDAL", b =>
                {
                    b.HasBaseType("SafeHouseAMS.DataLayer.Models.LifeSituations.LifeSituationDocumentDAL");

                    b.HasDiscriminator().HasValue("RegistrationStatusUpdate");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.LifeSituations.SpecialitiesUpdateDAL", b =>
                {
                    b.HasBaseType("SafeHouseAMS.DataLayer.Models.LifeSituations.LifeSituationDocumentDAL");

                    b.HasDiscriminator().HasValue("SpecialitiesUpdate");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.LifeSituations.SpecialityRecordDAL", b =>
                {
                    b.HasBaseType("SafeHouseAMS.DataLayer.Models.LifeSituations.BaseRecordDAL");

                    b.HasDiscriminator().HasValue("Speciality");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.AssistanceRequests.AssistanceActDAL", b =>
                {
                    b.HasOne("SafeHouseAMS.DataLayer.Models.AssistanceRequests.AssistanceRequestDAL", "Request")
                        .WithMany("AssistanceActs")
                        .HasForeignKey("RequestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.AssistanceRequests.AssistanceRequestDAL", b =>
                {
                    b.HasOne("SafeHouseAMS.DataLayer.Models.Survivors.SurvivorDAL", "Survivor")
                        .WithMany()
                        .HasForeignKey("SurvivorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survivor");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.ExploitationEpisodes.EpisodeDAL", b =>
                {
                    b.HasOne("SafeHouseAMS.DataLayer.Models.Survivors.SurvivorDAL", "Survivor")
                        .WithMany()
                        .HasForeignKey("SurvivorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survivor");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.LifeSituations.BaseRecordDAL", b =>
                {
                    b.HasOne("SafeHouseAMS.DataLayer.Models.LifeSituations.LifeSituationDocumentDAL", "Document")
                        .WithMany("AllRecords")
                        .HasForeignKey("DocumentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.LifeSituations.LifeSituationDocumentDAL", b =>
                {
                    b.HasOne("SafeHouseAMS.DataLayer.Models.Survivors.SurvivorDAL", "Survivor")
                        .WithMany()
                        .HasForeignKey("SurvivorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survivor");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.AssistanceRequests.AssistanceRequestDAL", b =>
                {
                    b.Navigation("AssistanceActs");
                });

            modelBuilder.Entity("SafeHouseAMS.DataLayer.Models.LifeSituations.LifeSituationDocumentDAL", b =>
                {
                    b.Navigation("AllRecords");
                });
#pragma warning restore 612, 618
        }
    }
}

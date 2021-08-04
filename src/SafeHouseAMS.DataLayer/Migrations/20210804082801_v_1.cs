using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SafeHouseAMS.DataLayer.Migrations
{
    [ExcludeFromCodeCoverage]
    internal partial class v_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "Survivors",
                schema: "public",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор записи"),
                    Num = table.Column<int>(type: "integer", nullable: false, comment: "Номер карточки")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false, comment: "Имя"),
                    Sex = table.Column<int>(type: "integer", nullable: false, comment: "Пол"),
                    OtherSex = table.Column<string>(type: "text", nullable: true, comment: "Уточнение пола"),
                    BirthDateAccurate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Точная дата рождения"),
                    BirthDateCalculated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Вычисленная дата рождения"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак удаленной записи"),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата создания"),
                    LastEdit = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата последнего редактирования")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Survivors", x => x.ID);
                },
                comment: "Пострадавшие");

            migrationBuilder.CreateTable(
                name: "LifeSituationDocuments",
                schema: "public",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор записи - ПК"),
                    DocumentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата документа"),
                    SurvivorID = table.Column<Guid>(type: "uuid", nullable: false, comment: "Внешний ключ - пострадавший к которому относится запись"),
                    DocumentType = table.Column<string>(type: "text", nullable: false),
                    IsJuvenile = table.Column<bool>(type: "boolean", nullable: true, comment: "Несовершеннолетний в момент обращения"),
                    IsSelfInquiry = table.Column<bool>(type: "boolean", nullable: true, comment: "Самообращение"),
                    SelfInquirySourcesMask = table.Column<int>(type: "integer", nullable: true, comment: "Битовая маска каналов самообращения"),
                    IsForwardedBySurvivor = table.Column<bool>(type: "boolean", nullable: true, comment: "Перенаправлен другим пострадавшим"),
                    ForwardedBySurvivor = table.Column<string>(type: "text", nullable: true, comment: "Детали направления другим пострадавшим"),
                    IsForwardedByPerson = table.Column<bool>(type: "boolean", nullable: true, comment: "Перенаправлен другим человеком"),
                    ForwardedByPerson = table.Column<string>(type: "text", nullable: true, comment: "Детали направления другим человеком"),
                    IsForwardedByOrganization = table.Column<bool>(type: "boolean", nullable: true, comment: "Направлен организацией"),
                    ForwardedByOrgannization = table.Column<string>(type: "text", nullable: true, comment: "Какая организация направила"),
                    WorkingExperience = table.Column<string>(type: "text", nullable: true, comment: "Описание опыта работы"),
                    HasAddiction = table.Column<bool>(type: "boolean", nullable: true, comment: "Наличие зависимости"),
                    AddictionKind = table.Column<string>(type: "text", nullable: true, comment: "Тип зависимости"),
                    ChildhoodViolence = table.Column<bool>(type: "boolean", nullable: true, comment: "Насилие в детстве"),
                    Homelessness = table.Column<bool>(type: "boolean", nullable: true, comment: "Бездомность"),
                    Migration = table.Column<bool>(type: "boolean", nullable: true, comment: "Мигрант_ка"),
                    OrphanageExperience = table.Column<bool>(type: "boolean", nullable: true, comment: "Опыт интернатного учреждения"),
                    HasOtherVulnerability = table.Column<bool>(type: "boolean", nullable: true, comment: "другие факторы уязвимости"),
                    OtherVulnerabilityDetails = table.Column<string>(type: "text", nullable: true, comment: "Описание других факторов уязвимости"),
                    HealthStatusVulnerabilityMask = table.Column<int>(type: "integer", nullable: true, comment: "битовая маска уязвимости по здоровью"),
                    OtherHealthStatusVulnerabilityDetail = table.Column<string>(type: "text", nullable: true, comment: "Детали уязвимости по здоровью"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак удалённой записи"),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата-время создания записи"),
                    LastEdit = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата-время последнего редактирования записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeSituationDocuments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LifeSituationDocuments_Survivors_SurvivorID",
                        column: x => x.SurvivorID,
                        principalSchema: "public",
                        principalTable: "Survivors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Документы изменения жизненных ситуаций");

            migrationBuilder.CreateTable(
                name: "Records",
                schema: "public",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор записи"),
                    DocumentID = table.Column<Guid>(type: "uuid", nullable: false, comment: "Ссылка на документ, породивший запись"),
                    Content = table.Column<string>(type: "jsonb", nullable: false, comment: "Содержимое записи в виде json"),
                    RecordType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Records_LifeSituationDocuments_DocumentID",
                        column: x => x.DocumentID,
                        principalSchema: "public",
                        principalTable: "LifeSituationDocuments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Записи изменения жизненных ситуаций");

            migrationBuilder.CreateIndex(
                name: "IX_LifeSituationDocuments_Created",
                schema: "public",
                table: "LifeSituationDocuments",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_LifeSituationDocuments_DocumentDate",
                schema: "public",
                table: "LifeSituationDocuments",
                column: "DocumentDate");

            migrationBuilder.CreateIndex(
                name: "IX_LifeSituationDocuments_ForwardedByOrgannization",
                schema: "public",
                table: "LifeSituationDocuments",
                column: "ForwardedByOrgannization");

            migrationBuilder.CreateIndex(
                name: "IX_LifeSituationDocuments_IsDeleted",
                schema: "public",
                table: "LifeSituationDocuments",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_LifeSituationDocuments_LastEdit",
                schema: "public",
                table: "LifeSituationDocuments",
                column: "LastEdit");

            migrationBuilder.CreateIndex(
                name: "IX_LifeSituationDocuments_SurvivorID",
                schema: "public",
                table: "LifeSituationDocuments",
                column: "SurvivorID");

            migrationBuilder.CreateIndex(
                name: "IX_Records_DocumentID",
                schema: "public",
                table: "Records",
                column: "DocumentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Records",
                schema: "public");

            migrationBuilder.DropTable(
                name: "LifeSituationDocuments",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Survivors",
                schema: "public");
        }
    }
}

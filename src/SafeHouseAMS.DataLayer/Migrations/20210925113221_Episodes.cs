using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SafeHouseAMS.DataLayer.Migrations
{
    [ExcludeFromCodeCoverage]
    internal partial class Episodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Episodes",
                schema: "public",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор записи"),
                    SurvivorID = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор пострадавшего"),
                    Involvement = table.Column<bool>(type: "boolean", nullable: false, comment: "Обращение по причине вовлечения"),
                    Cse = table.Column<bool>(type: "boolean", nullable: false, comment: "КСЭ"),
                    CseType = table.Column<int>(type: "integer", nullable: false, comment: "Тип КСЭ"),
                    ForcedLabour = table.Column<bool>(type: "boolean", nullable: false, comment: "Принудительный труд"),
                    ForcedLabourType = table.Column<int>(type: "integer", nullable: false, comment: "Тип принкдительного труда"),
                    ForcedMarriage = table.Column<bool>(type: "boolean", nullable: false, comment: "Принудительный брак"),
                    ForcedMarriageKind = table.Column<int>(type: "integer", nullable: false, comment: "Статус принудительного брака"),
                    Cre = table.Column<bool>(type: "boolean", nullable: false, comment: "КРЭ"),
                    Begging = table.Column<bool>(type: "boolean", nullable: false, comment: "Попрошайничество"),
                    ForcedCriminalActivity = table.Column<bool>(type: "boolean", nullable: false, comment: "Принудительная криминальная деятельность"),
                    CriminalActivityType = table.Column<int>(type: "integer", nullable: false, comment: "вид принудительной криминальной деятельности"),
                    OtherExploitationKind = table.Column<bool>(type: "boolean", nullable: false, comment: "Другой вид эксплуатации"),
                    SexualViolence = table.Column<bool>(type: "boolean", nullable: false, comment: "Сексуальное насилие"),
                    DomesticViolence = table.Column<bool>(type: "boolean", nullable: false, comment: "Домашнее насилие"),
                    OtherViolenceKind = table.Column<bool>(type: "boolean", nullable: false, comment: "Другой вид насилия"),
                    ContactReasonDescriptions = table.Column<string>(type: "jsonb", nullable: false, comment: "Описания причины обращения"),
                    Place = table.Column<string>(type: "text", nullable: false, comment: "Место эксплуатации"),
                    InvolvementDescription = table.Column<string>(type: "text", nullable: false, comment: "Кем и как вовлекалась"),
                    WasJuvenile = table.Column<bool>(type: "boolean", nullable: false, comment: "Несовершеннолетняя на момент вовлечения"),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false, comment: "Длительность эксплуатации"),
                    ControlMethods = table.Column<int>(type: "integer", nullable: false, comment: "Методы контроля"),
                    DebtKind = table.Column<int>(type: "integer", nullable: true, comment: "Тип долговой кабалы"),
                    OtherControlMethodDetails = table.Column<string>(type: "text", nullable: true, comment: "Уточнение других методов контроля"),
                    EscapeStatus = table.Column<int>(type: "integer", nullable: false, comment: "Статус освобождения"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак удаленной записи"),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата создания"),
                    LastEdit = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата последнего редактирования")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Episodes_Survivors_SurvivorID",
                        column: x => x.SurvivorID,
                        principalSchema: "public",
                        principalTable: "Survivors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Эпизоды эксплуатации");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_IsDeleted",
                schema: "public",
                table: "Episodes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_SurvivorID",
                schema: "public",
                table: "Episodes",
                column: "SurvivorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Episodes",
                schema: "public");
        }
    }
}

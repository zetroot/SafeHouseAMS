using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SafeHouseAMS.DataLayer.Migrations
{
    [ExcludeFromCodeCoverage]
    internal partial class Added_AssistanceRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEdit",
                schema: "public",
                table: "LifeSituationDocuments",
                type: "timestamp without time zone",
                nullable: false,
                comment: "Дата последнего редактирования",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldComment: "Дата-время последнего редактирования записи");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "public",
                table: "LifeSituationDocuments",
                type: "boolean",
                nullable: false,
                comment: "Признак удаленной записи",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldComment: "Признак удалённой записи");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "public",
                table: "LifeSituationDocuments",
                type: "timestamp without time zone",
                nullable: false,
                comment: "Дата создания",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldComment: "Дата-время создания записи");

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                schema: "public",
                table: "LifeSituationDocuments",
                type: "uuid",
                nullable: false,
                comment: "Идентификатор записи",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Идентификатор записи - ПК");

            migrationBuilder.CreateTable(
                name: "AssistanceRequests",
                schema: "public",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор записи"),
                    SurvivorID = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор пострадавшего"),
                    AssistanceKind = table.Column<int>(type: "integer", nullable: false, comment: "Тип запрашиваемой помощи"),
                    Details = table.Column<string>(type: "text", nullable: false, comment: "Дополнительная информация по запросу"),
                    IsAccomplished = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак выполненного запроса"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак удаленной записи"),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата создания"),
                    LastEdit = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата последнего редактирования")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssistanceRequests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AssistanceRequests_Survivors_SurvivorID",
                        column: x => x.SurvivorID,
                        principalSchema: "public",
                        principalTable: "Survivors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Запросы помощи");

            migrationBuilder.CreateTable(
                name: "AssistanceActs",
                schema: "public",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор записи"),
                    Details = table.Column<string>(type: "text", nullable: false, comment: "Дополнительная информация по этому акту помощи"),
                    WorkHours = table.Column<decimal>(type: "numeric", nullable: false, comment: "Потрачено часов"),
                    Money = table.Column<decimal>(type: "numeric", nullable: false, comment: "Потрачено денег"),
                    RequestID = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор запроса помощи"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак удаленной записи"),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата создания"),
                    LastEdit = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата последнего редактирования")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssistanceActs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AssistanceActs_AssistanceRequests_RequestID",
                        column: x => x.RequestID,
                        principalSchema: "public",
                        principalTable: "AssistanceRequests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Акты оказания помощи");

            migrationBuilder.CreateIndex(
                name: "IX_Survivors_Created",
                schema: "public",
                table: "Survivors",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Survivors_IsDeleted",
                schema: "public",
                table: "Survivors",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Survivors_LastEdit",
                schema: "public",
                table: "Survivors",
                column: "LastEdit");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_Created",
                schema: "public",
                table: "Episodes",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_LastEdit",
                schema: "public",
                table: "Episodes",
                column: "LastEdit");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceActs_Created",
                schema: "public",
                table: "AssistanceActs",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceActs_IsDeleted",
                schema: "public",
                table: "AssistanceActs",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceActs_LastEdit",
                schema: "public",
                table: "AssistanceActs",
                column: "LastEdit");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceActs_RequestID",
                schema: "public",
                table: "AssistanceActs",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequests_Created",
                schema: "public",
                table: "AssistanceRequests",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequests_IsAccomplished",
                schema: "public",
                table: "AssistanceRequests",
                column: "IsAccomplished");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequests_IsDeleted",
                schema: "public",
                table: "AssistanceRequests",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequests_LastEdit",
                schema: "public",
                table: "AssistanceRequests",
                column: "LastEdit");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequests_SurvivorID",
                schema: "public",
                table: "AssistanceRequests",
                column: "SurvivorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssistanceActs",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AssistanceRequests",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_Survivors_Created",
                schema: "public",
                table: "Survivors");

            migrationBuilder.DropIndex(
                name: "IX_Survivors_IsDeleted",
                schema: "public",
                table: "Survivors");

            migrationBuilder.DropIndex(
                name: "IX_Survivors_LastEdit",
                schema: "public",
                table: "Survivors");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_Created",
                schema: "public",
                table: "Episodes");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_LastEdit",
                schema: "public",
                table: "Episodes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEdit",
                schema: "public",
                table: "LifeSituationDocuments",
                type: "timestamp without time zone",
                nullable: false,
                comment: "Дата-время последнего редактирования записи",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldComment: "Дата последнего редактирования");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "public",
                table: "LifeSituationDocuments",
                type: "boolean",
                nullable: false,
                comment: "Признак удалённой записи",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldComment: "Признак удаленной записи");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "public",
                table: "LifeSituationDocuments",
                type: "timestamp without time zone",
                nullable: false,
                comment: "Дата-время создания записи",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldComment: "Дата создания");

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                schema: "public",
                table: "LifeSituationDocuments",
                type: "uuid",
                nullable: false,
                comment: "Идентификатор записи - ПК",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Идентификатор записи");
        }
    }
}

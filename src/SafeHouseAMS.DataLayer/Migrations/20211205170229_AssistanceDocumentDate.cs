using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SafeHouseAMS.DataLayer.Migrations
{
    [ExcludeFromCodeCoverage]
    internal partial class AssistanceDocumentDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEdit",
                schema: "public",
                table: "Survivors",
                type: "timestamp with time zone",
                nullable: false,
                comment: "Дата последнего редактирования",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldComment: "Дата последнего редактирования");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "public",
                table: "Survivors",
                type: "timestamp with time zone",
                nullable: false,
                comment: "Дата создания",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldComment: "Дата создания");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDateCalculated",
                schema: "public",
                table: "Survivors",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Вычисленная дата рождения",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldComment: "Вычисленная дата рождения");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDateAccurate",
                schema: "public",
                table: "Survivors",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Точная дата рождения",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldComment: "Точная дата рождения");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEdit",
                schema: "public",
                table: "LifeSituationDocuments",
                type: "timestamp with time zone",
                nullable: false,
                comment: "Дата последнего редактирования",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldComment: "Дата последнего редактирования");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DocumentDate",
                schema: "public",
                table: "LifeSituationDocuments",
                type: "timestamp with time zone",
                nullable: false,
                comment: "Дата документа",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldComment: "Дата документа");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "public",
                table: "LifeSituationDocuments",
                type: "timestamp with time zone",
                nullable: false,
                comment: "Дата создания",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldComment: "Дата создания");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEdit",
                schema: "public",
                table: "Episodes",
                type: "timestamp with time zone",
                nullable: false,
                comment: "Дата последнего редактирования",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldComment: "Дата последнего редактирования");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "public",
                table: "Episodes",
                type: "timestamp with time zone",
                nullable: false,
                comment: "Дата создания",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldComment: "Дата создания");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEdit",
                schema: "public",
                table: "AssistanceRequests",
                type: "timestamp with time zone",
                nullable: false,
                comment: "Дата последнего редактирования",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldComment: "Дата последнего редактирования");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "public",
                table: "AssistanceRequests",
                type: "timestamp with time zone",
                nullable: false,
                comment: "Дата создания",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldComment: "Дата создания");

            migrationBuilder.AddColumn<DateTime>(
                name: "DocumentDate",
                schema: "public",
                table: "AssistanceRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                comment: "Дата запроса помощи");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEdit",
                schema: "public",
                table: "AssistanceActs",
                type: "timestamp with time zone",
                nullable: false,
                comment: "Дата последнего редактирования",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldComment: "Дата последнего редактирования");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "public",
                table: "AssistanceActs",
                type: "timestamp with time zone",
                nullable: false,
                comment: "Дата создания",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldComment: "Дата создания");

            migrationBuilder.AddColumn<DateTime>(
                name: "DocumentDate",
                schema: "public",
                table: "AssistanceActs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                comment: "Дата совершения акта помощи");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequests_DocumentDate",
                schema: "public",
                table: "AssistanceRequests",
                column: "DocumentDate");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceActs_DocumentDate",
                schema: "public",
                table: "AssistanceActs",
                column: "DocumentDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AssistanceRequests_DocumentDate",
                schema: "public",
                table: "AssistanceRequests");

            migrationBuilder.DropIndex(
                name: "IX_AssistanceActs_DocumentDate",
                schema: "public",
                table: "AssistanceActs");

            migrationBuilder.DropColumn(
                name: "DocumentDate",
                schema: "public",
                table: "AssistanceRequests");

            migrationBuilder.DropColumn(
                name: "DocumentDate",
                schema: "public",
                table: "AssistanceActs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEdit",
                schema: "public",
                table: "Survivors",
                type: "timestamp without time zone",
                nullable: false,
                comment: "Дата последнего редактирования",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldComment: "Дата последнего редактирования");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "public",
                table: "Survivors",
                type: "timestamp without time zone",
                nullable: false,
                comment: "Дата создания",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldComment: "Дата создания");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDateCalculated",
                schema: "public",
                table: "Survivors",
                type: "timestamp without time zone",
                nullable: true,
                comment: "Вычисленная дата рождения",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldComment: "Вычисленная дата рождения");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDateAccurate",
                schema: "public",
                table: "Survivors",
                type: "timestamp without time zone",
                nullable: true,
                comment: "Точная дата рождения",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldComment: "Точная дата рождения");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEdit",
                schema: "public",
                table: "LifeSituationDocuments",
                type: "timestamp without time zone",
                nullable: false,
                comment: "Дата последнего редактирования",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldComment: "Дата последнего редактирования");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DocumentDate",
                schema: "public",
                table: "LifeSituationDocuments",
                type: "timestamp without time zone",
                nullable: false,
                comment: "Дата документа",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldComment: "Дата документа");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "public",
                table: "LifeSituationDocuments",
                type: "timestamp without time zone",
                nullable: false,
                comment: "Дата создания",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldComment: "Дата создания");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEdit",
                schema: "public",
                table: "Episodes",
                type: "timestamp without time zone",
                nullable: false,
                comment: "Дата последнего редактирования",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldComment: "Дата последнего редактирования");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "public",
                table: "Episodes",
                type: "timestamp without time zone",
                nullable: false,
                comment: "Дата создания",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldComment: "Дата создания");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEdit",
                schema: "public",
                table: "AssistanceRequests",
                type: "timestamp without time zone",
                nullable: false,
                comment: "Дата последнего редактирования",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldComment: "Дата последнего редактирования");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "public",
                table: "AssistanceRequests",
                type: "timestamp without time zone",
                nullable: false,
                comment: "Дата создания",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldComment: "Дата создания");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEdit",
                schema: "public",
                table: "AssistanceActs",
                type: "timestamp without time zone",
                nullable: false,
                comment: "Дата последнего редактирования",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldComment: "Дата последнего редактирования");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "public",
                table: "AssistanceActs",
                type: "timestamp without time zone",
                nullable: false,
                comment: "Дата создания",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldComment: "Дата создания");
        }
    }
}

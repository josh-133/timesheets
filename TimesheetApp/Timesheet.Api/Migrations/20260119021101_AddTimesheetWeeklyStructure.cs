using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timesheet.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTimesheetWeeklyStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimesheetId",
                table: "TimesheetEntries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeeklyTimesheetId",
                table: "TimesheetEntries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WeeklyTimesheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WeekStartDate = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyTimesheets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetEntries_WeeklyTimesheetId",
                table: "TimesheetEntries",
                column: "WeeklyTimesheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimesheetEntries_WeeklyTimesheets_WeeklyTimesheetId",
                table: "TimesheetEntries",
                column: "WeeklyTimesheetId",
                principalTable: "WeeklyTimesheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimesheetEntries_WeeklyTimesheets_WeeklyTimesheetId",
                table: "TimesheetEntries");

            migrationBuilder.DropTable(
                name: "WeeklyTimesheets");

            migrationBuilder.DropIndex(
                name: "IX_TimesheetEntries_WeeklyTimesheetId",
                table: "TimesheetEntries");

            migrationBuilder.DropColumn(
                name: "TimesheetId",
                table: "TimesheetEntries");

            migrationBuilder.DropColumn(
                name: "WeeklyTimesheetId",
                table: "TimesheetEntries");
        }
    }
}

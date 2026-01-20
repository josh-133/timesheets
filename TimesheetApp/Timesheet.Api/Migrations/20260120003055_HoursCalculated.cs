using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timesheet.Api.Migrations
{
    /// <inheritdoc />
    public partial class HoursCalculated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "BreakTime",
                table: "TimesheetEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EndTime",
                table: "TimesheetEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "StartTime",
                table: "TimesheetEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreakTime",
                table: "TimesheetEntries");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "TimesheetEntries");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "TimesheetEntries");
        }
    }
}

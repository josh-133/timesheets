using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timesheet.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTimesheetWeeklyStructure_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimesheetId",
                table: "TimesheetEntries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimesheetId",
                table: "TimesheetEntries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}

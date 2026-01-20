using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timesheet.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBreakTimeToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BreakTime",
                table: "TimesheetEntries",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "BreakTime",
                table: "TimesheetEntries",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}

namespace Timesheet.Api.Models;

public class WeeklyTimesheet {
    public int Id { get; set; }
    public DateOnly WeekStartDate { get; set; }
    public List<TimesheetEntry> Entries { get; set; } = new();
}
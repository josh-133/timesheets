namespace Timesheet.Api.Models;

public class TimesheetEntry
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; } = TimeOnly.MinValue;
    public TimeOnly EndTime { get; set; } = TimeOnly.MinValue;
    public int BreakTime { get; set; }
    public decimal Hours { get; set; }
    public string Description { get; set; } = string.Empty;

    public int WeeklyTimesheetId { get; set; }
    public WeeklyTimesheet WeeklyTimesheet { get; set; } = null!;


    public decimal CalculateHours() {
        var mins = TimeSpan.FromMinutes(BreakTime);
        var result = EndTime - StartTime - mins;
        Hours = (decimal)Math.Round(result.TotalHours, 1);
        return Hours;
    }
}
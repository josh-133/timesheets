namespace Timesheet.Api.Dtos;

public class TimesheetEntryDto
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int BreakTime { get; set; }
    public decimal Hours { get; set; }
    public string Description { get; set; } = string.Empty;
    public int WeeklyTimesheetId { get; set; }
}
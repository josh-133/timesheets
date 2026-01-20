namespace Timesheet.Api.Dtos;

public class CreateTimesheetEntryDto
{
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int BreakTime { get; set; }
    public string Description { get; set; } = string.Empty;
}
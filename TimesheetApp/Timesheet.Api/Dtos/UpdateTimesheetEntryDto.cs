namespace Timesheet.Api.Dtos;

public class UpdateTimesheetEntryDto
{
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int BreakTime { get; set; }
    public string Description { get; set; } = string.Empty;
}
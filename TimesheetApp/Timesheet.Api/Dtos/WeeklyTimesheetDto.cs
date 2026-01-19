namespace Timesheet.Api.Dtos;

public class WeeklyTimesheetDto
{
    public int Id { get; set; }
    public DateOnly WeekStartDate { get; set; }
    public List<TimesheetEntryDto> Entries { get; set; } = new();
}
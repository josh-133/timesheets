namespace Timesheet.Api.Models;

public class TimesheetEntry
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public decimal Hours { get; set; }
    public string Description { get; set; } = string.Empty;
}
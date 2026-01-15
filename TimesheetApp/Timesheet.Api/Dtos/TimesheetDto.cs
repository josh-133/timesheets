namespace Timesheet.Api.Dtos;

public class TimesheetDto
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public decimal Hours { get; set; }
    public string Description { get; set; } = string.Empty;
}
namespace Timesheet.Api.Dtos;

public class UpdateTimesheetDto
{
    public DateOnly Date { get; set; }
    public decimal Hours { get; set; }
    public string Description { get; set; } = string.Empty;
}
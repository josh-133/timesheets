using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Timesheet.Api.Data;
using Timesheet.Api.Dtos;
using Timesheet.Api.Models;

namespace Timesheet.Api.Controllers;

[ApiController]
[Route("api[controller]")]
public class WeeklyTimesheetController : ControllerBase
{
    private readonly TimesheetDbContext _db;

    public WeeklyTimesheetController(TimesheetDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IEnumerable<WeeklyTimesheetDto>> GetAll()
    {
        return await _db.WeeklyTimesheets
            .Include(t => t.Entries)
            .OrderByDescending(t => t.WeekStartDate)
            .Select(t => new WeeklyTimesheetDto
            {
                Id = t.Id,
                WeekStartDate = t.WeekStartDate,
                Entries = t.Entries.Select(e => new TimesheetEntryDto
                {
                    Id = e.Id,
                    Date = e.Date,
                    Hours = e.Hours,
                    Description = e.Description
                }).ToList()
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var timesheet = await _db.WeeklyTimesheets
            .Include(t => t.Entries)
            .Where(t => t.Id == id)
            .Select(t => new WeeklyTimesheetDto
            {
                Id = t.Id,
                WeekStartDate = t.WeekStartDate,
                Entries = t.Entries.Select(e => new TimesheetEntryDto
                {
                    Id = e.Id,
                    Date = e.Date,
                    Hours = e.Hours,
                    Description = e.Description
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (timesheet == null)
            return NotFound();

        return Ok(timesheet);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateWeeklyTimesheetDto dto)
    {
        var existing = await _db.WeeklyTimesheets
            .FirstOrDefaultAsync(t => t.WeekStartDate == dto.WeekStartDate);

        if (existing != null)
            return BadRequest("A timesheet for this week already exists.");
        
        var timesheet = new WeeklyTimesheet
        {
            WeekStartDate = dto.WeekStartDate
        };

        _db.WeeklyTimesheets.Add(timesheet);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = timesheet.Id }, new WeeklyTimesheetDto
        {
            Id = timesheet.Id,
            WeekStartDate = timesheet.WeekStartDate,
            Entries = new List<TimesheetEntryDto>()
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var timesheet = await _db.WeeklyTimesheets
            .Include(t => t.Entries)
            .FirstOrDefaultAsync(t => t.Id == id);
        
        if (timesheet == null)
            return NotFound();

        _db.WeeklyTimesheets.Remove(timesheet);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
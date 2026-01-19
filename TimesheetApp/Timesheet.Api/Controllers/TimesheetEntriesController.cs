using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Timesheet.Api.Data;
using Timesheet.Api.Dtos;
using Timesheet.Api.Models;

namespace Timesheet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimesheetEntriesController : ControllerBase
{
    private readonly TimesheetDbContext _db;

    public TimesheetEntriesController(TimesheetDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IEnumerable<TimesheetEntryDto>> GetAll()
    {
        return await _db.TimesheetEntries
            .Select(e => new TimesheetEntryDto
            {
                Id = e.Id,
                Date = e.Date,
                Hours = e.Hours,
                Description = e.Description,
                WeeklyTimesheetId = e.WeeklyTimesheetId
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var entry = await _db.TimesheetEntries.FindAsync(id);
        if (entry == null)
            return NotFound();

        return Ok(new TimesheetEntryDto
        {
            Id = entry.Id,
            Date = entry.Date,
            Hours = entry.Hours,
            Description = entry.Description,
            WeeklyTimesheetId = entry.WeeklyTimesheetId
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTimesheetEntryDto dto)
    {
        var weekStartDate = GetMondayOfWeek(dto.Date);

        var weeklyTimesheet = await _db.WeeklyTimesheets
            .FirstOrDefaultAsync(t => t.WeekStartDate == weekStartDate);

        if (weeklyTimesheet == null) 
        {
            weeklyTimesheet = new WeeklyTimesheet
            {
                WeekStartDate = weekStartDate
            };
            _db.WeeklyTimesheets.Add(weeklyTimesheet);
            await _db.SaveChangesAsync();
        }

        var entry = new TimesheetEntry
        {
            Date = dto.Date,
            Hours = dto.Hours,
            Description = dto.Description,
            WeeklyTimesheetId = weeklyTimesheet.Id
        };

        _db.TimesheetEntries.Add(entry);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { id = entry.Id }, new TimesheetEntryDto {
            Id = entry.Id,
            Date = entry.Date,
            Hours = entry.Hours,
            Description = entry.Description,
            WeeklyTimesheetId = entry.WeeklyTimesheetId
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTimesheetEntryDto dto)
    {
        var entry = await _db.TimesheetEntries.FindAsync(id);
        if (entry == null)
            return NotFound();

        var newWeekStartDate = GetMondayOfWeek(dto.Date);
        var oldWeekStartDate = GetMondayOfWeek(entry.Date);

        if (newWeekStartDate != oldWeekStartDate)
        {
            var newWeeklyTimesheet = await _db.WeeklyTimesheets
                .FirstOrDefaultAsync(t => t.WeekStartDate == newWeekStartDate);
            
            if (newWeeklyTimesheet == null)
            {
                newWeeklyTimesheet = new WeeklyTimesheet
                {
                    WeekStartDate = newWeekStartDate
                };
                _db.WeeklyTimesheets.Add(newWeeklyTimesheet);
                await _db.SaveChangesAsync();
            }

            entry.WeeklyTimesheetId = newWeeklyTimesheet.Id;
        }

        entry.Date = dto.Date;
        entry.Hours = dto.Hours;
        entry.Description = dto.Description;
        
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entry = await _db.TimesheetEntries.FindAsync(id);
        if (entry == null)
            return NotFound();

        _db.TimesheetEntries.Remove(entry);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    private DateOnly GetMondayOfWeek(DateOnly date)
    {
        int daysFromMonday = ((int)date.DayOfWeek - 1 + 7) % 7;
        return date.AddDays(-daysFromMonday);
    }
}


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
                Description = e.Description
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
            Description = entry.Description
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTimesheetEntryDto dto)
    {
        var entry = new TimesheetEntry
        {
            Date = dto.Date,
            Hours = dto.Hours,
            Description = dto.Description
        };

        _db.TimesheetEntries.Add(entry);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { id = entry.Id }, entry);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTimesheetEntryDto timesheet)
    {
        var entry = await _db.TimesheetEntries.FindAsync(id);
        if (entry == null)
            return NotFound();

        entry.Date = timesheet.Date;
        entry.Hours = timesheet.Hours;
        entry.Description = timesheet.Description;
        
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
}


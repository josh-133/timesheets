using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Timesheet.Api.Data;
using Timesheet.Api.Dtos;
using Timesheet.Api.Models;

namespace Timesheet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimesheetsController : ControllerBase
{
    private readonly TimesheetDbContext _db;

    public TimesheetsController(TimesheetDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IEnumerable<TimesheetDto>> Get()
    {
        return await _db.TimesheetEntries
            .Select(e => new TimesheetDto
            {
                Id = e.Id,
                Date = e.Date,
                Hours = e.Hours,
                Description = e.Description
            })
            .ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTimesheetDto dto)
    {
        var entry = new TimesheetEntry
        {
            Date = dto.Date,
            Hours = dto.Hours,
            Description = dto.Description
        };

        _db.TimesheetEntries.Add(entry);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = entry.Id }, entry);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTimesheetDto timesheet)
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


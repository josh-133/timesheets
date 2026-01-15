using Microsoft.EntityFrameworkCore;
using Timesheet.Api.Models;

namespace Timesheet.Api.Data;

public class TimesheetDbContext : DbContext
{
    public TimesheetDbContext(DbContextOptions<TimesheetDbContext> options)
        : base(options)
    {
    }

public DbSet<TimesheetEntry> TimesheetEntries => Set<TimesheetEntry>();
}
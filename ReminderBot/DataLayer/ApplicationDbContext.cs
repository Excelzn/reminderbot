using Microsoft.EntityFrameworkCore;
using ReminderBot.Models;

namespace ReminderBot.DataLayer;

public class ApplicationDbContext : DbContext
{
  public DbSet<Reminder> Reminders { get; set; }

  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt): base(opt)
  {
    
  }
}
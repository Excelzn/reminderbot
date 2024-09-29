using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ReminderBot.DataLayer;

public class ApplicationDbContextFactory: IDesignTimeDbContextFactory<ApplicationDbContext>
{
  public ApplicationDbContext CreateDbContext(string[] args)
  {
    var optsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
    optsBuilder.UseNpgsql(
      "User ID=postgres;Password=T3chn0m@ncy;Host=localhost;Port=5433;Database=reminderDb;Pooling=true;");
    return new ApplicationDbContext(optsBuilder.Options);
  }
}
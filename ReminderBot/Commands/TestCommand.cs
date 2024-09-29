using DSharpPlus.Commands;
using DSharpPlus.Commands.Trees.Metadata;
using DSharpPlus.Entities;
using Microsoft.Extensions.DependencyInjection;
using ReminderBot.DataLayer;
using ReminderBot.Models;

namespace ReminderBot.Commands;


public class TestCommand
{
  /// <summary>
  /// Sends a reminder to a role at some day in the future
  /// </summary>
  /// <param name="role">Discord Role to ping</param>
  /// <param name="text">Reminder Text</param>
  /// <param name="number">Number of timespans to wait (If you want to wait 1 week, this should be 1)</param>
  /// <param name="timespan">Timespan to count. Should be 'day' 'min' 'mon' 'week'.</param>
  [Command("test")]
  
  public static async ValueTask ExecuteAsync(CommandContext context, DiscordRole role, string text, int number, string timespan)
  {
    await context.DeferResponseAsync();
    var dbContext = context.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var now = DateTime.UtcNow;
    switch (timespan.ToLowerInvariant())
    {
      case "day":
        now = now.AddDays(number);
        break;
      case "min":
        now = now.AddMinutes(number);
        break;
      case "mon":
        now = now.AddMonths(number);
        break;
      case "week":
        now = now.AddDays(7 * number);
        break;
      default:
        await context.FollowupAsync("Invalid timeframe");
        break;
    }
    var reminder = new Reminder
    {
      ReminderText = text,
      RemindAt = now,
      RolesToPing = role.Id,
      Channel = context.Channel.Id,
      Guild = context.Guild.Id
    };
    dbContext.Reminders.Add(reminder);
    await dbContext.SaveChangesAsync();
    await context.FollowupAsync("Reminder scheduled!");
  }
}
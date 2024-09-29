using DSharpPlus;
using DSharpPlus.Commands;
using DSharpPlus.Commands.Processors.SlashCommands;
using DSharpPlus.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReminderBot.Commands;
using ReminderBot.DataLayer;
using ReminderBot.Models;

namespace ReminderBot;

class Program
{
  static async Task Main(string[] args)
  {
    var builder = new ConfigurationBuilder();
    builder.SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .AddJsonFile("appsettings.dev.json", optional: true, reloadOnChange: true)
      .AddEnvironmentVariables();
    var config = builder.Build();
    
    var discordConf = config.GetSection("DiscordConfig").Get<DiscordConfig>();
    var discordClientBuilder = DiscordClientBuilder.CreateDefault(discordConf.Token, DiscordIntents.All);
    discordClientBuilder.ConfigureServices(s =>
    {
      s.AddDbContext<ApplicationDbContext>(c =>
      {
        c.UseNpgsql(config.GetConnectionString("db"));
      });
    });
    discordClientBuilder.UseCommands(e =>
    {
      e.AddCommand(TestCommand.ExecuteAsync);
      var processor = new SlashCommandProcessor();
      e.AddProcessor(processor);
    }); 
    var client = discordClientBuilder.Build();
    await client.ConnectAsync();
    while (true)
    {
      var dbContext = client.ServiceProvider.GetRequiredService<ApplicationDbContext>();
      var now = DateTime.UtcNow;
      var reminders = await dbContext.Reminders
        .Where(x => x.RemindAt <= now && !x.Processed)
        .ToListAsync();
      foreach (var reminder in reminders)
      {
        try
        {
          var messageBuilder = new DiscordMessageBuilder()
            .WithContent($"{reminder.ReminderText} <@&{reminder.RolesToPing}>");
          if (client.Guilds.TryGetValue(reminder.Guild, out var guild))
          {
            var channel = await guild.GetChannelAsync(reminder.Channel);
            await channel.SendMessageAsync(messageBuilder);
            reminder.Processed = true;
            await dbContext.SaveChangesAsync();
          }
        }
        catch (Exception e)
        {
          Console.WriteLine(e);
        }
      }
      await Task.Delay(300000);
    }
  }
}
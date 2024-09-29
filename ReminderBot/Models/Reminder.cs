namespace ReminderBot.Models;

public class Reminder
{
  public int Id { get; set; }
  public string ReminderText { get; set; }
  public ulong Channel { get; set; }
  public ulong Guild { get; set; }
  public ulong RolesToPing { get; set; }
  public List<ulong>? UsersToPing { get; set; }
  public DateTime RemindAt { get; set; }
  public bool Processed { get; set; }
}
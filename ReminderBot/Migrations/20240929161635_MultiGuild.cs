using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReminderBot.Migrations
{
    /// <inheritdoc />
    public partial class MultiGuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Guild",
                table: "Reminders",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guild",
                table: "Reminders");
        }
    }
}

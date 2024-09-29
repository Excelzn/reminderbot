using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReminderBot.Migrations
{
    /// <inheritdoc />
    public partial class UsersNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal[]>(
                name: "UsersToPing",
                table: "Reminders",
                type: "numeric(20,0)[]",
                nullable: true,
                oldClrType: typeof(decimal[]),
                oldType: "numeric(20,0)[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal[]>(
                name: "UsersToPing",
                table: "Reminders",
                type: "numeric(20,0)[]",
                nullable: false,
                defaultValue: new decimal[0],
                oldClrType: typeof(decimal[]),
                oldType: "numeric(20,0)[]",
                oldNullable: true);
        }
    }
}

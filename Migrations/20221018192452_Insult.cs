using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwitchDiscordBot.Migrations
{
    public partial class Insult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Insult",
                table: "UserCommand",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Insult",
                table: "UserCommand");
        }
    }
}

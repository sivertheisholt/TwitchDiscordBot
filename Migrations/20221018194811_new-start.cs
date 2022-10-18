using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwitchDiscordBot.Migrations
{
    public partial class newstart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCommand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TranslateDoge = table.Column<bool>(type: "bit", nullable: false),
                    TranslateYoda = table.Column<bool>(type: "bit", nullable: false),
                    Insult = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCommand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Messagecount = table.Column<int>(type: "int", nullable: false),
                    Xp = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    UserCommandId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_UserCommand_UserCommandId",
                        column: x => x.UserCommandId,
                        principalTable: "UserCommand",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_UserCommandId",
                table: "User",
                column: "UserCommandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserCommand");
        }
    }
}

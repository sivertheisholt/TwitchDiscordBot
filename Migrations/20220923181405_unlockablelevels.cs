using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuskyBot.Migrations
{
    public partial class unlockablelevels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserCommandId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserCommand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DogeTranslate = table.Column<bool>(type: "bit", nullable: false),
                    YodaTranslate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCommand", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_UserCommandId",
                table: "User",
                column: "UserCommandId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserCommand_UserCommandId",
                table: "User",
                column: "UserCommandId",
                principalTable: "UserCommand",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_UserCommand_UserCommandId",
                table: "User");

            migrationBuilder.DropTable(
                name: "UserCommand");

            migrationBuilder.DropIndex(
                name: "IX_User_UserCommandId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserCommandId",
                table: "User");
        }
    }
}

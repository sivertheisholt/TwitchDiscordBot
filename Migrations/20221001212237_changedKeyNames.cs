using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuskyBot.Migrations
{
    public partial class changedKeyNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YodaTranslate",
                table: "UserCommand",
                newName: "TranslateYoda");

            migrationBuilder.RenameColumn(
                name: "DogeTranslate",
                table: "UserCommand",
                newName: "TranslateDoge");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TranslateYoda",
                table: "UserCommand",
                newName: "YodaTranslate");

            migrationBuilder.RenameColumn(
                name: "TranslateDoge",
                table: "UserCommand",
                newName: "DogeTranslate");
        }
    }
}

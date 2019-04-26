using Microsoft.EntityFrameworkCore.Migrations;

namespace Joblify.Core.Data.Migrations
{
    public partial class removetokenfromdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalProviderToken",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalProviderToken",
                table: "Users",
                nullable: false,
                defaultValue: "");
        }
    }
}

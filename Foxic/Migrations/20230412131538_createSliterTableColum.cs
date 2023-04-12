using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foxic.Migrations
{
    public partial class createSliterTableColum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVideo",
                table: "sliders",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "sliders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVideo",
                table: "sliders");

            migrationBuilder.DropColumn(
                name: "URL",
                table: "sliders");
        }
    }
}

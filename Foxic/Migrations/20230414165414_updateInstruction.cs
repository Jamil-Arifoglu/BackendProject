using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foxic.Migrations
{
    public partial class updateInstruction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Instructions",
                newName: "Polyester");

            migrationBuilder.AddColumn<string>(
                name: "Chlorine",
                table: "Instructions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Drycleaning",
                table: "Instructions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lining",
                table: "Instructions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chlorine",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "Drycleaning",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "Lining",
                table: "Instructions");

            migrationBuilder.RenameColumn(
                name: "Polyester",
                table: "Instructions",
                newName: "Text");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foxic.Migrations
{
    public partial class updateClothes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStok",
                table: "clothes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Stok",
                table: "clothes",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStok",
                table: "clothes");

            migrationBuilder.DropColumn(
                name: "Stok",
                table: "clothes");
        }
    }
}

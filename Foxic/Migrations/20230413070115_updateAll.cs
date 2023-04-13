using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foxic.Migrations
{
    public partial class updateAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_Clothes_ClothesId",
                table: "Instructions");

            migrationBuilder.DropIndex(
                name: "IX_Instructions_ClothesId",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "ClothesId",
                table: "Instructions");

            migrationBuilder.AddColumn<int>(
                name: "InstructionId",
                table: "Clothes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Clothes_InstructionId",
                table: "Clothes",
                column: "InstructionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clothes_Instructions_InstructionId",
                table: "Clothes",
                column: "InstructionId",
                principalTable: "Instructions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothes_Instructions_InstructionId",
                table: "Clothes");

            migrationBuilder.DropIndex(
                name: "IX_Clothes_InstructionId",
                table: "Clothes");

            migrationBuilder.DropColumn(
                name: "InstructionId",
                table: "Clothes");

            migrationBuilder.AddColumn<int>(
                name: "ClothesId",
                table: "Instructions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_ClothesId",
                table: "Instructions",
                column: "ClothesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_Clothes_ClothesId",
                table: "Instructions",
                column: "ClothesId",
                principalTable: "Clothes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foxic.Migrations
{
    public partial class createInstructionAndUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clothes_ClothesGlobalTabs_ClothesGlobalTabId",
                table: "clothes");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothesColorSizes_clothes_ClothesId",
                table: "ClothesColorSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothesImages_clothes_ClothesId",
                table: "ClothesImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothesTags_clothes_ClothesId",
                table: "ClothesTags");

            migrationBuilder.DropTable(
                name: "ClothesCollections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sliders",
                table: "sliders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_collections",
                table: "collections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clothes",
                table: "clothes");

            migrationBuilder.DropColumn(
                name: "Instruction",
                table: "clothes");

            migrationBuilder.DropColumn(
                name: "IsStok",
                table: "clothes");

            migrationBuilder.DropColumn(
                name: "discountRate",
                table: "clothes");

            migrationBuilder.RenameTable(
                name: "sliders",
                newName: "Sliders");

            migrationBuilder.RenameTable(
                name: "collections",
                newName: "Collections");

            migrationBuilder.RenameTable(
                name: "clothes",
                newName: "Clothes");

            migrationBuilder.RenameIndex(
                name: "IX_clothes_ClothesGlobalTabId",
                table: "Clothes",
                newName: "IX_Clothes_ClothesGlobalTabId");

            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "Clothes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sliders",
                table: "Sliders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collections",
                table: "Collections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clothes",
                table: "Clothes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Catagory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catagory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClothesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructions_Clothes_ClothesId",
                        column: x => x.ClothesId,
                        principalTable: "Clothes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClothesCatagory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionId = table.Column<int>(type: "int", nullable: false),
                    CatagoryId = table.Column<int>(type: "int", nullable: false),
                    ClothesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothesCatagory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothesCatagory_Catagory_CatagoryId",
                        column: x => x.CatagoryId,
                        principalTable: "Catagory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothesCatagory_Clothes_ClothesId",
                        column: x => x.ClothesId,
                        principalTable: "Clothes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clothes_CollectionId",
                table: "Clothes",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothesCatagory_CatagoryId",
                table: "ClothesCatagory",
                column: "CatagoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothesCatagory_ClothesId",
                table: "ClothesCatagory",
                column: "ClothesId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_ClothesId",
                table: "Instructions",
                column: "ClothesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clothes_ClothesGlobalTabs_ClothesGlobalTabId",
                table: "Clothes",
                column: "ClothesGlobalTabId",
                principalTable: "ClothesGlobalTabs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clothes_Collections_CollectionId",
                table: "Clothes",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothesColorSizes_Clothes_ClothesId",
                table: "ClothesColorSizes",
                column: "ClothesId",
                principalTable: "Clothes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothesImages_Clothes_ClothesId",
                table: "ClothesImages",
                column: "ClothesId",
                principalTable: "Clothes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothesTags_Clothes_ClothesId",
                table: "ClothesTags",
                column: "ClothesId",
                principalTable: "Clothes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothes_ClothesGlobalTabs_ClothesGlobalTabId",
                table: "Clothes");

            migrationBuilder.DropForeignKey(
                name: "FK_Clothes_Collections_CollectionId",
                table: "Clothes");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothesColorSizes_Clothes_ClothesId",
                table: "ClothesColorSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothesImages_Clothes_ClothesId",
                table: "ClothesImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothesTags_Clothes_ClothesId",
                table: "ClothesTags");

            migrationBuilder.DropTable(
                name: "ClothesCatagory");

            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "Catagory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sliders",
                table: "Sliders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collections",
                table: "Collections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clothes",
                table: "Clothes");

            migrationBuilder.DropIndex(
                name: "IX_Clothes_CollectionId",
                table: "Clothes");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Clothes");

            migrationBuilder.RenameTable(
                name: "Sliders",
                newName: "sliders");

            migrationBuilder.RenameTable(
                name: "Collections",
                newName: "collections");

            migrationBuilder.RenameTable(
                name: "Clothes",
                newName: "clothes");

            migrationBuilder.RenameIndex(
                name: "IX_Clothes_ClothesGlobalTabId",
                table: "clothes",
                newName: "IX_clothes_ClothesGlobalTabId");

            migrationBuilder.AddColumn<string>(
                name: "Instruction",
                table: "clothes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsStok",
                table: "clothes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "discountRate",
                table: "clothes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sliders",
                table: "sliders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_collections",
                table: "collections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clothes",
                table: "clothes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ClothesCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClothesId = table.Column<int>(type: "int", nullable: false),
                    CollectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothesCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothesCollections_clothes_ClothesId",
                        column: x => x.ClothesId,
                        principalTable: "clothes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothesCollections_collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothesCollections_ClothesId",
                table: "ClothesCollections",
                column: "ClothesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothesCollections_CollectionId",
                table: "ClothesCollections",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_clothes_ClothesGlobalTabs_ClothesGlobalTabId",
                table: "clothes",
                column: "ClothesGlobalTabId",
                principalTable: "ClothesGlobalTabs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothesColorSizes_clothes_ClothesId",
                table: "ClothesColorSizes",
                column: "ClothesId",
                principalTable: "clothes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothesImages_clothes_ClothesId",
                table: "ClothesImages",
                column: "ClothesId",
                principalTable: "clothes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothesTags_clothes_ClothesId",
                table: "ClothesTags",
                column: "ClothesId",
                principalTable: "clothes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

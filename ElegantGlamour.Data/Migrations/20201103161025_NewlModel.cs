using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElegantGlamour.Data.Migrations
{
    public partial class NewlModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestations_Categories_CategoryId",
                table: "Prestations");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Prestations_CategoryId",
                table: "Prestations");

            migrationBuilder.DeleteData(
                table: "Prestations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prestations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Prestations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Prestations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Prestations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Prestations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Prestations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Prestations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Prestations");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Prestations",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrestationCategoryId",
                table: "Prestations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PrestationCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrestationCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prestations_PrestationCategoryId",
                table: "Prestations",
                column: "PrestationCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestations_PrestationCategories_PrestationCategoryId",
                table: "Prestations",
                column: "PrestationCategoryId",
                principalTable: "PrestationCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestations_PrestationCategories_PrestationCategoryId",
                table: "Prestations");

            migrationBuilder.DropTable(
                name: "PrestationCategories");

            migrationBuilder.DropIndex(
                name: "IX_Prestations_PrestationCategoryId",
                table: "Prestations");

            migrationBuilder.DropColumn(
                name: "PrestationCategoryId",
                table: "Prestations");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Prestations",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Prestations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1, "Maquillage" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Title" },
                values: new object[] { 2, "Soins" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Title" },
                values: new object[] { 3, "Massage" });

            migrationBuilder.InsertData(
                table: "Prestations",
                columns: new[] { "Id", "CategoryId", "Description", "Duration", "Price", "Title" },
                values: new object[,]
                {
                    { 1, 1, "ceci est la préstation numéro 1", 45, 30f, "Prestation1" },
                    { 4, 1, "ceci est la préstation numéro 4", 45, 30f, "Prestation1" },
                    { 5, 1, "ceci est la préstation numéro 5", 45, 30f, "Prestation1" },
                    { 2, 2, "ceci est la préstation numéro 2", 45, 30f, "Prestation2" },
                    { 6, 2, "ceci est la préstation numéro 6", 45, 30f, "Prestation1" },
                    { 7, 2, "ceci est la préstation numéro 7", 45, 30f, "Prestation1" },
                    { 3, 3, "ceci est la préstation numéro 3", 45, 30f, "Prestation3" },
                    { 8, 3, "ceci est la préstation numéro 8", 45, 30f, "Prestation1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prestations_CategoryId",
                table: "Prestations",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestations_Categories_CategoryId",
                table: "Prestations",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

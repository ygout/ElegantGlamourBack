using Microsoft.EntityFrameworkCore.Migrations;

namespace ElegantGlamour.Data.Migrations
{
    public partial class SeddDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AcademyGraduationManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Graduation Certificates" },
                    { 2, "Hospitality" },
                    { 3, "Graduation Gifts" }
                });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "CategoryId", "OfferName", "Quantity", "SupplierName", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, "Premium Certificate Package", 35, "Supplier A", 2m },
                    { 2, 1, "Standard Certificate Package", 35, "Supplier B", 1.5m },
                    { 3, 1, "Luxury Certificate Package", 35, "Supplier C", 2.5m },
                    { 4, 2, "Coffee & Drinks Package", 100, "Supplier D", 2m },
                    { 5, 2, "Sandwiches & Snacks Package", 100, "Supplier E", 3m },
                    { 6, 2, "Full Hospitality Package", 100, "Supplier F", 4m },
                    { 7, 3, "Customized Mugs Package", 35, "Supplier G", 2m },
                    { 8, 3, "Graduation Bags Package", 35, "Supplier H", 3m },
                    { 9, 3, "Premium Gift Boxes Package", 35, "Supplier I", 4m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 9);

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

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaymentApp.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedDummyCardRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Balance", "CardHolderName", "CardNumber", "ExpiryDate", "IsActive" },
                values: new object[,]
                {
                    { 1m, 1000m, "John Doe", "1234567812345678", new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 2m, 1500m, "Jane Smith", "8765432187654321", new DateTime(2025, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 3m, 2000m, "Alice Johnson", "4444333322221111", new DateTime(2027, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 4m, 500m, "Bob Williams", "9999888877776666", new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 5m, 750m, "Charlie Brown", "1111222233334444", new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 1m);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 2m);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 3m);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 4m);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 5m);
        }
    }
}

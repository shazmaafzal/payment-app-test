using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaymentApp.API.Migrations
{
    public partial class CardIdasInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Delete seeded data (if any)
            migrationBuilder.DeleteData(table: "Cards", keyColumn: "Id", keyValue: 1m);
            migrationBuilder.DeleteData(table: "Cards", keyColumn: "Id", keyValue: 2m);
            migrationBuilder.DeleteData(table: "Cards", keyColumn: "Id", keyValue: 3m);
            migrationBuilder.DeleteData(table: "Cards", keyColumn: "Id", keyValue: 4m);
            migrationBuilder.DeleteData(table: "Cards", keyColumn: "Id", keyValue: 5m);

            // Drop PK to allow column drop
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cards",
                table: "Cards");

            // Drop the old Id column (decimal)
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Cards");

            // Add new Id column as int with Identity
            migrationBuilder.AddColumn<int>(
               name: "Id",
               table: "Cards",
               type: "int",
               nullable: false,
               defaultValue: 0)
               .Annotation("SqlServer:Identity", "1, 1");

            // Add PK on new Id column
            migrationBuilder.AddPrimaryKey(
                name: "PK_Cards",
                table: "Cards",
                column: "Id");

            // Insert seed data with int Ids
            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Balance", "CardHolderName", "CardNumber", "ExpiryDate", "IsActive" },
                values: new object[,]
                {
                    { 1, 1000m, "John Doe", "1234567812345678", new DateTime(2026, 12, 31), true },
                    { 2, 1500m, "Jane Smith", "8765432187654321", new DateTime(2025, 11, 30), true },
                    { 3, 2000m, "Alice Johnson", "4444333322221111", new DateTime(2027, 6, 15), true },
                    { 4, 500m, "Bob Williams", "9999888877776666", new DateTime(2024, 12, 31), false },
                    { 5, 750m, "Charlie Brown", "1111222233334444", new DateTime(2025, 8, 1), true }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete seeded data
            migrationBuilder.DeleteData(table: "Cards", keyColumn: "Id", keyValue: 1);
            migrationBuilder.DeleteData(table: "Cards", keyColumn: "Id", keyValue: 2);
            migrationBuilder.DeleteData(table: "Cards", keyColumn: "Id", keyValue: 3);
            migrationBuilder.DeleteData(table: "Cards", keyColumn: "Id", keyValue: 4);
            migrationBuilder.DeleteData(table: "Cards", keyColumn: "Id", keyValue: 5);

            // Drop PK on int Id
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cards",
                table: "Cards");

            // Drop int Id column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Cards");

            // Add old decimal Id column back (no identity)
            migrationBuilder.AddColumn<decimal>(
                name: "Id",
                table: "Cards",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            // Add PK on decimal Id
            migrationBuilder.AddPrimaryKey(
                name: "PK_Cards",
                table: "Cards",
                column: "Id");

            // Insert seed data with decimal Ids
            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Balance", "CardHolderName", "CardNumber", "ExpiryDate", "IsActive" },
                values: new object[,]
                {
                    { 1m, 1000m, "John Doe", "1234567812345678", new DateTime(2026, 12, 31), true },
                    { 2m, 1500m, "Jane Smith", "8765432187654321", new DateTime(2025, 11, 30), true },
                    { 3m, 2000m, "Alice Johnson", "4444333322221111", new DateTime(2027, 6, 15), true },
                    { 4m, 500m, "Bob Williams", "9999888877776666", new DateTime(2024, 12, 31), false },
                    { 5m, 750m, "Charlie Brown", "1111222233334444", new DateTime(2025, 8, 1), true }
                });
        }
    }
}

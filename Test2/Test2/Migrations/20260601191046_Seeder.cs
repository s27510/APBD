using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Test2.Migrations
{
    /// <inheritdoc />
    public partial class Seeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "ID", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "John", "Doe" },
                    { 2, "Jane", "Doe" },
                    { 3, "Julie", "Doe" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ID", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Apple", 3.45m },
                    { 2, "Bananas", 5.55m },
                    { 3, "Orange", 12.37m }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Created" },
                    { 2, "Ongoing" },
                    { 3, "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "ID", "ClientID", "CreatedAt", "FullfieldAt", "StatusID" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 2, 1, new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 3, 1, new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 4, 2, new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Product_Order",
                columns: new[] { "OrderID", "ProductID", "Amount" },
                values: new object[,]
                {
                    { 1, 1, 3 },
                    { 3, 1, 12 },
                    { 1, 2, 5 },
                    { 2, 2, 2 },
                    { 1, 3, 8 },
                    { 2, 3, 1 },
                    { 3, 3, 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Client",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Product_Order",
                keyColumns: new[] { "OrderID", "ProductID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Product_Order",
                keyColumns: new[] { "OrderID", "ProductID" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "Product_Order",
                keyColumns: new[] { "OrderID", "ProductID" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Product_Order",
                keyColumns: new[] { "OrderID", "ProductID" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Product_Order",
                keyColumns: new[] { "OrderID", "ProductID" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "Product_Order",
                keyColumns: new[] { "OrderID", "ProductID" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Product_Order",
                keyColumns: new[] { "OrderID", "ProductID" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Client",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Client",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}

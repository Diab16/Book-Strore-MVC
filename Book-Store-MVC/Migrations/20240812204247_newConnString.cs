using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_Store_MVC.Migrations
{
    /// <inheritdoc />
    public partial class newConnString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 8, 12, 23, 42, 46, 726, DateTimeKind.Local).AddTicks(5238));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 8, 12, 23, 42, 46, 726, DateTimeKind.Local).AddTicks(5339));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 8, 12, 23, 26, 3, 921, DateTimeKind.Local).AddTicks(8534));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 8, 12, 23, 26, 3, 921, DateTimeKind.Local).AddTicks(8652));
        }
    }
}

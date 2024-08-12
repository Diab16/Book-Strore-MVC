using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_Store_MVC.Migrations
{
    /// <inheritdoc />
    public partial class intiInlocal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 8, 8, 19, 24, 3, 86, DateTimeKind.Local).AddTicks(6691));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 8, 8, 19, 24, 3, 86, DateTimeKind.Local).AddTicks(6747));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_Store_MVC.Migrations
{
    /// <inheritdoc />
    public partial class ModifyONdeleteToBeNoAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 8, 8, 15, 43, 23, 156, DateTimeKind.Local).AddTicks(2037));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 8, 8, 15, 43, 23, 156, DateTimeKind.Local).AddTicks(2078));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 8, 8, 15, 41, 21, 723, DateTimeKind.Local).AddTicks(2173));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 8, 8, 15, 41, 21, 723, DateTimeKind.Local).AddTicks(2221));
        }
    }
}

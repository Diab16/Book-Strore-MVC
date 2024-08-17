using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_Store_MVC.Migrations
{
    /// <inheritdoc />
    public partial class Added_UniqueEmailAttribute_and_AZ_Possible_for_Email : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 8, 16, 19, 37, 57, 375, DateTimeKind.Local).AddTicks(4127));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 8, 16, 19, 37, 57, 375, DateTimeKind.Local).AddTicks(4208));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 8, 14, 4, 41, 23, 626, DateTimeKind.Local).AddTicks(848));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 8, 14, 4, 41, 23, 626, DateTimeKind.Local).AddTicks(917));
        }
    }
}

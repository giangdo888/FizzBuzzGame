using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FizzBuzzGame.Server.Migrations
{
    /// <inheritdoc />
    public partial class updateRequiredFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 17, 8, 2, 29, 974, DateTimeKind.Utc).AddTicks(7164));

            migrationBuilder.UpdateData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 17, 8, 2, 29, 974, DateTimeKind.Utc).AddTicks(7172));

            migrationBuilder.UpdateData(
                table: "GameRules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Divisor",
                value: 7);

            migrationBuilder.UpdateData(
                table: "GameRules",
                keyColumn: "Id",
                keyValue: 2,
                column: "Divisor",
                value: 11);

            migrationBuilder.UpdateData(
                table: "GameRules",
                keyColumn: "Id",
                keyValue: 3,
                column: "Divisor",
                value: 103);

            migrationBuilder.UpdateData(
                table: "GameRules",
                keyColumn: "Id",
                keyValue: 4,
                column: "Divisor",
                value: 3);

            migrationBuilder.UpdateData(
                table: "GameRules",
                keyColumn: "Id",
                keyValue: 5,
                column: "Divisor",
                value: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 16, 5, 59, 11, 401, DateTimeKind.Utc).AddTicks(5062));

            migrationBuilder.UpdateData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 16, 5, 59, 11, 401, DateTimeKind.Utc).AddTicks(5068));

            migrationBuilder.UpdateData(
                table: "GameRules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Divisor",
                value: 0);

            migrationBuilder.UpdateData(
                table: "GameRules",
                keyColumn: "Id",
                keyValue: 2,
                column: "Divisor",
                value: 0);

            migrationBuilder.UpdateData(
                table: "GameRules",
                keyColumn: "Id",
                keyValue: 3,
                column: "Divisor",
                value: 0);

            migrationBuilder.UpdateData(
                table: "GameRules",
                keyColumn: "Id",
                keyValue: 4,
                column: "Divisor",
                value: 0);

            migrationBuilder.UpdateData(
                table: "GameRules",
                keyColumn: "Id",
                keyValue: 5,
                column: "Divisor",
                value: 0);
        }
    }
}

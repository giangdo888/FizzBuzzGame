using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FizzBuzzGame.Server.Migrations
{
    /// <inheritdoc />
    public partial class addWordColumnForRule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Word",
                table: "GameRules",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 17, 10, 38, 38, 175, DateTimeKind.Utc).AddTicks(6969));

            migrationBuilder.UpdateData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 17, 10, 38, 38, 175, DateTimeKind.Utc).AddTicks(6976));

            migrationBuilder.UpdateData(
                table: "GameRules",
                keyColumns: new[] { "Divisor", "GameId" },
                keyValues: new object[] { 3, 2 },
                column: "Word",
                value: "Fizz");

            migrationBuilder.UpdateData(
                table: "GameRules",
                keyColumns: new[] { "Divisor", "GameId" },
                keyValues: new object[] { 5, 2 },
                column: "Word",
                value: "Buzz");

            migrationBuilder.UpdateData(
                table: "GameRules",
                keyColumns: new[] { "Divisor", "GameId" },
                keyValues: new object[] { 7, 1 },
                column: "Word",
                value: "Foo");

            migrationBuilder.UpdateData(
                table: "GameRules",
                keyColumns: new[] { "Divisor", "GameId" },
                keyValues: new object[] { 11, 1 },
                column: "Word",
                value: "Boo");

            migrationBuilder.UpdateData(
                table: "GameRules",
                keyColumns: new[] { "Divisor", "GameId" },
                keyValues: new object[] { 103, 1 },
                column: "Word",
                value: "Loo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Word",
                table: "GameRules");

            migrationBuilder.UpdateData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 17, 10, 5, 34, 710, DateTimeKind.Utc).AddTicks(6074));

            migrationBuilder.UpdateData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 17, 10, 5, 34, 710, DateTimeKind.Utc).AddTicks(6081));
        }
    }
}

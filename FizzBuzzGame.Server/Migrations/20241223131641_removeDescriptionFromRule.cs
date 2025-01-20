using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FizzBuzzGame.Server.Migrations
{
    /// <inheritdoc />
    public partial class removeDescriptionFromRule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "GameRules");

            migrationBuilder.UpdateData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 23, 13, 16, 40, 955, DateTimeKind.Utc).AddTicks(8727));

            migrationBuilder.UpdateData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 23, 13, 16, 40, 955, DateTimeKind.Utc).AddTicks(8736));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "GameRules",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 22, 7, 48, 42, 104, DateTimeKind.Utc).AddTicks(4879));

            migrationBuilder.UpdateData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 22, 7, 48, 42, 104, DateTimeKind.Utc).AddTicks(4891));

            migrationBuilder.UpdateData(
                table: "GameRules",
                keyColumns: new[] { "Divisor", "GameId" },
                keyValues: new object[] { 3, 2 },
                column: "Description",
                value: "Replace numbers divisible by 3 with 'Fizz'");

            migrationBuilder.UpdateData(
                table: "GameRules",
                keyColumns: new[] { "Divisor", "GameId" },
                keyValues: new object[] { 5, 2 },
                column: "Description",
                value: "Replace numbers divisible by 5 with 'Buzz'");

            migrationBuilder.UpdateData(
                table: "GameRules",
                keyColumns: new[] { "Divisor", "GameId" },
                keyValues: new object[] { 7, 1 },
                column: "Description",
                value: "Replace numbers divisible by 7 with 'Foo'");

            migrationBuilder.UpdateData(
                table: "GameRules",
                keyColumns: new[] { "Divisor", "GameId" },
                keyValues: new object[] { 11, 1 },
                column: "Description",
                value: "Replace numbers divisible by 11 with 'Boo'");

            migrationBuilder.UpdateData(
                table: "GameRules",
                keyColumns: new[] { "Divisor", "GameId" },
                keyValues: new object[] { 103, 1 },
                column: "Description",
                value: "Replace numbers divisible by 103 with 'Loo'");
        }
    }
}

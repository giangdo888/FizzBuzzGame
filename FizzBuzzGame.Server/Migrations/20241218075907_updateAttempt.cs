using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FizzBuzzGame.Server.Migrations
{
    /// <inheritdoc />
    public partial class updateAttempt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountDownTime",
                table: "Games");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Attempts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "IsCompleted" },
                values: new object[] { new DateTime(2024, 12, 18, 7, 59, 6, 832, DateTimeKind.Utc).AddTicks(2902), true });

            migrationBuilder.UpdateData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "IsCompleted" },
                values: new object[] { new DateTime(2024, 12, 18, 7, 59, 6, 832, DateTimeKind.Utc).AddTicks(2908), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Attempts");

            migrationBuilder.AddColumn<int>(
                name: "CountDownTime",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "CountDownTime",
                value: 15);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "CountDownTime",
                value: 10);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDeApplication.Migrations
{
    public partial class updateEmailCancel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "EmailCancel");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "EmailCancel");

            migrationBuilder.AddColumn<string>(
                name: "ReceivedTime",
                table: "EmailCancel",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceivedTimeFD",
                table: "EmailCancel",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceivedTime",
                table: "EmailCancel");

            migrationBuilder.DropColumn(
                name: "ReceivedTimeFD",
                table: "EmailCancel");

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "EmailCancel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "EmailCancel",
                nullable: false,
                defaultValue: 0);
        }
    }
}

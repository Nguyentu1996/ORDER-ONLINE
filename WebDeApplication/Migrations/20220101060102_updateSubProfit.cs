using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDeApplication.Migrations
{
    public partial class updateSubProfit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "subProfitId",
                table: "EmailReader",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isCheck",
                table: "EmailGroup",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "subProfitId",
                table: "EmailGroup",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmailDelay_ODParrent",
                table: "EmailDelay",
                column: "ODParrent");

            migrationBuilder.CreateIndex(
                name: "IX_EmailCancel_ODParrent",
                table: "EmailCancel",
                column: "ODParrent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmailDelay_ODParrent",
                table: "EmailDelay");

            migrationBuilder.DropIndex(
                name: "IX_EmailCancel_ODParrent",
                table: "EmailCancel");

            migrationBuilder.DropColumn(
                name: "subProfitId",
                table: "EmailReader");

            migrationBuilder.DropColumn(
                name: "isCheck",
                table: "EmailGroup");

            migrationBuilder.DropColumn(
                name: "subProfitId",
                table: "EmailGroup");
        }
    }
}

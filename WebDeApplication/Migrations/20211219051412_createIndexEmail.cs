using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDeApplication.Migrations
{
    public partial class createIndexEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "status2",
                table: "EmailReader",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ODNumber",
                table: "EmailReader",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailReader_ODNumber_status2",
                table: "EmailReader",
                columns: new[] { "ODNumber", "status2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmailReader_ODNumber_status2",
                table: "EmailReader");

            migrationBuilder.AlterColumn<string>(
                name: "status2",
                table: "EmailReader",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ODNumber",
                table: "EmailReader",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

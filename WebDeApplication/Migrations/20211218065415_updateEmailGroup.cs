using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDeApplication.Migrations
{
    public partial class updateEmailGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MessageId",
                table: "EmailGroup",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status2",
                table: "EmailGroup",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "EmailGroup");

            migrationBuilder.DropColumn(
                name: "status2",
                table: "EmailGroup");
        }
    }
}

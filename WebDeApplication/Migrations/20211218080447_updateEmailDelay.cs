using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDeApplication.Migrations
{
    public partial class updateEmailDelay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MessageId",
                table: "EmailDelay",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "EmailDelay");
        }
    }
}

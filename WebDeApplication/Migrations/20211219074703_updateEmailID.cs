using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDeApplication.Migrations
{
    public partial class updateEmailID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailId",
                table: "EmailGroup",
                newName: "EmailReaderId");

            migrationBuilder.RenameColumn(
                name: "EmailId",
                table: "EmailDelay",
                newName: "EmailGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailReaderId",
                table: "EmailGroup",
                newName: "EmailId");

            migrationBuilder.RenameColumn(
                name: "EmailGroupId",
                table: "EmailDelay",
                newName: "EmailId");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDeApplication.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "priority",
                table: "EmailGroup",
                newName: "ODParrent");

            migrationBuilder.AddColumn<DateTime>(
                name: "estimatime",
                table: "EmailGroup",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "received",
                table: "EmailGroup",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "shipped",
                table: "EmailGroup",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "tyGiaBan",
                table: "DataDauVao",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "tyGiaMua",
                table: "DataDauVao",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DashboardData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    TotalProfit = table.Column<float>(nullable: false),
                    PercentProfit = table.Column<float>(nullable: false),
                    TotalOrder = table.Column<int>(nullable: false),
                    PercentOrder = table.Column<float>(nullable: false),
                    TotalCancel = table.Column<int>(nullable: false),
                    PercentCancel = table.Column<float>(nullable: false),
                    TotalDelay = table.Column<int>(nullable: false),
                    PercentDelay = table.Column<float>(nullable: false),
                    SiteName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataProfitOrder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ODnumber = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NgayGui = table.Column<string>(nullable: true),
                    TotalProfit = table.Column<float>(nullable: false),
                    SiteName = table.Column<string>(nullable: true),
                    tyGiaMua = table.Column<int>(nullable: false),
                    tyGiaBan = table.Column<int>(nullable: false),
                    CanMua = table.Column<string>(nullable: true),
                    DaMua = table.Column<string>(nullable: true),
                    GiaUSD = table.Column<string>(nullable: true),
                    orderStop = table.Column<bool>(nullable: false),
                    GiaSale = table.Column<string>(nullable: true),
                    TongUSD = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProfitOrder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailCancel",
                columns: table => new
                {
                    ODNumber = table.Column<string>(nullable: false),
                    ODParrent = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Shippto = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailCancel", x => x.ODNumber);
                });

            migrationBuilder.CreateTable(
                name: "EmailDelay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmailId = table.Column<int>(nullable: false),
                    ODNumber = table.Column<string>(nullable: true),
                    ODParrent = table.Column<string>(nullable: true),
                    receivedTime = table.Column<string>(nullable: true),
                    fromAddress = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    shippto = table.Column<string>(nullable: true),
                    tracking = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    orderTotal = table.Column<string>(nullable: true),
                    shipped = table.Column<bool>(nullable: false),
                    estimatime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailDelay", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DashboardData");

            migrationBuilder.DropTable(
                name: "DataProfitOrder");

            migrationBuilder.DropTable(
                name: "EmailCancel");

            migrationBuilder.DropTable(
                name: "EmailDelay");

            migrationBuilder.DropColumn(
                name: "estimatime",
                table: "EmailGroup");

            migrationBuilder.DropColumn(
                name: "received",
                table: "EmailGroup");

            migrationBuilder.DropColumn(
                name: "shipped",
                table: "EmailGroup");

            migrationBuilder.DropColumn(
                name: "tyGiaBan",
                table: "DataDauVao");

            migrationBuilder.DropColumn(
                name: "tyGiaMua",
                table: "DataDauVao");

            migrationBuilder.RenameColumn(
                name: "ODParrent",
                table: "EmailGroup",
                newName: "priority");
        }
    }
}

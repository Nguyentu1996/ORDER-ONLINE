using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDeApplication.Migrations
{
    public partial class UpdateOder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "DataDauVao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    NgayGui = table.Column<string>(nullable: true),
                    MaSP = table.Column<string>(nullable: true),
                    LinkSanPham = table.Column<string>(nullable: true),
                    Mau = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    CanMua = table.Column<string>(nullable: true),
                    DaMua = table.Column<string>(nullable: true),
                    GiaUSD = table.Column<string>(nullable: true),
                    GiaSale = table.Column<string>(nullable: true),
                    ShipOrTax = table.Column<string>(nullable: true),
                    TongUSD = table.Column<string>(nullable: true),
                    Rate = table.Column<string>(nullable: true),
                    TongVND = table.Column<string>(nullable: true),
                    GhiChu = table.Column<string>(nullable: true),
                    ODNumber = table.Column<string>(nullable: true),
                    ItemInTrack = table.Column<string>(nullable: true),
                    LinkTrack = table.Column<string>(nullable: true),
                    Payment = table.Column<string>(nullable: true),
                    Debt = table.Column<string>(nullable: true),
                    Adress = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    isChecked = table.Column<bool>(nullable: false),
                    stopOrder = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<long>(nullable: false),
                    tyGiaMua = table.Column<int>(nullable: false),
                    tyGiaBan = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataDauVao", x => x.Id);
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ODNumber = table.Column<string>(nullable: true),
                    ODParrent = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Shippto = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailCancel", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "EmailGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmailId = table.Column<int>(nullable: false),
                    ODNumber = table.Column<string>(nullable: true),
                    ODParrent = table.Column<string>(nullable: true),
                    toAddress = table.Column<string>(nullable: true),
                    receivedTime = table.Column<string>(nullable: true),
                    fromAddress = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    shippto = table.Column<string>(nullable: true),
                    tracking = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    orderTotal = table.Column<string>(nullable: true),
                    received = table.Column<DateTime>(nullable: false),
                    shipped = table.Column<bool>(nullable: false),
                    estimatime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailReader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ODNumber = table.Column<string>(nullable: true),
                    summary = table.Column<string>(nullable: true),
                    sentDateInGMT = table.Column<string>(nullable: true),
                    subject = table.Column<string>(nullable: true),
                    messageId = table.Column<string>(nullable: true),
                    priority = table.Column<string>(nullable: true),
                    hasInline = table.Column<string>(nullable: true),
                    toAddress = table.Column<string>(nullable: true),
                    folderId = table.Column<string>(nullable: true),
                    ccAddress = table.Column<string>(nullable: true),
                    sender = table.Column<string>(nullable: true),
                    receivedTime = table.Column<string>(nullable: true),
                    receivedTimeLong = table.Column<long>(nullable: false),
                    fromAddress = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    status2 = table.Column<string>(nullable: true),
                    isChecked = table.Column<bool>(nullable: false),
                    orderDate = table.Column<string>(nullable: true),
                    estimateDilivery = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    shippto = table.Column<string>(nullable: true),
                    tracking = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    orderTotal = table.Column<string>(nullable: true),
                    odParrent = table.Column<string>(nullable: true),
                    shipped = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailReader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ItemCd = table.Column<string>(nullable: true),
                    ODnumber = table.Column<string>(nullable: true),
                    MessageId = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    receiveiTime = table.Column<string>(nullable: true),
                    receiveiTimeLong = table.Column<long>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Price = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DashboardData");

            migrationBuilder.DropTable(
                name: "DataDauVao");

            migrationBuilder.DropTable(
                name: "DataProfitOrder");

            migrationBuilder.DropTable(
                name: "EmailCancel");

            migrationBuilder.DropTable(
                name: "EmailDelay");

            migrationBuilder.DropTable(
                name: "EmailGroup");

            migrationBuilder.DropTable(
                name: "EmailReader");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}

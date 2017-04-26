using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.EntityFrameworkCore.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityRole<int>",
                columns: table => new
                {
                    IdentityRoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole<int>", x => x.IdentityRoleId);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUser<int>",
                columns: table => new
                {
                    IdentityUserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 255, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 255, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser<int>", x => x.IdentityUserId);
                });

            migrationBuilder.CreateTable(
                name: "IdentityRoleClaim<int>",
                columns: table => new
                {
                    IdentityRoleClaimId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRoleClaim<int>", x => x.IdentityRoleClaimId);
                    table.ForeignKey(
                        name: "FK_IdentityRoleClaim<int>_IdentityRole<int>_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IdentityRole<int>",
                        principalColumn: "IdentityRoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserClaim<int>",
                columns: table => new
                {
                    IdentityUserClaimId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserClaim<int>", x => x.IdentityUserClaimId);
                    table.ForeignKey(
                        name: "FK_IdentityUserClaim<int>_IdentityUser<int>_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser<int>",
                        principalColumn: "IdentityUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserLogin<int>",
                columns: table => new
                {
                    IdentityUserLoginId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    ProviderKey = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserLogin<int>", x => x.IdentityUserLoginId);
                    table.ForeignKey(
                        name: "FK_IdentityUserLogin<int>_IdentityUser<int>_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser<int>",
                        principalColumn: "IdentityUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserRole<int>",
                columns: table => new
                {
                    IdentityUserRoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<int>", x => x.IdentityUserRoleId);
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<int>_IdentityRole<int>_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IdentityRole<int>",
                        principalColumn: "IdentityRoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<int>_IdentityUser<int>_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser<int>",
                        principalColumn: "IdentityUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserToken<int>",
                columns: table => new
                {
                    IdentityUserTokenId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserToken<int>", x => x.IdentityUserTokenId);
                    table.ForeignKey(
                        name: "FK_IdentityUserToken<int>_IdentityUser<int>_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser<int>",
                        principalColumn: "IdentityUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "IdentityRole<int>",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityRoleClaim<int>_RoleId",
                table: "IdentityRoleClaim<int>",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "IdentityUser<int>",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "IdentityUser<int>",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserClaim<int>_UserId",
                table: "IdentityUserClaim<int>",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserLogin<int>_UserId",
                table: "IdentityUserLogin<int>",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserLogin<int>_LoginProvider_ProviderKey",
                table: "IdentityUserLogin<int>",
                columns: new[] { "LoginProvider", "ProviderKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserRole<int>_RoleId",
                table: "IdentityUserRole<int>",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserRole<int>_UserId",
                table: "IdentityUserRole<int>",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserRole<int>_UserId_RoleId",
                table: "IdentityUserRole<int>",
                columns: new[] { "UserId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserToken<int>_UserId",
                table: "IdentityUserToken<int>",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserToken<int>_UserId_LoginProvider_Name",
                table: "IdentityUserToken<int>",
                columns: new[] { "UserId", "LoginProvider", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRoleClaim<int>");

            migrationBuilder.DropTable(
                name: "IdentityUserClaim<int>");

            migrationBuilder.DropTable(
                name: "IdentityUserLogin<int>");

            migrationBuilder.DropTable(
                name: "IdentityUserRole<int>");

            migrationBuilder.DropTable(
                name: "IdentityUserToken<int>");

            migrationBuilder.DropTable(
                name: "IdentityRole<int>");

            migrationBuilder.DropTable(
                name: "IdentityUser<int>");
        }
    }
}

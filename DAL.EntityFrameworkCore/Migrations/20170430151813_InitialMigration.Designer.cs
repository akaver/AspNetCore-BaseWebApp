using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DAL.EntityFrameworkCore;

namespace DAL.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170430151813_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityRole", b =>
                {
                    b.Property<int>("IdentityRoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(255);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(255);

                    b.HasKey("IdentityRoleId");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("IdentityRoles");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityRoleClaim", b =>
                {
                    b.Property<int>("IdentityRoleClaimId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("IdentityRoleClaimId");

                    b.HasIndex("RoleId");

                    b.ToTable("IdentityRoleClaims");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUser", b =>
                {
                    b.Property<int>("IdentityUserId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(255);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(255);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(255);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(255);

                    b.HasKey("IdentityUserId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("IdentityUsers");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUserClaim", b =>
                {
                    b.Property<int>("IdentityUserClaimId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("IdentityUserClaimId");

                    b.HasIndex("UserId");

                    b.ToTable("IdentityUserClaims");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUserLogin", b =>
                {
                    b.Property<int>("IdentityUserLoginId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("ProviderKey");

                    b.Property<int>("UserId");

                    b.HasKey("IdentityUserLoginId");

                    b.HasIndex("UserId");

                    b.HasIndex("LoginProvider", "ProviderKey")
                        .IsUnique();

                    b.ToTable("IdentityUserLogins");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUserRole", b =>
                {
                    b.Property<int>("IdentityUserRoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("IdentityUserRoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId", "RoleId")
                        .IsUnique();

                    b.ToTable("IdentityUserRoles");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUserToken", b =>
                {
                    b.Property<int>("IdentityUserTokenId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<int>("UserId");

                    b.Property<string>("Value");

                    b.HasKey("IdentityUserTokenId");

                    b.HasIndex("UserId", "LoginProvider", "Name")
                        .IsUnique();

                    b.ToTable("IdentityUserTokens");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityRoleClaim", b =>
                {
                    b.HasOne("AspNetCore.Identity.Uow.Models.IdentityRole", "Role")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUserClaim", b =>
                {
                    b.HasOne("AspNetCore.Identity.Uow.Models.IdentityUser", "User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUserLogin", b =>
                {
                    b.HasOne("AspNetCore.Identity.Uow.Models.IdentityUser", "User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUserRole", b =>
                {
                    b.HasOne("AspNetCore.Identity.Uow.Models.IdentityRole", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.HasOne("AspNetCore.Identity.Uow.Models.IdentityUser", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUserToken", b =>
                {
                    b.HasOne("AspNetCore.Identity.Uow.Models.IdentityUser", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId");
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DAL.EntityFrameworkCore;

namespace DAL.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170426104734_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityRole<int>", b =>
                {
                    b.Property<int>("IdentityRoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name")
                        .HasMaxLength(255);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(255);

                    b.HasKey("IdentityRoleId");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("IdentityRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole<int>");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("IdentityRoleClaimId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("RoleId");

                    b.HasKey("IdentityRoleClaimId");

                    b.HasIndex("RoleId");

                    b.ToTable("IdentityRoleClaims");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRoleClaim<int>");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUser<int>", b =>
                {
                    b.Property<int>("IdentityUserId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

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

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser<int>");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("IdentityUserClaimId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("IdentityUserClaimId");

                    b.HasIndex("UserId");

                    b.ToTable("IdentityUserClaims");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserClaim<int>");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUserLogin<int>", b =>
                {
                    b.Property<int>("IdentityUserLoginId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("ProviderKey");

                    b.Property<int>("UserId");

                    b.HasKey("IdentityUserLoginId");

                    b.HasIndex("UserId");

                    b.HasIndex("LoginProvider", "ProviderKey")
                        .IsUnique();

                    b.ToTable("IdentityUserLogins");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserLogin<int>");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("IdentityUserRoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("IdentityUserRoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId", "RoleId")
                        .IsUnique();

                    b.ToTable("IdentityUserRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserRole<int>");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("IdentityUserTokenId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<int>("UserId");

                    b.Property<string>("Value");

                    b.HasKey("IdentityUserTokenId");

                    b.HasIndex("UserId", "LoginProvider", "Name")
                        .IsUnique();

                    b.ToTable("IdentityUserTokens");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserToken<int>");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityRole", b =>
                {
                    b.HasBaseType("AspNetCore.Identity.Uow.Models.IdentityRole<int>");


                    b.ToTable("IdentityRole");

                    b.HasDiscriminator().HasValue("IdentityRole");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityRoleClaim", b =>
                {
                    b.HasBaseType("AspNetCore.Identity.Uow.Models.IdentityRoleClaim<int>");


                    b.ToTable("IdentityRoleClaim");

                    b.HasDiscriminator().HasValue("IdentityRoleClaim");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUser", b =>
                {
                    b.HasBaseType("AspNetCore.Identity.Uow.Models.IdentityUser<int>");


                    b.ToTable("IdentityUser");

                    b.HasDiscriminator().HasValue("IdentityUser");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUserClaim", b =>
                {
                    b.HasBaseType("AspNetCore.Identity.Uow.Models.IdentityUserClaim<int>");


                    b.ToTable("IdentityUserClaim");

                    b.HasDiscriminator().HasValue("IdentityUserClaim");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUserLogin", b =>
                {
                    b.HasBaseType("AspNetCore.Identity.Uow.Models.IdentityUserLogin<int>");


                    b.ToTable("IdentityUserLogin");

                    b.HasDiscriminator().HasValue("IdentityUserLogin");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUserRole", b =>
                {
                    b.HasBaseType("AspNetCore.Identity.Uow.Models.IdentityUserRole<int>");


                    b.ToTable("IdentityUserRole");

                    b.HasDiscriminator().HasValue("IdentityUserRole");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUserToken", b =>
                {
                    b.HasBaseType("AspNetCore.Identity.Uow.Models.IdentityUserToken<int>");


                    b.ToTable("IdentityUserToken");

                    b.HasDiscriminator().HasValue("IdentityUserToken");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("AspNetCore.Identity.Uow.Models.IdentityRole<int>", "Role")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("AspNetCore.Identity.Uow.Models.IdentityUser<int>", "User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("AspNetCore.Identity.Uow.Models.IdentityUser<int>", "User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUserRole<int>", b =>
                {
                    b.HasOne("AspNetCore.Identity.Uow.Models.IdentityRole<int>", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.HasOne("AspNetCore.Identity.Uow.Models.IdentityUser<int>", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("AspNetCore.Identity.Uow.Models.IdentityUserToken<int>", b =>
                {
                    b.HasOne("AspNetCore.Identity.Uow.Models.IdentityUser<int>", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId");
                });
        }
    }
}

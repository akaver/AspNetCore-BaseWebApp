using System;
using AspNetCore.Identity.Uow.Models;
using DAL.EntityFrameworkCore.Extensions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityFrameworkCore
{
    public class ApplicationDbContext : DbContext, IDataContext
    {
        // Web scaffolding requires fully qyalified names
        public DbSet<AspNetCore.Identity.Uow.Models.IdentityRole> IdentityRoles { get; set; }
        public DbSet<AspNetCore.Identity.Uow.Models.IdentityRoleClaim> IdentityRoleClaims { get; set; }
        public DbSet<Domain.ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<AspNetCore.Identity.Uow.Models.IdentityUserClaim> IdentityUserClaims { get; set; }
        public DbSet<AspNetCore.Identity.Uow.Models.IdentityUserLogin> IdentityUserLogins { get; set; }
        public DbSet<AspNetCore.Identity.Uow.Models.IdentityUserRole> IdentityUserRoles { get; set; }
        public DbSet<AspNetCore.Identity.Uow.Models.IdentityUserToken> IdentityUserTokens { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options: options)
        { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder: modelBuilder);

            modelBuilder.DisableCascadingDeletes();

            #region Identity configuration
            // configure identity entities
            // removed composite keys
            // renamed all PK-s as <ClassName>Id
            // added all possible navigation properties 
            modelBuilder.Entity<ApplicationUser>(buildAction: b =>
            {
                b.HasIndex(indexExpression: u => u.NormalizedUserName).HasName(name: "UserNameIndex").IsUnique();
                b.HasIndex(indexExpression: u => u.NormalizedEmail).HasName(name: "EmailIndex");
                b.Property(propertyExpression: u => u.ConcurrencyStamp).IsConcurrencyToken();

                b.ToTable(name: "IdentityUsers");
            });

            modelBuilder.Entity<IdentityRole>(buildAction: b =>
            {
                b.HasIndex(indexExpression: r => r.NormalizedName).HasName(name: "RoleNameIndex").IsUnique();
                b.Property(propertyExpression: r => r.ConcurrencyStamp).IsConcurrencyToken();

                b.ToTable(name: "IdentityRoles");
            });

            modelBuilder.Entity<IdentityRoleClaim>(buildAction: b =>
            {
                b.ToTable(name: "IdentityRoleClaims");
            });


            modelBuilder.Entity<IdentityUserClaim>(buildAction: b =>
            {

                b.ToTable(name: "IdentityUserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin>(buildAction: b =>
            {
                b.HasIndex(indexExpression: l => new { l.LoginProvider, l.ProviderKey }).IsUnique();

                b.ToTable(name: "IdentityUserLogins");
            });

            modelBuilder.Entity<IdentityUserRole>(buildAction: b =>
            {
                b.HasIndex(indexExpression: r => new { r.UserId, r.RoleId }).IsUnique();

                b.ToTable(name: "IdentityUserRoles");
            });

            modelBuilder.Entity<IdentityUserToken>(buildAction: b =>
            {
                b.HasIndex(indexExpression: l => new { l.UserId, l.LoginProvider, l.Name }).IsUnique();

                b.ToTable(name: "IdentityUserTokens");
            });
            #endregion

        }


    }
}

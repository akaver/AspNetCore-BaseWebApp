using System;
using System.Collections.Generic;
using System.Text;
using AspNetCore.Identity.Uow.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Identity.Uow
{
    public static class IdentityAppExtensions
    {
        public static async void EnsureDefaultUsersAndRoles(this IApplicationBuilder app)
        {
            var users = new List<IdentityUserWithRoles>()
            {
                new IdentityUserWithRoles(name: "admin@eesti.ee", email: "admin@eesti.ee", roles: new[] { "Admin" }, password: "kala.maja"),
                new IdentityUserWithRoles(name: "user@eesti.ee", email: "user@eesti.ee", roles: new[] { "User" }, password: "kala.maja"),
                new IdentityUserWithRoles(name: "regular@eesti.ee", email: "regular@eesti.ee", roles:null, password: "kala.maja"),
            };


            // get the rolemanager and usermanager from dependency injection engine
            RoleManager<IdentityRole> roleManager = app.ApplicationServices.GetService<RoleManager<IdentityRole>>();
            UserManager<IdentityUser> userManager = app.ApplicationServices.GetService<UserManager<IdentityUser>>();

            // managers can be null?, maybe there is no identity support
            if (roleManager == null || userManager == null)
            {
                return;
            }

            foreach (var identityUser in users)
            {
                foreach (var roleName in identityUser.Roles)
                {
                    // check for role and create, if needed
                    if (!await roleManager.RoleExistsAsync(roleName: roleName))
                    {
                        await roleManager.CreateAsync(role: new IdentityRole(roleName: roleName));
                    }
                }

                var user = await userManager.FindByEmailAsync(email: identityUser.User.NormalizedEmail);
                if (user == null)
                {
                    var res = await userManager.CreateAsync(user: identityUser.User);
                    // get the user, should exist now
                    user = await userManager.FindByEmailAsync(email: identityUser.User.NormalizedEmail);
                }

                var resAddToRoles = await userManager.AddToRolesAsync(user: user, roles: identityUser.Roles);
            }
        }
    }

    public class IdentityUserWithRoles
    {
        private readonly IdentityUser _user;
        public IdentityUser User => _user;
        public string[] Roles { get; set; }
        public IdentityUserWithRoles(string name, string email, string[] roles = null, string password = "secret")
        {
            Roles = roles ?? new string[] { };
            _user = new IdentityUser
            {
                Email = email,
                NormalizedEmail = email.ToUpper(),
                UserName = name,
                NormalizedUserName = name.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(format: "D"),
            };
            _user.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(user: _user, password: password);
        }
    }
}

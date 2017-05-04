using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Models;
using DAL.Repositories;

namespace AspNetCore.Identity.Uow.Interfaces
{

    //public interface IIdentityUserRepository : IIdentityUserRepository<IdentityUser>
    //{

    //}

    public interface IIdentityUserRepository<TUser> : IRepository<TUser>
        where TUser : IdentityUser
    {
        bool Exists(int id);

        Task<bool> ExistsAsync(int id);

        Task<List<TUser>> AllIncludeRolesAsync();

        Task<TUser> FindByIdIncludeRolesAsync(int userId);

        Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default(CancellationToken));

        Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<TUser>> GetUsersInRoleAsync(int roleId, CancellationToken cancellationToken = default(CancellationToken));

    }

}

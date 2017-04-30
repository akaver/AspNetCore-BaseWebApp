using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Models;
using DAL.Repositories;

namespace AspNetCore.Identity.Uow.Interfaces
{
    public interface IIdentityUserRepository : IIdentityUserRepository<IdentityUser>
    {
    }

    public interface IIdentityUserRepository<TUser> : IIdentityUserRepository<int, TUser>
        where TUser : IdentityUser<int>, new()
    {
    }

    public interface IIdentityUserRepository<TKey, TUser> : IRepository<TUser>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>, new()
    {
        bool Exists(TKey id);

        Task<bool> ExistsAsync(TKey id);

        Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default(CancellationToken));

        Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<TUser>> GetUsersInRoleAsync(TKey roleId, CancellationToken cancellationToken = default(CancellationToken));

    }

}

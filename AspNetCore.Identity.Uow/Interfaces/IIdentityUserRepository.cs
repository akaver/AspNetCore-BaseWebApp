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

    public interface IIdentityUserRepository : IRepository<IdentityUser>
    {
        bool Exists(int id);

        Task<bool> ExistsAsync(int id);

        Task<List<IdentityUser>> AllIncludeRolesAsync();

        Task<IdentityUser> FindByIdIncludeRolesAsync(int userId);

        Task<IdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default(CancellationToken));

        Task<IdentityUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<IdentityUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<IdentityUser>> GetUsersInRoleAsync(int roleId, CancellationToken cancellationToken = default(CancellationToken));

    }

}

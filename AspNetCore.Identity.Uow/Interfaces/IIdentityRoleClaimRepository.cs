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

    public interface IIdentityRoleClaimRepository : IRepository<IdentityRoleClaim>
    {
        bool Exists(int id);
        Task<bool> ExistsAsync(int id);

        Task<List<IdentityRoleClaim>> AllIncludeRoleAsync();

        Task<IdentityRoleClaim> SingleByIdIncludeRole(int id);

        Task<IList<Claim>> GetClaimsAsync(int roleId, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveClaimAsync(int roleId, Claim claim,
            CancellationToken cancellationToken = default(CancellationToken));

    }

}

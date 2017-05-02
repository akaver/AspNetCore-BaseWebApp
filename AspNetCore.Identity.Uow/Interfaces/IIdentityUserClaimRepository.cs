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
    public interface IIdentityUserClaimRepository : IRepository<IdentityUserClaim>
    {
        bool Exists(int id);
        Task<bool> ExistsAsync(int id);

        Task<List<IdentityUserClaim>> AllIncludeUserAsync();
        Task<IdentityUserClaim> SingleByIdIncludeUserAsync(int id);

        Task<List<Claim>> GetClaimsAsync(int userId, CancellationToken cancellationToken = default(CancellationToken));

        Task ReplaceClaimAsync(int userId, Claim claim, Claim newClaim, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveClaimsAsync(int userId, IEnumerable<Claim> claims, CancellationToken cancellationToken = default(CancellationToken));

    }
}

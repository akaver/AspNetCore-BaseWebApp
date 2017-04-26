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
    public interface IIdentityRoleClaimRepository : IIdentityRoleClaimRepository<int, IdentityRoleClaim<int>>
    {
    }

    public interface IIdentityRoleClaimRepository<TKey> : IIdentityRoleClaimRepository<TKey, IdentityRoleClaim<TKey>>
        where TKey : IEquatable<TKey>
    {
    }

    public interface IIdentityRoleClaimRepository<TKey, TRoleClaim> : IRepository<TRoleClaim>
        where TKey : IEquatable<TKey>
        where TRoleClaim : class, new()
    {
        Task<IList<Claim>> GetClaimsAsync(TKey roleId, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveClaimAsync(TKey roleId, Claim claim,
            CancellationToken cancellationToken = default(CancellationToken));

    }

}

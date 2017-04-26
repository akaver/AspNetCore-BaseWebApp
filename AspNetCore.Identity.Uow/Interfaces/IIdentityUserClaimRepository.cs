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
    public interface IIdentityUserClaimRepository : IIdentityUserClaimRepository<int, IdentityUserClaim<int>>
    {

    }
    public interface IIdentityUserClaimRepository<TKey> : IIdentityUserClaimRepository<TKey, IdentityUserClaim<TKey>>
        where TKey : IEquatable<TKey>
    {

    }

    public interface IIdentityUserClaimRepository<TKey, TUserClaim> : IRepository<TUserClaim>
        where TKey: IEquatable<TKey>
        where TUserClaim : class, new()
    {
        Task<List<Claim>> GetClaimsAsync(TKey userId, CancellationToken cancellationToken = default(CancellationToken));

        Task ReplaceClaimAsync(TKey userId, Claim claim, Claim newClaim, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveClaimsAsync(TKey userId, IEnumerable<Claim> claims, CancellationToken cancellationToken = default(CancellationToken));

    }
}

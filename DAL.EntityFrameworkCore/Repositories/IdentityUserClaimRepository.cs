using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Interfaces;
using AspNetCore.Identity.Uow.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityFrameworkCore.Repositories
{
    public class IdentityUserClaimRepository : IdentityUserClaimRepository<int>, IIdentityUserClaimRepository
    {
        public IdentityUserClaimRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }
    }

    public class IdentityUserClaimRepository<TKey> : IdentityUserClaimRepository<TKey, IdentityUserClaim<TKey>>, IIdentityUserClaimRepository<TKey>
        where TKey : IEquatable<TKey>
    {
        public IdentityUserClaimRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }
    }


    public class IdentityUserClaimRepository<TKey, TUserClaim> : EFRepository<TUserClaim>, IIdentityUserClaimRepository<TKey, TUserClaim>
        where TKey: IEquatable<TKey>
        where TUserClaim: IdentityUserClaim<TKey>, new()
    {
        public IdentityUserClaimRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }

        public Task<List<Claim>> GetClaimsAsync(TKey userId, CancellationToken cancellationToken = new CancellationToken())
        {
            return RepositoryDbSet
                // ReSharper disable ArgumentsStyleNamedExpression
                .Where(predicate: uc => uc.UserId.Equals(userId))
                // ReSharper restore ArgumentsStyleNamedExpression
                .Select(selector: c => c.ToClaim())
                .ToListAsync(cancellationToken: cancellationToken);

            // UserClaims.Where(uc => uc.UserId.Equals(user.Id)).Select(c => c.ToClaim()).ToListAsync(cancellationToken);
        }

        public async Task ReplaceClaimAsync(TKey userId, Claim claim, Claim newClaim,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var matchedClaims = await RepositoryDbSet
                // ReSharper disable ArgumentsStyleNamedExpression
                .Where(predicate: uc => uc.UserId.Equals(userId) && uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type)
                // ReSharper restore ArgumentsStyleNamedExpression
                .ToListAsync(cancellationToken: cancellationToken);
            foreach (var matchedClaim in matchedClaims)
            {
                matchedClaim.ClaimValue = newClaim.Value;
                matchedClaim.ClaimType = newClaim.Type;
            }

        }

        public async Task RemoveClaimsAsync(TKey userId, IEnumerable<Claim> claims, CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var claim in claims)
            {
                var matchedClaims = await RepositoryDbSet
                    // ReSharper disable ArgumentsStyleNamedExpression
                    .Where(predicate: uc => uc.UserId.Equals(userId) && uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type)
                    // ReSharper restore ArgumentsStyleNamedExpression
                    .ToListAsync(cancellationToken: cancellationToken);
                foreach (var c in matchedClaims)
                {
                    RepositoryDbSet.Remove(entity: c);
                }
            }
        }
    }

}

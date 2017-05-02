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
    public class IdentityUserClaimRepository : EFRepository<IdentityUserClaim>, IIdentityUserClaimRepository
    {
        public IdentityUserClaimRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }

        public bool Exists(int id)
        {
            return RepositoryDbSet.Any(uc => uc.IdentityUserClaimId == id);
        }

        public Task<bool> ExistsAsync(int id)
        {
            return RepositoryDbSet.AnyAsync(uc => uc.IdentityUserClaimId == id);
        }

        public Task<List<IdentityUserClaim>> AllIncludeUserAsync()
        {
            return RepositoryDbSet.Include(uc => uc.User).ToListAsync();
        }

        public Task<IdentityUserClaim> SingleByIdIncludeUserAsync(int id)
        {
            return RepositoryDbSet.Include(uc => uc.User).SingleOrDefaultAsync(u => u.IdentityUserClaimId == id);
        }

        public Task<List<Claim>> GetClaimsAsync(int userId, CancellationToken cancellationToken = new CancellationToken())
        {
            return RepositoryDbSet
                // ReSharper disable ArgumentsStyleNamedExpression
                .Where(predicate: uc => uc.UserId == userId)
                // ReSharper restore ArgumentsStyleNamedExpression
                .Select(selector: c => c.ToClaim())
                .ToListAsync(cancellationToken: cancellationToken);

            // UserClaims.Where(uc => uc.UserId.Equals(user.Id)).Select(c => c.ToClaim()).ToListAsync(cancellationToken);
        }

        public async Task ReplaceClaimAsync(int userId, Claim claim, Claim newClaim,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var matchedClaims = await RepositoryDbSet
                // ReSharper disable ArgumentsStyleNamedExpression
                .Where(predicate: uc => uc.UserId == userId && uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type)
                // ReSharper restore ArgumentsStyleNamedExpression
                .ToListAsync(cancellationToken: cancellationToken);
            foreach (var matchedClaim in matchedClaims)
            {
                matchedClaim.ClaimValue = newClaim.Value;
                matchedClaim.ClaimType = newClaim.Type;
            }

        }

        public async Task RemoveClaimsAsync(int userId, IEnumerable<Claim> claims, CancellationToken cancellationToken = new CancellationToken())
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

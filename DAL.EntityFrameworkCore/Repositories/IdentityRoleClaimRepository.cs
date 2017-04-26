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
    public class IdentityRoleClaimRepository : IdentityRoleClaimRepository<int>, IIdentityRoleClaimRepository
    {
        public IdentityRoleClaimRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }
    }
    public class IdentityRoleClaimRepository<TKey> : IdentityRoleClaimRepository<TKey, IdentityRoleClaim<TKey>>, IIdentityRoleClaimRepository<TKey>
        where TKey : IEquatable<TKey>
    {
        public IdentityRoleClaimRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }
    }

    public class IdentityRoleClaimRepository<TKey, TIdentityRoleClaim> : EFRepository<TIdentityRoleClaim>, IIdentityRoleClaimRepository<TKey, TIdentityRoleClaim>
        where TKey : IEquatable<TKey>
        where TIdentityRoleClaim : IdentityRoleClaim<TKey>, new()
    {
        public IdentityRoleClaimRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }

        public async Task<IList<Claim>> GetClaimsAsync(TKey roleId, CancellationToken cancellationToken = new CancellationToken())
        {
            // ReSharper disable once ArgumentsStyleNamedExpression
            return await RepositoryDbSet.Where(predicate: rc => rc.RoleId.Equals(roleId)).Select(selector: c => new Claim(c.ClaimType, c.ClaimValue)).ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task RemoveClaimAsync(TKey roleId, Claim claim, CancellationToken cancellationToken = new CancellationToken())
        {
            // ReSharper disable once ArgumentsStyleNamedExpression
            var claims = await RepositoryDbSet.Where(predicate: rc => rc.RoleId.Equals(roleId) && rc.ClaimValue == claim.Value && rc.ClaimType == claim.Type).ToListAsync(cancellationToken: cancellationToken);
            foreach (var c in claims)
            {
                RepositoryDbSet.Remove(entity: c);
            }

        }
    }

}

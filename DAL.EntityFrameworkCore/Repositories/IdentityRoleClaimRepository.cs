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
    public class IdentityRoleClaimRepository : EFRepository<IdentityRoleClaim>, IIdentityRoleClaimRepository
    {
        public bool Exists(int id)
        {
            return RepositoryDbSet.Any(r => r.IdentityRoleClaimId.Equals(id));
        }

        public Task<bool> ExistsAsync(int id)
        {
            return RepositoryDbSet.AnyAsync(r => r.IdentityRoleClaimId.Equals(id));
        }

        public Task<List<IdentityRoleClaim>> AllIncludeRoleAsync()
        {
            return RepositoryDbSet.Include(r => r.Role).ToListAsync();
        }

        public Task<IdentityRoleClaim> SingleByIdIncludeRole(int id)
        {
            return RepositoryDbSet.Include(rc => rc.Role).SingleOrDefaultAsync(rc => rc.IdentityRoleClaimId == id);
        }

        public async Task<IList<Claim>> GetClaimsAsync(int roleId, CancellationToken cancellationToken = new CancellationToken())
        {
            // ReSharper disable once ArgumentsStyleNamedExpression
            return await RepositoryDbSet.Where(predicate: rc => rc.RoleId.Equals(roleId)).Select(selector: c => new Claim(c.ClaimType, c.ClaimValue)).ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task RemoveClaimAsync(int roleId, Claim claim, CancellationToken cancellationToken = new CancellationToken())
        {
            // ReSharper disable once ArgumentsStyleNamedExpression
            var claims = await RepositoryDbSet.Where(predicate: rc => rc.RoleId.Equals(roleId) && rc.ClaimValue == claim.Value && rc.ClaimType == claim.Type).ToListAsync(cancellationToken: cancellationToken);
            foreach (var c in claims)
            {
                RepositoryDbSet.Remove(entity: c);
            }

        }

        public IdentityRoleClaimRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }

    }

}

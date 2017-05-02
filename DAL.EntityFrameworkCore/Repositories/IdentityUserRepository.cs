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

    public class IdentityUserRepository : EFRepository<IdentityUser>, IIdentityUserRepository
    {
        public IdentityUserRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }

        public bool Exists(int id)
        {
            // ReSharper disable ArgumentsStyleNamedExpression
            return RepositoryDbSet.Any(predicate: u => u.IdentityUserId.Equals(id));
            // ReSharper restore ArgumentsStyleNamedExpression
        }

        public Task<bool> ExistsAsync(int id)
        {
            // ReSharper disable ArgumentsStyleNamedExpression
            return RepositoryDbSet.AnyAsync(predicate: u => u.IdentityUserId.Equals(id));
            // ReSharper restore ArgumentsStyleNamedExpression
        }

        public Task<List<IdentityUser>> AllIncludeRolesAsync()
        {
            return RepositoryDbSet.Include(u => u.Roles).ThenInclude(r => r.Role).ToListAsync();
        }

        public Task<IdentityUser> FindByIdIncludeRolesAsync(int userId)
        {
            return RepositoryDbSet.Include(ur => ur.Roles)
                .ThenInclude(r => r.Role)
                .SingleOrDefaultAsync(u => u.IdentityUserId == userId);
        }

        public Task<IdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return RepositoryDbSet.FirstOrDefaultAsync(predicate: u => u.NormalizedUserName == normalizedUserName, cancellationToken: cancellationToken);
        }

        public Task<IdentityUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken))
        {
            return RepositoryDbSet.FirstOrDefaultAsync(predicate: u => u.NormalizedEmail == normalizedEmail, cancellationToken: cancellationToken);
        }

        public Task<List<IdentityUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        {
            // TODO: Is this working?

            var query = RepositoryDbSet
                // ReSharper disable ArgumentsStyleAnonymousFunction
                .Where(predicate: u => u.Claims.Any(c => c.ClaimValue == claim.Value && c.ClaimType == claim.Type));
                // ReSharper restore ArgumentsStyleAnonymousFunction

            return query.ToListAsync(cancellationToken: cancellationToken);

            //var query = from userclaims in UserClaims
            //    join user in Users on userclaims.UserId equals user.Id
            //    where userclaims.ClaimValue == claim.Value
            //          && userclaims.ClaimType == claim.Type
            //    select user;

            //return await query.ToListAsync(cancellationToken);
        }

        public Task<List<IdentityUser>> GetUsersInRoleAsync(int roleId, CancellationToken cancellationToken = new CancellationToken())
        {
            var query = RepositoryDbSet
                // ReSharper disable ArgumentsStyleAnonymousFunction
                .Where(predicate: u => u.Roles.Any(r => r.RoleId.Equals(roleId)));
                // ReSharper restore ArgumentsStyleAnonymousFunction

            return query.ToListAsync(cancellationToken: cancellationToken);

            //var query = from userrole in UserRoles
            //    join user in Users on userrole.UserId equals user.Id
            //    where userrole.RoleId.Equals(role.Id)
            //    select user;

            //return await query.ToListAsync(cancellationToken);

        }
    }
}

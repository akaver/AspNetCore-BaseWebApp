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
    public class IdentityUserRepository : IdentityUserRepository<int>, IIdentityUserRepository
    {
        public IdentityUserRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }
    }

    public class IdentityUserRepository<TKey> : IdentityUserRepository<TKey, IdentityUser<TKey>>, IIdentityUserRepository<TKey>
        where TKey: IEquatable<TKey>
    {
        public IdentityUserRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }
    }

    public class IdentityUserRepository<TKey, TUser> : EFRepository<TUser>, IIdentityUserRepository<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>, new()
    {
        public IdentityUserRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }

        public Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return RepositoryDbSet.FirstOrDefaultAsync(predicate: u => u.NormalizedUserName == normalizedUserName, cancellationToken: cancellationToken);
        }

        public Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken))
        {
            return RepositoryDbSet.FirstOrDefaultAsync(predicate: u => u.NormalizedEmail == normalizedEmail, cancellationToken: cancellationToken);
        }

        public Task<List<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = default(CancellationToken))
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

        public Task<List<TUser>> GetUsersInRoleAsync(TKey roleId, CancellationToken cancellationToken = new CancellationToken())
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

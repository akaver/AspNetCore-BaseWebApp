using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Interfaces;
using AspNetCore.Identity.Uow.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityFrameworkCore.Repositories
{

    public class IdentityUserRoleRepository : IdentityUserRoleRepository<int>, IIdentityUserRoleRepository
    {
        public IdentityUserRoleRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }
    }

    public class IdentityUserRoleRepository<TKey> : IdentityUserRoleRepository<TKey, IdentityUserRole<TKey>>, IIdentityUserRoleRepository<TKey>
        where TKey : IEquatable<TKey>
    {
        public IdentityUserRoleRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }
    }



    public class IdentityUserRoleRepository<TKey, TUserRole> : EFRepository<TUserRole>, IIdentityUserRoleRepository<TKey, TUserRole>
        where TKey : IEquatable<TKey>
        where TUserRole : IdentityUserRole<TKey>, new()
    {
        public IdentityUserRoleRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }

        public Task<TUserRole> FindUserRoleAsync(TKey userId, TKey roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return RepositoryDbSet.FirstOrDefaultAsync(
                // ReSharper disable ArgumentsStyleNamedExpression
                predicate: u => u.UserId.Equals(userId) && u.RoleId.Equals(roleId),
                // ReSharper restore ArgumentsStyleNamedExpression
                cancellationToken: cancellationToken);
        }

        public Task<List<string>> GetRolesAsync(TKey userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = RepositoryDbSet.Where(u => u.UserId.Equals(userId))
                .Include(a => a.Role)
                .Select(r => r.Role.Name);

            return query.ToListAsync(cancellationToken);

            //var query = from userRole in UserRoles
            //    join role in Roles on userRole.RoleId equals role.Id
            //    where userRole.UserId.Equals(userId)
            //    select role.Name;
            //return await query.ToListAsync();

        }
    }



}

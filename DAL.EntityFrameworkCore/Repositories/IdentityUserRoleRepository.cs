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
    public class IdentityUserRoleRepository : EFRepository<IdentityUserRole>, IIdentityUserRoleRepository
    {
        public IdentityUserRoleRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }

        public bool Exists(int id)
        {
            return RepositoryDbSet.Any(r => r.IdentityUserRoleId.Equals(id));
        }

        public Task<bool> ExistsAsync(int id)
        {
            return RepositoryDbSet.AnyAsync(r => r.IdentityUserRoleId.Equals(id));
        }

        
        public Task<IdentityUserRole> SingleIncludeUserAndRoleAsync(int id)
        {
            return RepositoryDbSet.Include(i => i.Role)
                .Include(i => i.User)
                .SingleOrDefaultAsync(m => m.IdentityUserRoleId == id);
        }



        public Task<List<IdentityUserRole>> AllIncludeRoleAndUserAsync()
        {
            return RepositoryDbSet.Include(r => r.Role).Include(u => u.User).ToListAsync();
        }

        public Task<IdentityUserRole> FindUserRoleAsync(int userId, int roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return RepositoryDbSet.FirstOrDefaultAsync(
                // ReSharper disable ArgumentsStyleNamedExpression
                predicate: u => u.UserId.Equals(userId) && u.RoleId.Equals(roleId),
                // ReSharper restore ArgumentsStyleNamedExpression
                cancellationToken: cancellationToken);
        }

        public Task<List<string>> GetRolesAsync(int userId, CancellationToken cancellationToken = default(CancellationToken))
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

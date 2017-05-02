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
    public class IdentityRoleRepository : EFRepository<IdentityRole>, IIdentityRoleRepository
    {
        public IdentityRoleRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }

        public bool Exists(int id)
        {
            return RepositoryDbSet.Any(r => r.IdentityRoleId.Equals(id));
        }

        public Task<bool> ExistsAsync(int id)
        {
            return RepositoryDbSet.AnyAsync(r => r.IdentityRoleId.Equals(id));
        }

        public Task<IdentityRole> SingleByIdIncludeUserAsync(int id)
        {
            return RepositoryDbSet.Include(r => r.Users)
                .ThenInclude(u => u.User)
                .SingleOrDefaultAsync(r => r.IdentityRoleId == id);
        }

        public Task<List<IdentityRole>> AllIncludeUserAsync()
        {
            return RepositoryDbSet.Include(r => r.Users).ThenInclude(u => u.User).ToListAsync();
        }

        public Task<IdentityRole> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = new CancellationToken())
        {
            return RepositoryDbSet.FirstOrDefaultAsync(predicate: r => r.NormalizedName == normalizedName, cancellationToken: cancellationToken);
        }


    }

}

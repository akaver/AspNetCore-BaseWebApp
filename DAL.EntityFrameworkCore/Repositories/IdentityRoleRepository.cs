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

    public class IdentityRoleRepository : IdentityRoleRepository<IdentityRole>, IIdentityRoleRepository
    {
        public IdentityRoleRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }
    }

    public class IdentityRoleRepository<TRole> : IdentityRoleRepository<int, TRole>, IIdentityRoleRepository<TRole>
        where TRole : IdentityRole<int>, new()
    {
        public IdentityRoleRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }
    }


    public class IdentityRoleRepository<TKey, TRole> : EFRepository<TRole>, IIdentityRoleRepository<TKey, TRole>
        where TKey : IEquatable<TKey>
        where TRole : IdentityRole<TKey>, new()
    {
        public IdentityRoleRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }

        public bool Exists(TKey id)
        {
            return RepositoryDbSet.Any(r => r.IdentityRoleId.Equals(id));
        }

        public Task<TRole> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = new CancellationToken())
        {
            return RepositoryDbSet.FirstOrDefaultAsync(predicate: r => r.NormalizedName == normalizedName, cancellationToken: cancellationToken);
        }


    }

}

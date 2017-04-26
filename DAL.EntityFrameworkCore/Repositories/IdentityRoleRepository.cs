using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Interfaces;
using AspNetCore.Identity.Uow.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityFrameworkCore.Repositories
{

    public class IdentityRoleRepository : IdentityRoleRepository<int>, IIdentityRoleRepository
    {
        public IdentityRoleRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }
    }

    public class IdentityRoleRepository<TKey> : IdentityRoleRepository<TKey, IdentityRole<TKey>>, IIdentityRoleRepository<TKey>
        where TKey : IEquatable<TKey>
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

        public Task<TRole> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = new CancellationToken())
        {
            return RepositoryDbSet.FirstOrDefaultAsync(predicate: r => r.NormalizedName == normalizedName, cancellationToken: cancellationToken);
        }


    }

}

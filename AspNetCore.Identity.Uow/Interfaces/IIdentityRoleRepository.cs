using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Models;
using DAL.Repositories;

namespace AspNetCore.Identity.Uow.Interfaces
{
    public interface IIdentityRoleRepository : IIdentityRoleRepository<int, IdentityRole<int>>
    {

    }

    public interface IIdentityRoleRepository<TKey> : IIdentityRoleRepository<TKey, IdentityRole<TKey>>
        where TKey : IEquatable<TKey>
    {

    }

    public interface IIdentityRoleRepository<TKey, TRole> : IRepository<TRole>
        where TKey : IEquatable<TKey>
        where TRole : class, new()
    {
        Task<TRole> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = default(CancellationToken));
    }

}

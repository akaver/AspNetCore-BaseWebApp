using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Models;
using DAL.Repositories;

namespace AspNetCore.Identity.Uow.Interfaces
{
    public interface IIdentityRoleRepository : IIdentityRoleRepository<IdentityRole>
    {

    }

    public interface IIdentityRoleRepository<TRole> : IIdentityRoleRepository<int, TRole>
        where TRole : IdentityRole<int>, new()
    {

    }

    public interface IIdentityRoleRepository<TKey, TRole> : IRepository<TRole>
        where TKey : IEquatable<TKey>
        where TRole : IdentityRole<TKey>, new()
    {
        bool Exists(TKey id);

        Task<TRole> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = default(CancellationToken));
    }

}

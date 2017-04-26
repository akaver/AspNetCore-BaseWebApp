using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Models;
using DAL.Repositories;

namespace AspNetCore.Identity.Uow.Interfaces
{
    public interface IIdentityUserRoleRepository : IIdentityUserRoleRepository<int>
    {

    }
    public interface IIdentityUserRoleRepository<TKey> : IIdentityUserRoleRepository<TKey, IdentityUserRole<TKey>>
        where TKey : IEquatable<TKey>
    {

    }

    public interface IIdentityUserRoleRepository<TKey, TUserRole> : IRepository<TUserRole>
        where TKey : IEquatable<TKey>
        where TUserRole : class, new()
    {
        Task<TUserRole> FindUserRoleAsync(TKey userId, TKey roleId, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<string>> GetRolesAsync(TKey userId, CancellationToken cancellationToken = default(CancellationToken));
    }
}

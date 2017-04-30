using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Models;
using DAL.Repositories;

namespace AspNetCore.Identity.Uow.Interfaces
{
    public interface IIdentityUserRoleRepository : IIdentityUserRoleRepository<IdentityUserRole<int>>
    {

    }
    public interface IIdentityUserRoleRepository<TUserRole> : IIdentityUserRoleRepository<int, TUserRole>
        where TUserRole : IdentityUserRole<int>, new()
    {

    }

    public interface IIdentityUserRoleRepository<TKey, TUserRole> : IRepository<TUserRole>
        where TKey : IEquatable<TKey>
        where TUserRole : IdentityUserRole<TKey>, new()
    {
        Task<TUserRole> FindUserRoleAsync(TKey userId, TKey roleId, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<string>> GetRolesAsync(TKey userId, CancellationToken cancellationToken = default(CancellationToken));
    }
}

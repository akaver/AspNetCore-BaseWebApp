using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Identity.Uow.Interfaces
{
    public interface IIdentityUserLoginRepository : IIdentityUserLoginRepository<int, IdentityUserLogin<int>>
    {

    }
    public interface IIdentityUserLoginRepository<TKey> : IIdentityUserLoginRepository<TKey, IdentityUserLogin<TKey>>
        where TKey : IEquatable<TKey>
    {

    }

    public interface IIdentityUserLoginRepository<TKey, TUserLogin> : IRepository<TUserLogin>
        where TKey: IEquatable<TKey>
        where TUserLogin : class, new()
    {
        Task<TUserLogin> FindUserLoginAsync(TKey userId, string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken));

        Task<TUserLogin> FindUserLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<UserLoginInfo>> GetLoginsAsync(TKey userId, CancellationToken cancellationToken = default(CancellationToken));
    }

}

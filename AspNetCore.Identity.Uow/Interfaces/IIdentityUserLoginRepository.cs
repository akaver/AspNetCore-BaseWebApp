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
    public interface IIdentityUserLoginRepository : IIdentityUserLoginRepository<IdentityUserLogin<int>>
    {

    }
    public interface IIdentityUserLoginRepository<TUserLogin> : IIdentityUserLoginRepository<int, TUserLogin>
        where TUserLogin : IdentityUserLogin<int>, new()
    {

    }

    public interface IIdentityUserLoginRepository<TKey, TUserLogin> : IRepository<TUserLogin>
        where TKey: IEquatable<TKey>
        where TUserLogin : IdentityUserLogin<TKey>, new()
    {
        Task<TUserLogin> FindUserLoginAsync(TKey userId, string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken));

        Task<TUserLogin> FindUserLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<UserLoginInfo>> GetLoginsAsync(TKey userId, CancellationToken cancellationToken = default(CancellationToken));
    }

}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Models;
using DAL.Repositories;

namespace AspNetCore.Identity.Uow.Interfaces
{
    public interface IIdentityUserTokenRepository : IIdentityUserTokenRepository<IdentityUserToken<int>>
    {

    }
    public interface IIdentityUserTokenRepository<TUserToken> : IIdentityUserTokenRepository<int, TUserToken>
        where TUserToken : IdentityUserToken<int>, new()
    {

    }

    public interface IIdentityUserTokenRepository<TKey, TUserToken> : IRepository<TUserToken>
        where TKey : IEquatable<TKey>
        where TUserToken : IdentityUserToken<TKey>, new()
    {
        Task<TUserToken> FindTokenAsync(TKey userId, string loginProvider, string name, CancellationToken cancellationToken = default(CancellationToken));

    }
}

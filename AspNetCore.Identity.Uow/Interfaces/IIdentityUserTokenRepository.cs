using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Models;
using DAL.Repositories;

namespace AspNetCore.Identity.Uow.Interfaces
{
    public interface IIdentityUserTokenRepository : IIdentityUserTokenRepository<int, IdentityUserToken<int>>
    {

    }
    public interface IIdentityUserTokenRepository<TKey> : IIdentityUserTokenRepository<TKey, IdentityUserToken<TKey>>
        where TKey : IEquatable<TKey>
    {

    }

    public interface IIdentityUserTokenRepository<TKey, TUserToken> : IRepository<TUserToken>
        where TKey : IEquatable<TKey>
        where TUserToken : class, new()
    {
        Task<TUserToken> FindTokenAsync(TKey userId, string loginProvider, string name, CancellationToken cancellationToken = default(CancellationToken));

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Interfaces;
using AspNetCore.Identity.Uow.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityFrameworkCore.Repositories
{

    public class IdentityUserLoginRepository : IdentityUserLoginRepository<int>, IIdentityUserLoginRepository
    {
        public IdentityUserLoginRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }
    }
    public class IdentityUserLoginRepository<TKey> : IdentityUserLoginRepository<TKey, IdentityUserLogin<TKey>>, IIdentityUserLoginRepository<TKey>
        where TKey : IEquatable<TKey>
    {
        public IdentityUserLoginRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }
    }


    public class IdentityUserLoginRepository<TKey, TUserLogin> : EFRepository<TUserLogin>, IIdentityUserLoginRepository<TKey, TUserLogin>
        where TKey : IEquatable<TKey>
        where TUserLogin : IdentityUserLogin<TKey>, new()
    {
        public IdentityUserLoginRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }

        public Task<TUserLogin> FindUserLoginAsync(TKey userId, string loginProvider, string providerKey,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return RepositoryDbSet.SingleOrDefaultAsync(
                // ReSharper disable ArgumentsStyleNamedExpression
                predicate: userLogin => userLogin.UserId.Equals(userId) && userLogin.LoginProvider == loginProvider && userLogin.ProviderKey == providerKey,
                // ReSharper restore ArgumentsStyleNamedExpression
                cancellationToken: cancellationToken);

        }

        public Task<TUserLogin> FindUserLoginAsync(string loginProvider, string providerKey,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return RepositoryDbSet.SingleOrDefaultAsync(
                // ReSharper disable ArgumentsStyleNamedExpression
                predicate: userLogin => userLogin.LoginProvider == loginProvider && userLogin.ProviderKey == providerKey,
                // ReSharper restore ArgumentsStyleNamedExpression
                cancellationToken: cancellationToken);
        }

        public Task<List<UserLoginInfo>> GetLoginsAsync(TKey userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return RepositoryDbSet
                // ReSharper disable ArgumentsStyleNamedExpression
                .Where(predicate: l => l.UserId.Equals(userId))
                .Select(selector: l => new UserLoginInfo(l.LoginProvider, l.ProviderKey,l.ProviderDisplayName))
                // ReSharper restore ArgumentsStyleNamedExpression
                .ToListAsync(cancellationToken: cancellationToken);

        }
    }
}

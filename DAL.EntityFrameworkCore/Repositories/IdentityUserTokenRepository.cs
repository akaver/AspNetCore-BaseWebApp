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
    public class IdentityUserTokenRepository : IdentityUserTokenRepository<IdentityUserToken<int>>, IIdentityUserTokenRepository
    {
        public IdentityUserTokenRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }
    }

    public class IdentityUserTokenRepository<TUserToken> : IdentityUserTokenRepository<int, TUserToken>, IIdentityUserTokenRepository<TUserToken>
        where TUserToken : IdentityUserToken<int>, new()
    {
        public IdentityUserTokenRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }
    }

    public class IdentityUserTokenRepository<TKey, TUserToken> : EFRepository<TUserToken>, IIdentityUserTokenRepository<TKey, TUserToken>
            where TKey : IEquatable<TKey>
            where TUserToken : IdentityUserToken<TKey>, new()
    {
        public IdentityUserTokenRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }


        public Task<TUserToken> FindTokenAsync(TKey userId, string loginProvider, string name,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return RepositoryDbSet
                // ReSharper disable ArgumentsStyleNamedExpression
                .FirstOrDefaultAsync(predicate: ut => ut.UserId.Equals(userId) && ut.LoginProvider == loginProvider && ut.Name == name, cancellationToken: cancellationToken);
            // ReSharper restore ArgumentsStyleNamedExpression

            // FindAsync(new object[] { user.Id, loginProvider, name }, cancellationToken);
        }
    }
}

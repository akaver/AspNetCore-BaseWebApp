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

    public class IdentityUserLoginRepository : EFRepository<IdentityUserLogin>, IIdentityUserLoginRepository
    {
        public IdentityUserLoginRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }

        public bool Exists(int id)
        {
            return RepositoryDbSet.Any(ul => ul.IdentityUserLoginId == id);
        }

        public Task<bool> ExistsAsync(int id)
        {
            return RepositoryDbSet.AnyAsync(ul => ul.IdentityUserLoginId == id);
        }

        public Task<List<IdentityUserLogin>> AllIncludeUserAsync()
        {
            return RepositoryDbSet.Include(ul => ul.User).ToListAsync();
        }

        public Task<IdentityUserLogin> SingleByIdIncludeUserAsync(int id)
        {
            return RepositoryDbSet.Include(ul => ul.User).SingleOrDefaultAsync(ul => ul.IdentityUserLoginId == id);
        }

        public Task<IdentityUserLogin> FindUserLoginAsync(int userId, string loginProvider, string providerKey,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return RepositoryDbSet.SingleOrDefaultAsync(
                // ReSharper disable ArgumentsStyleNamedExpression
                predicate: userLogin => userLogin.UserId.Equals(userId) && userLogin.LoginProvider == loginProvider && userLogin.ProviderKey == providerKey,
                // ReSharper restore ArgumentsStyleNamedExpression
                cancellationToken: cancellationToken);

        }

        public Task<IdentityUserLogin> FindUserLoginAsync(string loginProvider, string providerKey,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return RepositoryDbSet.SingleOrDefaultAsync(
                // ReSharper disable ArgumentsStyleNamedExpression
                predicate: userLogin => userLogin.LoginProvider == loginProvider && userLogin.ProviderKey == providerKey,
                // ReSharper restore ArgumentsStyleNamedExpression
                cancellationToken: cancellationToken);
        }

        public Task<List<UserLoginInfo>> GetLoginsAsync(int userId, CancellationToken cancellationToken = default(CancellationToken))
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

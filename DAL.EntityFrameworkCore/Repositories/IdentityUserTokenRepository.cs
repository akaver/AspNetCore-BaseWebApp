using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Interfaces;
using AspNetCore.Identity.Uow.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityFrameworkCore.Repositories
{

    public class IdentityUserTokenRepository : EFRepository<IdentityUserToken>, IIdentityUserTokenRepository
    {
        public IdentityUserTokenRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }


        public bool Exists(int id)
        {
            return RepositoryDbSet.Any(ut => ut.IdentityUserTokenId == id);
        }

        public Task<bool> ExistsAsync(int id)
        {
            return RepositoryDbSet.AnyAsync(ut => ut.IdentityUserTokenId == id);
        }

        public Task<List<IdentityUserToken>> AllIncludeUserAsync()
        {
            return RepositoryDbSet.Include(ut => ut.User).ToListAsync();
        }

        public Task<IdentityUserToken> SingleByIdIncludeUserAsync(int id)
        {
            return RepositoryDbSet.Include(ut => ut.User).SingleOrDefaultAsync(ut => ut.IdentityUserTokenId == id);
        }

        public Task<IdentityUserToken> FindTokenAsync(int userId, string loginProvider, string name,
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

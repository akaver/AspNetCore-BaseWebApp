using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Interfaces;
using AspNetCore.Identity.Uow.Models;

namespace DAL.Rest.Repositories
{
    public class IdentityUserTokenRepository: RestRepository<IdentityUserToken>, IIdentityUserTokenRepository
    {
        public IdentityUserTokenRepository(IDataContext context, string endPoint) : base(context, endPoint)
        {
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<IdentityUserToken>> AllIncludeUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUserToken> SingleByIdIncludeUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUserToken> FindTokenAsync(int userId, string loginProvider, string name,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}

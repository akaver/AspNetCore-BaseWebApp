using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Interfaces;
using AspNetCore.Identity.Uow.Models;
using Microsoft.AspNetCore.Identity;

namespace DAL.Rest.Repositories
{
    public class IdentityUserLoginRepository : RestRepository<IdentityUserLogin>, IIdentityUserLoginRepository
    {
        public IdentityUserLoginRepository(IDataContext context, string endPoint) : base(context, endPoint)
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

        public Task<List<IdentityUserLogin>> AllIncludeUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUserLogin> SingleByIdIncludeUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUserLogin> FindUserLoginAsync(int userId, string loginProvider, string providerKey,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUserLogin> FindUserLoginAsync(string loginProvider, string providerKey,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<List<UserLoginInfo>> GetLoginsAsync(int userId, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}

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
    public interface IIdentityUserLoginRepository : IRepository<IdentityUserLogin>
    {
        bool Exists(int id);
        Task<bool> ExistsAsync(int id);

        Task<List<IdentityUserLogin>> AllIncludeUserAsync();
        Task<IdentityUserLogin> SingleByIdIncludeUserAsync(int id);

        Task<IdentityUserLogin> FindUserLoginAsync(int userId, string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken));

        Task<IdentityUserLogin> FindUserLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<UserLoginInfo>> GetLoginsAsync(int userId, CancellationToken cancellationToken = default(CancellationToken));
    }

}

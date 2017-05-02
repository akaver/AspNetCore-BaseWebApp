using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Models;
using DAL.Repositories;

namespace AspNetCore.Identity.Uow.Interfaces
{
    public interface IIdentityUserTokenRepository : IRepository<IdentityUserToken>
    {
        bool Exists(int id);

        Task<bool> ExistsAsync(int id);

        Task<List<IdentityUserToken>> AllIncludeUserAsync();

        Task<IdentityUserToken> SingleByIdIncludeUserAsync(int id);

        Task<IdentityUserToken> FindTokenAsync(int userId, string loginProvider, string name, CancellationToken cancellationToken = default(CancellationToken));

    }
}

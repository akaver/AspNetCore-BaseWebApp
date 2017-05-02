using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Models;
using DAL.Repositories;

namespace AspNetCore.Identity.Uow.Interfaces
{

    public interface IIdentityUserRoleRepository : IRepository<IdentityUserRole>
    {
        bool Exists(int id);
        Task<bool> ExistsAsync(int id);

        Task<IdentityUserRole> SingleIncludeUserAndRoleAsync(int id);

        Task<List<IdentityUserRole>> AllIncludeRoleAndUserAsync();

        Task<IdentityUserRole> FindUserRoleAsync(int userId, int roleId, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<string>> GetRolesAsync(int userId, CancellationToken cancellationToken = default(CancellationToken));
    }
}

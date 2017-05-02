using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Models;
using DAL.Repositories;

namespace AspNetCore.Identity.Uow.Interfaces
{
 
    public interface IIdentityRoleRepository : IRepository<IdentityRole>
    {
        bool Exists(int id);
        Task<bool> ExistsAsync(int id);

        Task<IdentityRole> SingleByIdIncludeUserAsync(int id);

        Task<List<IdentityRole>> AllIncludeUserAsync();

        Task<IdentityRole> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = default(CancellationToken));
    }

}

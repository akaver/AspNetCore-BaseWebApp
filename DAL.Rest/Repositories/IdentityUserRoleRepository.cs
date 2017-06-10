using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Interfaces;
using AspNetCore.Identity.Uow.Models;

namespace DAL.Rest.Repositories
{
    public class IdentityUserRoleRepository : RestRepository<IdentityUserRole>, IIdentityUserRoleRepository
    {
        public IdentityUserRoleRepository(IDataContext context, string endPoint) : base(context, endPoint)
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

        public Task<IdentityUserRole> SingleIncludeUserAndRoleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<IdentityUserRole>> AllIncludeRoleAndUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUserRole> FindUserRoleAsync(int userId, int roleId, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetRolesAsync(int userId, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}

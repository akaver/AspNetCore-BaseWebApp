using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Interfaces;
using AspNetCore.Identity.Uow.Models;

namespace DAL.Rest.Repositories
{
    public class IdentityRoleRepository : RestRepository<IdentityRole>, IIdentityRoleRepository
    {
        public IdentityRoleRepository(IDataContext context, string endPoint) : base(context, endPoint)
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

        public Task<IdentityRole> SingleByIdIncludeUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<IdentityRole>> AllIncludeUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityRole> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}

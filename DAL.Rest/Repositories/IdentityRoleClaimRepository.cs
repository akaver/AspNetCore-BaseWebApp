using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Interfaces;
using AspNetCore.Identity.Uow.Models;

namespace DAL.Rest.Repositories
{
    public class IdentityRoleClaimRepository : RestRepository<IdentityRoleClaim>, IIdentityRoleClaimRepository
    {
        public IdentityRoleClaimRepository(IDataContext context, string endPoint) : base(context, endPoint)
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

        public Task<List<IdentityRoleClaim>> AllIncludeRoleAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityRoleClaim> SingleByIdIncludeRole(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Claim>> GetClaimsAsync(int roleId, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(int roleId, Claim claim, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}

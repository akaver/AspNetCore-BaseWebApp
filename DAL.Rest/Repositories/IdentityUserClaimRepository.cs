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
    public class IdentityUserClaimRepository : RestRepository<IdentityUserClaim>, IIdentityUserClaimRepository
    {
        public IdentityUserClaimRepository(IDataContext context, string endPoint) : base(context, endPoint)
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

        public Task<List<IdentityUserClaim>> AllIncludeUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUserClaim> SingleByIdIncludeUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Claim>> GetClaimsAsync(int userId, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task ReplaceClaimAsync(int userId, Claim claim, Claim newClaim,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimsAsync(int userId, IEnumerable<Claim> claims, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}

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
    public class IdentityUserRepository<TUser> : RestRepository<TUser>, IIdentityUserRepository<TUser>
        where TUser : IdentityUser
    {
        public IdentityUserRepository(IDataContext context, string endPoint) : base(context, endPoint)
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

        public Task<List<TUser>> AllIncludeRolesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TUser> FindByIdIncludeRolesAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<List<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<List<TUser>> GetUsersInRoleAsync(int roleId, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}

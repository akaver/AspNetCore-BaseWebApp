using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
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

        public async Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = new CancellationToken())
        {
            //return RepositoryDbSet.FirstOrDefaultAsync(predicate: u => u.NormalizedEmail == normalizedEmail, cancellationToken: cancellationToken);

            var uri = EndPoint + "/" + nameof(FindByEmailAsync) + "/" + normalizedEmail;

            var response = await HttpClient.GetAsync(requestUri: uri, cancellationToken: cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                _serializer = new DataContractJsonSerializer(type: typeof(TUser));
                var content = await response.Content.ReadAsStringAsync();

                var res = _serializer.ReadObject(stream: new MemoryStream(buffer: Encoding.UTF8.GetBytes(s: content))) as TUser;
                return res;
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                SignOutAndRedirectToLogin();
            }

            return null;
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

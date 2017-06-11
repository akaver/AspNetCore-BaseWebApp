using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Interfaces;
using AspNetCore.Identity.Uow.Models;

namespace DAL.Rest.Repositories
{
    public class IdentityRoleRepository : RestRepository<IdentityRole>, IIdentityRoleRepository
    {
        public IdentityRoleRepository(IDataContext context, string endPoint) : base(context: context, endPoint: endPoint)
        {
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityRole> SingleByIdIncludeUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<IdentityRole>> AllIncludeUserAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityRole> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = new CancellationToken())
        {
            //return RepositoryDbSet.FirstOrDefaultAsync(predicate: r => r.NormalizedName == normalizedName, cancellationToken: cancellationToken);

            var uri = EndPoint + "/" + nameof(FindByNameAsync) + "/" + normalizedName;
           
            var response = await HttpClient.GetAsync(requestUri:  uri, cancellationToken: cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                _serializer = new DataContractJsonSerializer(type: typeof(IdentityRole));
                var content = await response.Content.ReadAsStringAsync();

                var res = _serializer.ReadObject(stream: new MemoryStream(buffer: Encoding.UTF8.GetBytes(s: content))) as IdentityRole;
                return res;
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                SignOutAndRedirectToLogin();
            }

            return null;
        }
    }
}

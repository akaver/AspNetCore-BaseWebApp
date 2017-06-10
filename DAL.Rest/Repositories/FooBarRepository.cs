using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.App;
using Domain;

namespace DAL.Rest.Repositories
{
    public class FooBarRepository : RestRepository<FooBar>, IFooBarRepository
    {
        public FooBarRepository(IDataContext context, string endPoint) : base(context: context, endPoint: endPoint)
        {
        }

        public Task<FooBar> SingleOrDefaultIncludeNavigation(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<FooBar>> AllForUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

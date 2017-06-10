using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.App;
using Domain;

namespace DAL.Rest.Repositories
{
    public class BlahRepository : RestRepository<Blah>, IBlahRepository
    {
        public BlahRepository(IDataContext context, string endPoint) : base(context, endPoint)
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
    }
}

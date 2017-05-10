using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.App;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityFrameworkCore.Repositories
{
    public class BlahRepository : EFRepository<Blah>, IBlahRepository
    {
        public BlahRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }

        public bool Exists(int id)
        {
            return RepositoryDbSet.Any(predicate: f => f.BlahId == id);
        }

        public Task<bool> ExistsAsync(int id)
        {
            return RepositoryDbSet.AnyAsync(predicate: f => f.BlahId == id);
        }
    }
}

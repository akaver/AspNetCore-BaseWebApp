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
    public class FooBarRepository : EFRepository<FooBar>, IFooBarRepository
    {
        public FooBarRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }
        public Task<List<FooBar>> AllForUserAsync(int userId)
        {
            return RepositoryDbSet
                .Include(navigationPropertyPath: a => a.BlahOne)
                .Include(navigationPropertyPath: b => b.BlahTwo)
                .Include(navigationPropertyPath: c => c.BlahThree)
                .Where(predicate: f => f.ApplicationUserId == userId).ToListAsync();
        }

        public bool Exists(int id)
        {
            return RepositoryDbSet.Any(predicate: f => f.FooBarId == id);
        }

        public Task<bool> ExistsAsync(int id)
        {
            return RepositoryDbSet.AnyAsync(predicate: f => f.FooBarId == id);
        }
    }
}

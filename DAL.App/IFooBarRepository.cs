using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;
using Domain;

namespace DAL.App
{
    public interface IFooBarRepository : IRepository<FooBar>
    {
        Task<FooBar> SingleOrDefaultIncludeNavigation(int id);

        Task<List<FooBar>> AllForUserAsync(int userId);
        bool Exists(int id);
        Task<bool> ExistsAsync(int id);

    }
}

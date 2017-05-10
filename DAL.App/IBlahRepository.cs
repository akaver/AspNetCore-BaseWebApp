using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;
using Domain;

namespace DAL.App
{
    public interface IBlahRepository : IRepository<Blah>
    {
        bool Exists(int id);
        Task<bool> ExistsAsync(int id);
    }
}

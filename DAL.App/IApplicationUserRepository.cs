using System;
using DAL.Repositories;
using Domain;

namespace DAL.App
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
    }
}

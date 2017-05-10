using System;
using System.Collections.Generic;
using System.Text;
using AspNetCore.Identity.Uow.Interfaces;
using DAL.Repositories;
using Domain;

namespace DAL.App
{
    public interface IApplicationUnitOfWork : IUnitOfWork, IIdentityUnitOfWork<ApplicationUser>
    {
        IFooBarRepository FooBars { get; }
        IApplicationUserRepository ApplicationUsers { get; }

    }
}

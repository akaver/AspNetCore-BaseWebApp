using System;
using System.Collections.Generic;
using System.Text;
using DAL.Repositories;
using Domain;

namespace DAL.App
{
    public interface IFooBarRepository : IRepository<FooBar>
    {
    }
}

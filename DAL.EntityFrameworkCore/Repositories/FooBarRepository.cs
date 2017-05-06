using System;
using System.Collections.Generic;
using System.Text;
using DAL.App;
using Domain;

namespace DAL.EntityFrameworkCore.Repositories
{
    public class FooBarRepository : EFRepository<FooBar>, IFooBarRepository
    {
        public FooBarRepository(IDataContext dataContext) : base(dataContext: dataContext)
        {
        }
    }
}

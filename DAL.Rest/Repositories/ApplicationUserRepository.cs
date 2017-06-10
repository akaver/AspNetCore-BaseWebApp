using System;
using System.Collections.Generic;
using System.Text;
using DAL.App;
using Domain;

namespace DAL.Rest.Repositories
{
    public class ApplicationUserRepository : RestRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(IDataContext context, string endPoint) : base(context, endPoint)
        {
        }
    }
}

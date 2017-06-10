using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using AspNetCore.Identity.Uow.Interfaces;
using AspNetCore.Identity.Uow.Models;
using DAL.App;
using DAL.Helpers;
using DAL.Rest.Repositories;
using Domain;

namespace DAL.Rest.Helpers
{
    public class RestRepositoryFactory : IRepositoryFactory
    {
        private readonly IDictionary<Type, Func<IDataContext, object>> _repositoryFactories;

        public RestRepositoryFactory()
        {
            _repositoryFactories = GetCustomFactories();
        }

        //this ctor is for testing only, you can give here an arbitrary list of repos
        public RestRepositoryFactory(IDictionary<Type, Func<IDataContext, object>> factories)
        {
            _repositoryFactories = factories;
        }


        //special repos with custom interfaces are registered here
        private static IDictionary<Type, Func<IDataContext, object>> GetCustomFactories()
        {
            return new Dictionary<Type, Func<IDataContext, object>>
            {
                // app repos
                {typeof(IFooBarRepository), dataContext => new FooBarRepository(context: dataContext, endPoint: nameof(FooBar))},
                {typeof(IBlahRepository), dataContext => new BlahRepository(context: dataContext, endPoint: nameof(Blah))},
                {typeof(IApplicationUserRepository), dataContext => new ApplicationUserRepository(context: dataContext, endPoint: nameof(ApplicationUser))},
                
                // Identity repos
                { typeof(IIdentityRoleClaimRepository), dataContext => new IdentityRoleClaimRepository(context: dataContext, endPoint: nameof(IdentityRoleClaim))},
                {typeof(IIdentityRoleRepository), dataContext => new IdentityRoleRepository(context: dataContext, endPoint: nameof(IdentityRole))},
                {typeof(IIdentityUserClaimRepository), dataContext => new IdentityUserClaimRepository(context: dataContext, endPoint: nameof(IdentityUserClaim))},
                {typeof(IIdentityUserLoginRepository), dataContext => new IdentityUserLoginRepository(context: dataContext, endPoint: nameof(IdentityUserLogin))},
                {typeof(IIdentityUserRepository<ApplicationUser>), dataContext => new IdentityUserRepository<ApplicationUser>(context: dataContext, endPoint: nameof(IdentityUser))},
                {typeof(IIdentityUserRoleRepository), dataContext => new IdentityUserRoleRepository(context: dataContext, endPoint: nameof(IdentityUserRole))},
                {typeof(IIdentityUserTokenRepository), dataContext => new IdentityUserTokenRepository(context: dataContext, endPoint: nameof(IdentityUserToken))},
            };
        }

        public Func<IDataContext, object> GetRepositoryFactoryForType<T>() where T : class
        {
            return GetCustomRepositoryFactory<T>() ?? GetStandardRepositoryFactory<T>();
        }

        public Func<IDataContext, object> GetCustomRepositoryFactory<T>() where T : class
        {

            Func<IDataContext, object> factory;

            _repositoryFactories.TryGetValue(key: typeof(T), value: out factory);

            return factory;
        }



        // return factory (function) for creation of standard repositories
        public Func<IDataContext, object> GetStandardRepositoryFactory<TEntity>() where TEntity : class
        {
            // create new instance of EFRepository<TEntity>
            return dataContext => new RestRepository<TEntity>(context: dataContext, endPoint: nameof(TEntity));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using AspNetCore.Identity.Uow.Interfaces;
using AspNetCore.Identity.Uow.Models;
using DAL.App;
using DAL.EntityFrameworkCore.Repositories;
using DAL.Helpers;
using Domain;


namespace DAL.EntityFrameworkCore.Helpers
{
    public class EFRepositoryFactory : IRepositoryFactory
    {
        private readonly IDictionary<Type, Func<IDataContext, object>> _repositoryFactories;

        public EFRepositoryFactory()
        {
            _repositoryFactories = GetCustomFactories();
        }

        //this ctor is for testing only, you can give here an arbitrary list of repos
        public EFRepositoryFactory(IDictionary<Type, Func<IDataContext, object>> factories)
        {
            _repositoryFactories = factories;
        }


        //special repos with custom interfaces are registered here
        private static IDictionary<Type, Func<IDataContext, object>> GetCustomFactories()
        {
            return new Dictionary<Type, Func<IDataContext, object>>
            {
                // app repos
                {typeof(IFooBarRepository), dbContext => new FooBarRepository(dataContext: dbContext)},
                {typeof(IBlahRepository), dbContext => new BlahRepository(dataContext: dbContext)},
                {typeof(IApplicationUserRepository), dbContext => new ApplicationUserRepository(dataContext: dbContext)},
                
                // Identity repos
                { typeof(IIdentityRoleClaimRepository), dbContext => new IdentityRoleClaimRepository(dataContext: dbContext)},
                {typeof(IIdentityRoleRepository), dbContext => new IdentityRoleRepository(dataContext: dbContext)},
                {typeof(IIdentityUserClaimRepository), dbContext => new IdentityUserClaimRepository(dataContext: dbContext)},
                {typeof(IIdentityUserLoginRepository), dbContext => new IdentityUserLoginRepository(dataContext: dbContext)},
                {typeof(IIdentityUserRepository<ApplicationUser>), dbContext => new IdentityUserRepository<ApplicationUser>(dataContext: dbContext)},
                {typeof(IIdentityUserRoleRepository), dbContext => new IdentityUserRoleRepository(dataContext: dbContext)},
                {typeof(IIdentityUserTokenRepository), dbContext => new IdentityUserTokenRepository(dataContext: dbContext)},
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
            return dataContext => new EFRepository<TEntity>(dataContext: dataContext);
        }
    }
}

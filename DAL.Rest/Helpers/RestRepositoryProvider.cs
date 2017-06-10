using System;
using System.Collections.Generic;
using System.Text;
using DAL.Helpers;
using DAL.Repositories;

namespace DAL.Rest.Helpers
{
    public class RestRepositoryProvider<TContext> : IRepositoryProvider
        where TContext : class, IDataContext
    {
        private readonly IDataContext _context;
        private readonly IRepositoryFactory _repositoryFactory;

        // declare new type. same as Func<IDataContext, object>
        // public delegate object RepositoryFactory(IDataContext context);

        // cache for repositories
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        // this context is actually httpClient
        public RestRepositoryProvider(TContext context, IRepositoryFactory repositoryFactory)
        {
            _context = context ?? throw new NullReferenceException(message: nameof(context));
            _repositoryFactory = repositoryFactory ?? throw new NullReferenceException(message: nameof(repositoryFactory));
        }

        // return standard repository
        public IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class
        {
            return GetOrMakeRepository<IRepository<TEntity>>(factory: _repositoryFactory.GetStandardRepositoryFactory<TEntity>());
        }

        // interface based custom repos
        public TRepository GetCustomRepository<TRepository>() where TRepository : class
        {
            return GetOrMakeRepository<TRepository>(factory: _repositoryFactory.GetCustomRepositoryFactory<TRepository>());
        }


        private TRepository GetOrMakeRepository<TRepository>(Func<IDataContext, object> factory = null) where TRepository : class
        {
            // Look for T dictionary cache under typeof(T).
            object repoObj;
            _repositories.TryGetValue(key: typeof(TRepository), value: out repoObj);
            if (repoObj != null)
            {
                return (TRepository)repoObj;
            }

            // repsoitory was not found in cache. try to create it.

            return MakeRepository<TRepository>(factory: factory);

        }

        private TRepository MakeRepository<TRepository>(Func<IDataContext, object> factory) where TRepository : class
        {
            var repositoryFactory = factory ?? _repositoryFactory.GetCustomRepositoryFactory<TRepository>();

            if (repositoryFactory == null)
            {
                throw new NotImplementedException(message: $"No factory for repository type {typeof(TRepository).FullName}");
            }
            var repo = (TRepository)repositoryFactory(arg: _context);
            _repositories[key: typeof(TRepository)] = repo;
            return repo;
        }


    }
}

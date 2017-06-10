using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Interfaces;
using DAL.App;
using DAL.Helpers;
using DAL.Repositories;
using Domain;

namespace DAL.Rest
{
    public class ApplicationUnitOfWorkRest<TContext> : IApplicationUnitOfWork
        where TContext : IDataContext
    {
        #region Repositories
        public IFooBarRepository FooBars => GetCustomRepository<IFooBarRepository>();
        public IBlahRepository Blahs => GetCustomRepository<IBlahRepository>();
        public IApplicationUserRepository ApplicationUsers => GetCustomRepository<IApplicationUserRepository>();


        public IIdentityRoleClaimRepository IdentityRoleClaims => GetCustomRepository<IIdentityRoleClaimRepository>();
        public IIdentityRoleRepository IdentityRoles => GetCustomRepository<IIdentityRoleRepository>();
        public IIdentityUserClaimRepository IdentityUserClaims => GetCustomRepository<IIdentityUserClaimRepository>();
        public IIdentityUserLoginRepository IdentityUserLogins => GetCustomRepository<IIdentityUserLoginRepository>();
        public IIdentityUserRepository<ApplicationUser> IdentityUsers => GetCustomRepository<IIdentityUserRepository<ApplicationUser>>();
        public IIdentityUserRoleRepository IdentityUserRoles => GetCustomRepository<IIdentityUserRoleRepository>();
        public IIdentityUserTokenRepository IdentityUserTokens => GetCustomRepository<IIdentityUserTokenRepository>();
        #endregion


        // httpclient, common for all repos
        // make it static, do not dispose. much better resource usage
        // https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/

        private readonly HttpClient _httpClient;
        private readonly IRepositoryProvider _repositoryProvider;

        public ApplicationUnitOfWorkRest(TContext context, IRepositoryProvider repositoryProvider)
        {
            _httpClient = (context as HttpClient) ?? throw new 
                ArgumentNullException(
                paramName: nameof(context), 
                message: $"{nameof(context)}(HttpClient) in {nameof(ApplicationUnitOfWorkRest<TContext>)} cannot be null, and it should be singleton!");
            _repositoryProvider = repositoryProvider ?? throw new NullReferenceException(message: nameof(repositoryProvider));
        }

        public IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class
        {
            CheckDisposed();
            return _repositoryProvider.GetEntityRepository<TEntity>();
        }

        public TRepository GetCustomRepository<TRepository>() where TRepository : class
        {
            CheckDisposed();
            return _repositoryProvider.GetCustomRepository<TRepository>();
        }


        #region SaveChanges
        public int SaveChanges()
        {
            // not used in rest based implementation
            return 0;
        }

        public Task<int> SaveChangesAsync()
        {
            // not used in rest based implementation
            return new Task<int>(function: () => 0);
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            // not used in rest based implementation
            return new Task<int>(function: () => 0, cancellationToken: cancellationToken);
        }
        #endregion




        #region IDisposable Implementation

        private bool _isDisposed;

        protected void CheckDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(objectName: "The UnitOfWork is already disposed and cannot be used anymore.");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // do not dispose httpclient, its static
                }
            }
            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(obj: this);
        }

        // destructor
        ~ApplicationUnitOfWorkRest()
        {
            Dispose(disposing: false);
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Interfaces;
using DAL.App;
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

        public ApplicationUnitOfWorkRest(HttpClient httpClient, string baseAddr)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(paramName: nameof(httpClient), message: "HttpClient in UOW cannot be null, and it should be singleton!");

            if (string.IsNullOrWhiteSpace(value: baseAddr))
            {
                throw new ArgumentNullException(paramName: nameof(baseAddr), message: "Please provide Rest server base address!");
            }

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(item: new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
            _httpClient.BaseAddress = new Uri(uriString: baseAddr);
        }

        public IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public TRepository GetCustomRepository<TRepository>() where TRepository : class
        {
            throw new NotImplementedException();
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

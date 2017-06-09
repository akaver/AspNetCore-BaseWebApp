using System;
using System.Collections.Generic;
using System.Net.Http;
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

        // httpclient, common for all repos
        private readonly HttpClient _httpClient = new HttpClient();



        public IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public TRepository GetCustomRepository<TRepository>() where TRepository : class
        {
            throw new NotImplementedException();
        }

        public IIdentityRoleClaimRepository IdentityRoleClaims { get; }
        public IIdentityRoleRepository IdentityRoles { get; }
        public IIdentityUserClaimRepository IdentityUserClaims { get; }
        public IIdentityUserLoginRepository IdentityUserLogins { get; }
        public IIdentityUserRepository<ApplicationUser> IdentityUsers { get; }
        public IIdentityUserRoleRepository IdentityUserRoles { get; }
        public IIdentityUserTokenRepository IdentityUserTokens { get; }
        public IFooBarRepository FooBars { get; }
        public IBlahRepository Blahs { get; }
        public IApplicationUserRepository ApplicationUsers { get; }

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
                    _httpClient?.Dispose();
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

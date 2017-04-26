using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Interfaces;
using AspNetCore.Identity.Uow.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Identity.Uow
{



    /// <summary>
    /// Creates a new instance of a persistence store for roles.
    /// </summary>
    /// <typeparam name="TRole">The type of the class representing a role</typeparam>
    //public class RoleStore<TRole> : RoleStore<int, TRole, IdentityUserRole, IdentityRoleClaim, IIdentityUnitOfWork, IIdentityRoleRepository, IIdentityRoleClaimRepository>
    //    where TRole : IdentityRole<int>
    //{

    //}

    /// <summary>
    /// Creates a new instance of a persistence store for roles.
    /// </summary>
    /// <typeparam name="TRole">The type of the class representing a role.</typeparam>
    /// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
    //public class RoleStore<TRole, TContext> : RoleStore<TRole, TContext, string>
    //    where TRole : IdentityRole<string>
    //    where TContext : DbContext
    //{
    //    /// <summary>
    //    /// Constructs a new instance of <see cref="RoleStore{TRole, TContext}"/>.
    //    /// </summary>
    //    /// <param name="context">The <see cref="DbContext"/>.</param>
    //    /// <param name="describer">The <see cref="IdentityErrorDescriber"/>.</param>
    //    public RoleStore(TContext context, IdentityErrorDescriber describer = null) : base(context: context, describer: describer) { }
    //}

    /// <summary>
    /// Creates a new instance of a persistence store for roles.
    /// </summary>
    /// <typeparam name="TRole">The type of the class representing a role.</typeparam>
    /// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for a role.</typeparam>
    //public class RoleStore<TRole, TContext, TKey> : RoleStore<TRole, TContext, TKey, IdentityUserRole<TKey>, IdentityRoleClaim<TKey>>,
    //    IQueryableRoleStore<TRole>,
    //    IRoleClaimStore<TRole>
    //    where TRole : IdentityRole<TKey>
    //    where TKey : IEquatable<TKey>
    //    where TContext : DbContext
    //{
    //    /// <summary>
    //    /// Constructs a new instance of <see cref="RoleStore{TRole, TContext, TKey}"/>.
    //    /// </summary>
    //    /// <param name="context">The <see cref="DbContext"/>.</param>
    //    /// <param name="describer">The <see cref="IdentityErrorDescriber"/>.</param>
    //    public RoleStore(TContext context, IdentityErrorDescriber describer = null) : base(context: context, describer: describer) { }
    //}


    /// <summary>
    /// Creates a new instance of a persistence store for roles.
    /// </summary>
    /// <typeparam name="TKey">The type of the primary key for a role.</typeparam>
    /// <typeparam name="TRole">The type of the class representing a role.</typeparam>
    /// <typeparam name="TUserRole">The type of the class representing a user role.</typeparam>
    /// <typeparam name="TRoleClaim">The type of the class representing a role claim.</typeparam>
    /// <typeparam name="TUnitOfWork">The type of Unit of Work used to access the store.</typeparam>
    /// <typeparam name="TRoleRepository">The type of repository used to access the role store inside the uow.</typeparam>
    /// <typeparam name="TRoleClaimRepository">The type of repository used to access the roleclaim store inside the uow.</typeparam>
    public class RoleStore<TKey, TRole, TUserRole, TRoleClaim, TUnitOfWork, TRoleRepository, TRoleClaimRepository> :
        IRoleClaimStore<TRole>
        where TKey : IEquatable<TKey>
        where TRole : IdentityRole<TKey>, new()
        where TUserRole : IdentityUserRole<TKey>, new()
        where TRoleClaim : IdentityRoleClaim<TKey>, new()
        where TUnitOfWork : IIdentityUnitOfWork
        where TRoleRepository : class, IIdentityRoleRepository<TKey,TRole>
        where TRoleClaimRepository : class, IIdentityRoleClaimRepository<TKey,TRoleClaim>
    {
        /// <summary>
        /// Constructs a new instance of <see cref="RoleStore{TKey, TRole, TUserRole, TRoleClaim, TUnitOfWork, TRoleRepository}"/>.
        /// </summary>
        /// <param name="uow">The <see cref="IIdentityUnitOfWork"/>.</param>
        /// <param name="describer">The <see cref="IdentityErrorDescriber"/>.</param>
        public RoleStore(TUnitOfWork uow, IdentityErrorDescriber describer = null)
        {

            Uow = uow;
            if (Uow == null)
            {
                throw new ArgumentNullException(paramName: nameof(uow));
            }
            ErrorDescriber = describer ?? new IdentityErrorDescriber();
        }

        private bool _disposed;


        /// <summary>
        /// Gets the database context for this store.
        /// </summary>
        public TUnitOfWork Uow { get; private set; }

        /// <summary>
        /// Gets or sets the <see cref="IdentityErrorDescriber"/> for any error that occurred with the current operation.
        /// </summary>
        public IdentityErrorDescriber ErrorDescriber { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if changes should be persisted after CreateAsync, UpdateAsync and DeleteAsync are called.
        /// </summary>
        /// <value>
        /// True if changes should be automatically persisted, otherwise false.
        /// </value>
        public bool AutoSaveChanges { get; set; } = true;

        /// <summary>Saves the current store.</summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
        private async Task SaveChanges(CancellationToken cancellationToken)
        {
            if (AutoSaveChanges)
            {
                await Uow.SaveChangesAsync(cancellationToken: cancellationToken);
            }
        }

        /// <summary>
        /// Creates a new role in a store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role to create in the store.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the <see cref="IdentityResult"/> of the asynchronous query.</returns>
        public virtual async Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(paramName: nameof(role));
            }
            Uow.GetCustomRepository<TRoleRepository>().Add(entity: role);
            await SaveChanges(cancellationToken: cancellationToken);
            return IdentityResult.Success;
        }

        /// <summary>
        /// Updates a role in a store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role to update in the store.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the <see cref="IdentityResult"/> of the asynchronous query.</returns>
        public virtual async Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(paramName: nameof(role));
            }

            //Context.Attach(role);
            role.ConcurrencyStamp = Guid.NewGuid().ToString();
            Uow.GetCustomRepository<TRoleRepository>().Update(entity: role);

            try
            {
                await SaveChanges(cancellationToken: cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return IdentityResult.Failed(ErrorDescriber.ConcurrencyFailure());
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError(){Code = ex.GetType().Name, Description = ex.Message});
            }
            return IdentityResult.Success;
        }

        /// <summary>
        /// Deletes a role from the store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role to delete from the store.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the <see cref="IdentityResult"/> of the asynchronous query.</returns>
        public virtual async Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(paramName: nameof(role));
            }
            Uow.GetCustomRepository<TRoleRepository>().Remove(entity: role);
            try
            {
                await SaveChanges(cancellationToken: cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return IdentityResult.Failed(ErrorDescriber.ConcurrencyFailure());
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError() { Code = ex.GetType().Name, Description = ex.Message });
            }
            return IdentityResult.Success;
        }

        /// <summary>
        /// Gets the ID for a role from the store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose ID should be returned.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> that contains the ID of the role.</returns>
        public virtual Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(paramName: nameof(role));
            }
            return Task.FromResult(result: ConvertIdToString(id: role.IdentityRoleId));
        }

        /// <summary>
        /// Gets the name of a role from the store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose name should be returned.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> that contains the name of the role.</returns>
        public virtual Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(paramName: nameof(role));
            }
            return Task.FromResult(result: role.Name);
        }

        /// <summary>
        /// Sets the name of a role in the store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose name should be set.</param>
        /// <param name="roleName">The name of the role.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
        public virtual Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(paramName: nameof(role));
            }
            role.Name = roleName;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Converts the provided <paramref name="id"/> to a strongly typed key object.
        /// </summary>
        /// <param name="id">The id to convert.</param>
        /// <returns>An instance of <typeparamref name="TKey"/> representing the provided <paramref name="id"/>.</returns>
        public virtual TKey ConvertIdFromString(string id)
        {
            if (id == null)
            {
                return default(TKey);
            }
            return (TKey)TypeDescriptor.GetConverter(type: typeof(TKey)).ConvertFromInvariantString(text: id);
        }

        /// <summary>
        /// Converts the provided <paramref name="id"/> to its string representation.
        /// </summary>
        /// <param name="id">The id to convert.</param>
        /// <returns>An <see cref="string"/> representation of the provided <paramref name="id"/>.</returns>
        public virtual string ConvertIdToString(TKey id)
        {
            if (id.Equals(other: default(TKey)))
            {
                return null;
            }
            return id.ToString();
        }

        /// <summary>
        /// Finds the role who has the specified ID as an asynchronous operation.
        /// </summary>
        /// <param name="id">The role ID to look for.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> that result of the look up.</returns>
        public virtual Task<TRole> FindByIdAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var roleId = ConvertIdFromString(id: id);
            return Uow.GetCustomRepository<TRoleRepository>().FindAsync(roleId, cancellationToken);
            
            //return Roles.FirstOrDefaultAsync(predicate: u => u.IdentityRoleId.Equals(roleId), cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Finds the role who has the specified normalized name as an asynchronous operation.
        /// </summary>
        /// <param name="normalizedName">The normalized role name to look for.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> that result of the look up.</returns>
        public virtual Task<TRole> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Uow.GetCustomRepository<TRoleRepository>().FindByNameAsync(normalizedName: normalizedName, cancellationToken: cancellationToken);

            // return Roles.FirstOrDefaultAsync(predicate: r => r.NormalizedName == normalizedName, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Get a role's normalized name as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose normalized name should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> that contains the name of the role.</returns>
        public virtual Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(paramName: nameof(role));
            }
            return Task.FromResult(result: role.NormalizedName);
        }

        /// <summary>
        /// Set a role's normalized name as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose normalized name should be set.</param>
        /// <param name="normalizedName">The normalized name to set</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
        public virtual Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(paramName: nameof(role));
            }
            role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Throws if this class has been disposed.
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(objectName: GetType().Name);
            }
        }

        /// <summary>
        /// Dispose the stores
        /// </summary>
        public void Dispose()
        {
            _disposed = true;
        }

        /// <summary>
        /// Get the claims associated with the specified <paramref name="role"/> as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose claims should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task{TResult}"/> that contains the claims granted to a role.</returns>
        public virtual async Task<IList<Claim>> GetClaimsAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(paramName: nameof(role));
            }

            return await Uow.GetCustomRepository<TRoleClaimRepository>().GetClaimsAsync(roleId: role.IdentityRoleId, cancellationToken: cancellationToken);

            //return await RoleClaims.Where(predicate: rc => rc.RoleId.Equals(other: role.Id)).Select(selector: c => new Claim(type: c.ClaimType, value: c.ClaimValue)).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Adds the <paramref name="claim"/> given to the specified <paramref name="role"/>.
        /// </summary>
        /// <param name="role">The role to add the claim to.</param>
        /// <param name="claim">The claim to add to the role.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
        public virtual Task AddClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(paramName: nameof(role));
            }
            if (claim == null)
            {
                throw new ArgumentNullException(paramName: nameof(claim));
            }

            Uow.GetCustomRepository<TRoleClaimRepository>().Add(entity: CreateRoleClaim(role: role, claim: claim));
            return Task.FromResult(result: false);
        }

        /// <summary>
        /// Removes the <paramref name="claim"/> given from the specified <paramref name="role"/>.
        /// </summary>
        /// <param name="role">The role to remove the claim from.</param>
        /// <param name="claim">The claim to remove from the role.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
        public virtual async Task RemoveClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(paramName: nameof(role));
            }
            if (claim == null)
            {
                throw new ArgumentNullException(paramName: nameof(claim));
            }

            //var claims = await RoleClaims.Where(predicate: rc => rc.RoleId.Equals(other: role.Id) && rc.ClaimValue == claim.Value && rc.ClaimType == claim.Type).ToListAsync(cancellationToken);

            await Uow.GetCustomRepository<TRoleClaimRepository>()
                .RemoveClaimAsync(roleId: role.IdentityRoleId, claim: claim, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Creates a entity representing a role claim.
        /// </summary>
        /// <param name="role">The associated role.</param>
        /// <param name="claim">The associated claim.</param>
        /// <returns>The role claim entity.</returns>
        protected virtual TRoleClaim CreateRoleClaim(TRole role, Claim claim)
        {
            return new TRoleClaim { RoleId = role.IdentityRoleId, ClaimType = claim.Type, ClaimValue = claim.Value };
        }
    }
}

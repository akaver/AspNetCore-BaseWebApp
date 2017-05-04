using System;
using System.Collections.Generic;
using System.Text;
using AspNetCore.Identity.Uow.Models;
using DAL;

namespace AspNetCore.Identity.Uow.Interfaces
{
    /// <summary>
    /// Identity UOW specification
    /// </summary>
    public interface IIdentityUnitOfWork<TUser> : IUnitOfWork 
        where TUser: IdentityUser
    {
        IIdentityRoleClaimRepository IdentityRoleClaims { get; }
        IIdentityRoleRepository IdentityRoles { get; }
        IIdentityUserClaimRepository IdentityUserClaims { get; }
        IIdentityUserLoginRepository IdentityUserLogins { get; }
        IIdentityUserRepository<TUser> IdentityUsers { get; }
        IIdentityUserRoleRepository IdentityUserRoles { get; }
        IIdentityUserTokenRepository IdentityUserTokens { get; }

    }
}

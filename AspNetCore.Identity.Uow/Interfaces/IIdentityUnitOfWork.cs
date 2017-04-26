using System;
using System.Collections.Generic;
using System.Text;
using DAL;

namespace AspNetCore.Identity.Uow.Interfaces
{
    /// <summary>
    /// Identity UOW specification
    /// </summary>
    public interface IIdentityUnitOfWork : IUnitOfWork 
    {
        IIdentityRoleClaimRepository IdentityRoleClaims { get; }
        IIdentityRoleRepository IdentityRoles { get; }
        IIdentityUserClaimRepository IdentityUserClaims { get; }
        IIdentityUserLoginRepository IdentityUserLogins { get; }
        IIdentityUserRepository IdentityUsers { get; }
        IIdentityUserRoleRepository IdentityUserRoles { get; }
        IIdentityUserTokenRepository IdentityUserTokens { get; }

    }
}

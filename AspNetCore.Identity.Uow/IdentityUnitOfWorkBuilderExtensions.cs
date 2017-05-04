using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using AspNetCore.Identity.Uow.Interfaces;
using AspNetCore.Identity.Uow.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AspNetCore.Identity.Uow
{
    /// <summary>
    /// Contains extension methods to <see cref="IdentityBuilder"/> for adding Unit Of Work based stores.
    /// </summary>
    public static class IdentityUnitOfWorkBuilderExtensions
    {
        /// <summary>
        /// Adds an Entity Framework implementation of identity information stores.
        /// </summary>
        /// <typeparam name="TContext">The Entity Framework database context to use.</typeparam>
        /// <param name="builder">The <see cref="IdentityBuilder"/> instance this method extends.</param>
        /// <returns>The <see cref="IdentityBuilder"/> instance this method extends.</returns>
        //  TODO: specify TKey for repo types
        public static IdentityBuilder AddUnitOfWork<TUserRepository, TRoleRepository, TUserRoleRepository, TUserLoginRepository, TUserClaimRepository, TUserTokenRepository, TRoleClaimRepository>(this IdentityBuilder builder)
            where TUserRepository : class //IIdentityUserRepository
            where TRoleRepository : class //IIdentityRoleRepository
            where TUserRoleRepository : class //IIdentityUserRoleRepository
            where TUserLoginRepository : class //IIdentityUserLoginRepository
            where TUserClaimRepository : class //IIdentityUserClaimRepository
            where TUserTokenRepository : class //IIdentityUserTokenRepository
            where TRoleClaimRepository : class //IIdentityRoleClaimRepository
        {
            AddStores(services: builder.Services, userType: builder.UserType, roleType: builder.RoleType,
                userRepositoryType: typeof(TUserRepository),
                roleRepositoryType: typeof(TRoleRepository),
                userRoleRepositoryType: typeof(TUserRoleRepository),
                userLoginRepositoryType: typeof(TUserLoginRepository),
                userClaimRepositoryType: typeof(TUserClaimRepository), 
                userTokenRepositoryType: typeof(TUserTokenRepository), 
                roleClaimRepositoryType: typeof(TRoleClaimRepository));
            return builder;
        }

        private static void AddStores(IServiceCollection services, Type userType, Type roleType,
            Type userRepositoryType, Type roleRepositoryType, Type userRoleRepositoryType, Type userLoginRepositoryType, Type userClaimRepositoryType, Type userTokenRepositoryType, Type roleClaimRepositoryType)
        {
            // public class IdentityUser<TKey, TUserClaim, TUserRole, TUserLogin, TUserToken>
            //var identityUserType = FindGenericBaseType(currentType: userType, genericBaseType: typeof(IdentityUser));
            //if (identityUserType == null)
            //{
            //    throw new InvalidOperationException(message: "NotIdentityUser");
            //}

            // public class IdentityRole<TKey, TUserRole, TRoleClaim>
            //var identityRoleType = FindGenericBaseType(currentType: roleType, genericBaseType: typeof(IdentityRole));
            //if (identityRoleType == null)
            //{
            //    throw new InvalidOperationException(message: "NotIdentityRole");
            //}



            // public class RoleStore<TKey, TRole, TUserRole, TRoleClaim, TUnitOfWork, TRoleRepository, TRoleClaimRepository>
         services.TryAddScoped(
                service: typeof(IRoleStore<>).MakeGenericType(roleType),
                implementationType: typeof(RoleStore<>).MakeGenericType(userType));



            // public class UserStore<TKey, TUser, TRole, TUserClaim, TUserRole, TUserLogin, TUserToken, TRoleClaim, TUnitOfWork, TUserRepository, TRoleRepository, TUserRoleRepository, TUserLoginRepository, TUserClaimRepository, TUserTokenRepository>
            services.TryAddScoped(
                service: typeof(IUserStore<>).MakeGenericType(userType),
                implementationType: typeof(UserStore<>).MakeGenericType(userType));
        }

        private static TypeInfo FindGenericBaseType(Type currentType, Type genericBaseType)
        {
            var type = currentType.GetTypeInfo();
            while (type.BaseType != null)
            {
                type = type.BaseType.GetTypeInfo();
                var genericType = type.IsGenericType ? type.GetGenericTypeDefinition() : null;
                if (genericType != null && genericType == genericBaseType)
                {
                    return type;
                }
            }
            return null;
        }

    }
}

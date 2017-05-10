using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Claims;
using System.Text;

namespace AspNetCore.Identity.Extensions
{
    public static class ClaimsPrincipalExtensions
    {

        /// <summary>
        /// Returns user id
        /// </summary>
        /// <param name="principal">User</param>
        /// <returns>user id - string</returns>
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(paramName: nameof(principal));

            return principal.FindFirst(type: ClaimTypes.NameIdentifier)?.Value;
        }

        /// <summary>
        /// Returns user id
        /// </summary>
        /// <param name="principal">User</param>
        /// <returns>user id, coverts from string to T (usually int)</returns>
        public static T GetUserId<T>(this ClaimsPrincipal principal)
            where T : struct 
        {

            var userId = GetUserId(principal: principal);

            if (string.IsNullOrWhiteSpace(value: userId))
            {
                throw new NullReferenceException(message: "ClaimPrincipal (user) id returned null!");
            }

            return ConvertToTypeFromString<T>(id: userId);
        }

        private static T ConvertToTypeFromString<T>(string id)
            where T : struct
        {
            if (id == null)
            {
                return default(T);
            }
            return (T)TypeDescriptor.GetConverter(type: typeof(T)).ConvertFromInvariantString(text: id);
        }
    }
}

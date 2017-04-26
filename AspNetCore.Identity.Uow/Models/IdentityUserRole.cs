using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AspNetCore.Identity.Uow.Models
{
    /// <summary>
    /// Represents the link between a user and a role.
    /// </summary>
    public class IdentityUserRole : IdentityUserRole<int>
    {
    }

    public class IdentityUserRole<TKey> : IdentityUserRole<TKey, IdentityUser<TKey>, IdentityRole<TKey>>
        where TKey: IEquatable<TKey>
    {
    }


    /// <summary>
    /// Represents the link between a user and a role.
    /// </summary>
    /// <typeparam name="TKey">The type of the primary key used for users and roles.</typeparam>
    public class IdentityUserRole<TKey, TUser, TRole> 
        where TKey : IEquatable<TKey>
    {
        [Key]
        public virtual TKey IdentityUserRoleId { get; set; }

        /// <summary>
        /// Gets or sets the primary key of the user that is linked to a role.
        /// </summary>
        public virtual TKey UserId { get; set; }
        public virtual TUser User { get; set; }

        /// <summary>
        /// Gets or sets the primary key of the role that is linked to the user.
        /// </summary>
        public virtual TKey RoleId { get; set; }
        public virtual TRole Role { get; set; }
    }
}

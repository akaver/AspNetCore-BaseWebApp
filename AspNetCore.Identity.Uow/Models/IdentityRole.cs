using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AspNetCore.Identity.Uow.Models
{
    /// <summary>
    /// Represents a role in the identity system
    /// </summary>
    public class IdentityRole
    {
        public IdentityRole() { }

        public IdentityRole(string roleName) : this()
        {
            Name = roleName;
        }


        [Key]
        public virtual int IdentityRoleId { get; set; }

        /// <summary>
        /// Gets or sets the name for this role.
        /// </summary>
        [MaxLength(length: 255)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the normalized name for this role.
        /// </summary>
        [MaxLength(length: 255)]
        public virtual string NormalizedName { get; set; }

        /// <summary>
        /// A random value that should change whenever a role is persisted to the store
        /// </summary>
        public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Navigation property for the users in this role.
        /// </summary>
        public virtual ICollection<IdentityUserRole> Users { get; } = new List<IdentityUserRole>();

        /// <summary>
        /// Navigation property for claims in this role.
        /// </summary>
        public virtual ICollection<IdentityRoleClaim> Claims { get; } = new List<IdentityRoleClaim>();

        /// <summary>
        /// Returns the name of the role.
        /// </summary>
        /// <returns>The name of the role.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}

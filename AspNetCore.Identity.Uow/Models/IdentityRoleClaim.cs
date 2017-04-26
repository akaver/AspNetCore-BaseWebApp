﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace AspNetCore.Identity.Uow.Models
{

    /// <summary>
    /// Represents a claim that is granted to all users within a role.
    /// </summary>
    public class IdentityRoleClaim : IdentityRoleClaim<int>
    {
    }

    public class IdentityRoleClaim<TKey> : IdentityRoleClaim<TKey, IdentityRole<TKey>>
        where TKey : IEquatable<TKey>
    {
    }

    /// <summary>
    /// Represents a claim that is granted to all users within a role.
    /// </summary>
    /// <typeparam name="TKey">The type of the primary key of the role associated with this claim.</typeparam>
    public class IdentityRoleClaim<TKey, TRole> 
        where TKey : IEquatable<TKey>
        where TRole : class, new()
    {
        /// <summary>
        /// Gets or sets the identifier for this role claim.
        /// </summary>
        [Key]
        public virtual TKey IdentityRoleClaimId { get; set; }

        /// <summary>
        /// Gets or sets the of the primary key of the role associated with this claim.
        /// </summary>
        public virtual TKey RoleId { get; set; }

        public virtual TRole Role { get; set; }

        /// <summary>
        /// Gets or sets the claim type for this claim.
        /// </summary>
        public virtual string ClaimType { get; set; }

        /// <summary>
        /// Gets or sets the claim value for this claim.
        /// </summary>
        public virtual string ClaimValue { get; set; }

        /// <summary>
        /// Constructs a new claim with the type and value.
        /// </summary>
        /// <returns></returns>
        public virtual Claim ToClaim()
        {
            return new Claim(type: ClaimType, value: ClaimValue);
        }

        /// <summary>
        /// Initializes by copying ClaimType and ClaimValue from the other claim.
        /// </summary>
        /// <param name="claim">The claim to initialize from.</param>
        public virtual void InitializeFromClaim(Claim claim)
        {
            ClaimType = claim?.Type;
            ClaimValue = claim?.Value;
        }
    }

}

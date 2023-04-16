// ***********************************************************************
// Assembly         : ENSEK,Entities
// Author           : pdurr
// Created          : 04-13-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-15-2023
// ***********************************************************************
// <copyright file="Account.cs" company="ENSEK,Entities">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace ENSEK.Entities.Models
{


    /// <summary>
    /// Class Account.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Account" /> class.
        /// </summary>
        public Account()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Account" /> class.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        public Account(long accountId, string? firstName, string? lastName)
        {
            AccountId = accountId;
            FirstName = firstName;
            LastName = lastName;
        }
        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>The account identifier.</value>
        public long AccountId { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string? FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string? LastName { get; set; }

    }
}
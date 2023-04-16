// ***********************************************************************
// Assembly         : ENSEK.Database
// Author           : pdurr
// Created          : 04-14-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-15-2023
// ***********************************************************************
// <copyright file="ApiContext.cs" company="ENSEK.Database">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENSEK.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ENSEK.Database
{
    /// <summary>
    /// Class ApiContext.
    /// Implements the <see cref="DbContext" />
    /// </summary>
    /// <seealso cref="DbContext" />
    public class ApiContext : DbContext
    {
        /// <summary>
        /// Override this method to configure the database (and other options) to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// The base implementation does nothing.
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
        /// typically define extension methods on this object that allow you to configure the context.</param>
        /// <remarks><para>
        /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
        /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        /// the options have already been set, and skip some or all of the logic in
        /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        /// </para>
        /// <para>
        /// See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
        /// for more information and examples.
        /// </para></remarks>
        protected override void OnConfiguring
            (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ENSEK_Readings");
        }
        /// <summary>
        /// Gets or sets the readings.
        /// </summary>
        /// <value>The readings.</value>
        public DbSet<MeterReading> Readings { get; set; }

    }
}

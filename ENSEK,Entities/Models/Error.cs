﻿// ***********************************************************************
// Assembly         : ENSEK,Entities
// Author           : pdurr
// Created          : 04-16-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-16-2023
// ***********************************************************************
// <copyright file="Error.cs" company="ENSEK,Entities">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ENSEK.Entities.Models
{
    /// <summary>
    /// Class Error.
    /// </summary>
    public class Error
    {
        private long _rowNo;
        private string? _reason;

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        public Error() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="rowNo">The row no.</param>
        /// <param name="reason">The reason.</param>
        public Error(long rowNo, string reason)
        {
            RowNo = rowNo;
            Reason = reason;
        }

        /// <summary>
        /// Gets or sets the row no.
        /// </summary>
        /// <value>The row no.</value>
        public Int64 RowNo
        {
            get => _rowNo;
            set => _rowNo = value;
        }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>The reason.</value>
        public string Reason
        {
            get => _reason ?? string.Empty;

            set => _reason = value;
        }
    }
}

// ***********************************************************************
// Assembly         : ENSEKWeb
// Author           : pdurr
// Created          : 04-21-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-21-2023
// ***********************************************************************
// <copyright file="MeterReadingsViewModel.cs" company="ENSEKWeb">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using ENSEK.Entities.Models;

namespace ENSEKWeb.Models
{
    /// <summary>
    /// Class MeterReadingsViewModel.
    /// Implements the <see cref="MeterReading" />
    /// </summary>
    /// <seealso cref="MeterReading" />
    public class MeterReadingsViewModel:MeterReading

    {

        /// <summary>
        /// Gets or sets the meter readings.
        /// </summary>
        /// <value>The meter readings.</value>
        public List<MeterReading>? MeterReadings { get; set; }
    }
}

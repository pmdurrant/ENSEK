// ***********************************************************************
// Assembly         : ENSEK.Contracts
// Author           : pdurr
// Created          : 04-14-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-15-2023
// ***********************************************************************
// <copyright file="IMeterReadingRepository.cs" company="ENSEK.Contracts">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using ENSEK.Entities.Models;

namespace ENSEK.Contracts
{
    /// <summary>
    /// Interface IMeterReadingRepository
    /// </summary>
    public interface IMeterReadingRepository
    {
        /// <summary>
        /// Gets the meter readings.
        /// </summary>
        /// <returns>List&lt;MeterReading&gt;.</returns>
        List<MeterReading> GetMeterReadings();

        /// <summary>
        /// Updates the meter readings.
        /// </summary>
        /// <param name="meterReadings">The meter readings.</param>
        void UpdateMeterReadings(List<MeterReading> meterReadings);

    }
}
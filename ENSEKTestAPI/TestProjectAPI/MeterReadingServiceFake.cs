// ***********************************************************************
// Assembly         : TestProjectAPI
// Author           : pdurr
// Created          : 04-16-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-17-2023
// ***********************************************************************
// <copyright file="MeterReadingServiceFake.cs" company="TestProjectAPI">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using ENSEK.Contracts;
using ENSEK.Entities.Models;

namespace TestProjectAPI
{
    /// <summary>
    /// Class MeterReadingServiceFake.
    /// Implements the <see cref="IMeterReadingRepository" />
    /// </summary>
    /// <seealso cref="IMeterReadingRepository" />
    public class MeterReadingServiceFake : IMeterReadingRepository
    {
        /// <summary>
        /// The meter readings
        /// </summary>
        private readonly List<MeterReading> _meterReadings;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeterReadingServiceFake"/> class.
        /// </summary>
        public MeterReadingServiceFake()
        {
            _meterReadings = new List<MeterReading>()
            {
            new MeterReading( "2344",DateTime.Parse("2/04/2019 09:24"),  "1002"),
              new MeterReading(    "2233",DateTime.Parse("2/04/2019 12:25"),    "323"),
                  new MeterReading(  "8766",DateTime.Parse("2/04/2019 12:25"),    "3440"),
                      new MeterReading(  "2344",DateTime.Parse("2/04/2019 12:25"),    "1002")


            };
        }

        /// <summary>
        /// Gets the meter readings.
        /// </summary>
        /// <returns>IEnumerable&lt;MeterReading&gt;.</returns>
        public IEnumerable<MeterReading> GetMeterReadings()
        {
            return _meterReadings;
        }

        /// <summary>
        /// Updates the meter readings.
        /// </summary>
        /// <param name="newItems">The new items.</param>
        /// <returns>List&lt;MeterReading&gt;.</returns>
        public List<MeterReading> UpdateMeterReadings(List<MeterReading> newItems)
        {

            _meterReadings.AddRange(_meterReadings);
            return _meterReadings;
        }

        /// <summary>
        /// Gets the meter readings.
        /// </summary>
        /// <returns>List&lt;MeterReading&gt;.</returns>
        List<MeterReading> IMeterReadingRepository.GetMeterReadings()
        {
            return _meterReadings;

        }

        /// <summary>
        /// Updates the meter readings.
        /// </summary>
        /// <param name="meterReadings">The meter readings.</param>
        void IMeterReadingRepository.UpdateMeterReadings(List<MeterReading> meterReadings)
        {
            _meterReadings.AddRange(meterReadings);
        }
    }
}

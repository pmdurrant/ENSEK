// ***********************************************************************
// Assembly         : ENSEK,Entities
// Author           : pdurr
// Created          : 04-13-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-15-2023
// ***********************************************************************
// <copyright file="MeterReading.cs" company="ENSEK,Entities">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel.DataAnnotations;

namespace ENSEK.Entities.Models
{


    /// <summary>
    /// Class MeterReading.
    /// </summary>
    public class MeterReading
    {
        /// <summary>
        /// The identifier
        /// </summary>
        private Int32 _id;
        /// <summary>
        /// The account identifier
        /// </summary>
        private string? _accountId;
        /// <summary>
        /// The meter reading date time
        /// </summary>
        private DateTime _meterReadingDateTime;
        /// <summary>
        /// The meter read value
        /// </summary>
        private string? _meterReadValue;
        /// <summary>
        /// The row no
        /// </summary>
        private long _rowNo;

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key]
        public Int32 Id
        {
            get => _id;
            set => _id = value;
        }

        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>The account identifier.</value>
        public string? AccountId
        {
            get => _accountId;
            set => _accountId = value;
        }

        /// <summary>
        /// Gets or sets the meter reading date time.
        /// </summary>
        /// <value>The meter reading date time.</value>
        public DateTime MeterReadingDateTime
        {
            get => _meterReadingDateTime;
            set => _meterReadingDateTime = value;
        }

        /// <summary>
        /// Gets or sets the meter read value.
        /// </summary>
        /// <value>The meter read value.</value>
        public string? MeterReadValue
        {
            get => _meterReadValue;
            set => _meterReadValue = value;
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
        /// Initializes a new instance of the <see cref="MeterReading" /> class.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <param name="meterReadingDateTime">The meter reading date time.</param>
        /// <param name="meterReadValue">The meter read value.</param>
        public MeterReading(string? accountId, DateTime meterReadingDateTime, string? meterReadValue)
        {
            AccountId = accountId;

            MeterReadingDateTime = meterReadingDateTime;
            MeterReadValue = meterReadValue;

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeterReading" /> class.
        /// </summary>
        public MeterReading()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MeterReading" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public MeterReading(Int32 id)
        {

            Id = id;
        }
    }
}



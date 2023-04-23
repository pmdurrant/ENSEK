// ***********************************************************************
// Assembly         : ENSEKWeb
// Author           : pdurr
// Created          : 04-21-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-23-2023
// ***********************************************************************
// <copyright file="MeterReadingViewModel.cs" company="ENSEKWeb">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel.DataAnnotations;

namespace ENSEKWeb.Models
{
    /// <summary>
    /// Class MeterReadingViewModel.
    /// </summary>
    public class MeterReadingViewModel
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
        public MeterReadingViewModel()
        {
        }
       
    }
}

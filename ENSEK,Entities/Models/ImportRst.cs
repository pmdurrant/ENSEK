// ***********************************************************************
// Assembly         : ENSEK,Entities
// Author           : pdurr
// Created          : 04-16-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-16-2023
// ***********************************************************************
// <copyright file="ImportRst.cs" company="ENSEK,Entities">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENSEK.Entities.Models.Enum;

namespace ENSEK.Entities.Models
{
    /// <summary>
    /// Class ImportRst.
    /// </summary>
    public class ImportRst
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportRst"/> class.
        /// </summary>
        /// <param name="fileImportResponse">The file import response.</param>
        /// <param name="errors">The errors.</param>
        /// <param name="meterReadings">The meter readings.</param>
        public ImportRst(ImportResponse fileImportResponse, List<Error> errors ,List<MeterReading> meterReadings)
        {
            FileImportResponse = fileImportResponse;
            Errors = errors;
            MetersReadings= meterReadings;
        }

        /// <summary>
        /// Gets or sets the meters readings.
        /// </summary>
        /// <value>The meters readings.</value>
        public List<MeterReading> MetersReadings { get; set; }
        /// <summary>
        /// Gets or sets the file import response.
        /// </summary>
        /// <value>The file import response.</value>
        public ImportResponse FileImportResponse { get; set; }
        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public List<Error> Errors { get; set; }
    }
}

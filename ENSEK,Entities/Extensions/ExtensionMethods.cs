// ***********************************************************************
// Assembly         : ENSEK,Entities
// Author           : pdurr
// Created          : 04-13-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-15-2023
// ***********************************************************************
// <copyright file="ExtensionMethods.cs" company="ENSEK,Entities">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using ENSEK.Entities.Models;
using Microsoft.Extensions.FileSystemGlobbing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ENSEK.Entities.Extensions
{
    /// <summary>
    /// Class ExtensionMethods.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Converts to proper.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string ToProper(this string str)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            return textInfo.ToTitleCase(str);

        }


        /// <summary>
        /// Finds the date.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string findDate(this string str)
        {
            try
            {
                Regex regexObj = new Regex("[0-9]{2}/[0-9]{2}/[0-9]{4} [0-9]{2}[:][0-9]{2}[:][0-9]{2}");
                Match matchResults = regexObj.Match(str);
                while (matchResults.Success)
                {
                    // matched text: matchResults.Value
                    // match start: matchResults.Index
                    // match length: matchResults.Length
                    return matchResults.Value;
                    matchResults = matchResults.NextMatch();
                   
                }
            }
            catch (ArgumentException ex)
            {
                // Syntax error in the regular expression
            }

            return "Invalid Data";
        }




        /// <summary>
        /// Gets the meter reading.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="row">The row.</param>
        /// <returns>MeterReading.</returns>
        public static MeterReading GetMeterReading(DataTable table, DataRow row)
        {
            var meterReading = new MeterReading();
            for (var i = 0; i < table.Columns.Count; i++)
                switch (i)
                {
                    case 0:
                        meterReading.AccountId = row[i].ToString();
                        break;
                    case 1:

                        var readingTmp = row[i].ToString();
                        DateTime Tmp;
                        DateTime.TryParse(readingTmp.findDate(), out Tmp);
                        meterReading.MeterReadingDateTime = Tmp;
                        break;
                    case 2:
                        meterReading.MeterReadValue = row[i].ToString();
                        break;
                }

            return meterReading;
        }

    }
}
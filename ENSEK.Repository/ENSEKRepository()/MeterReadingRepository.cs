// ***********************************************************************
// Assembly         : ENSEKRepository()
// Author           : pdurr
// Created          : 04-14-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-15-2023
// ***********************************************************************
// <copyright file="MeterReadingRepository.cs" company="ENSEKRepository()">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using ENSEK.Contracts;
using ENSEK.Database;
using ENSEK.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ENSEK.Entities.DTO;


namespace ENSEK.Repository
{
    /// <summary>
    /// Class MeterReadingRepository.
    /// Implements the <see cref="IMeterReadingRepository" />
    /// </summary>
    /// <seealso cref="IMeterReadingRepository" />
    public class MeterReadingRepository : IMeterReadingRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeterReadingRepository" /> class.
        /// </summary>
        public MeterReadingRepository()
        {
            //using (var context = new ApiContext())
            //{
            //    var authors = new List<Author>
            //    {
            //        new Author
            //        {
            //            FirstName ="Joydip",
            //            LastName ="Kanjilal",
            //            Books = new List<Book>()
            //            {
            //                new Book { Title = "Mastering C# 8.0"},
            //                new Book { Title = "Entity Framework Tutorial"},
            //                new Book { Title = "ASP.NET 4.0 Programming"}
            //            }
            //        },
            //        new Author
            //        {
            //            FirstName ="Yashavanth",
            //            LastName ="Kanetkar",
            //            Books = new List<Book>()
            //            {
            //                new Book { Title = "Let us C"},
            //                new Book { Title = "Let us C++"},
            //                new Book { Title = "Let us C#"}
            //            }
            //        }
            //    };
            //    context.Authors.AddRange(authors);
            //    context.SaveChanges();
            //}
        }


        /// <summary>
        /// Updates the meter readings.
        /// </summary>
        /// <param name="meterReadings">The meter readings.</param>
        public void UpdateMeterReadings(List<MeterReading> meterReadings)
            {
                using (var context = new ApiContext())
                {
                    context.Readings.AddRange(meterReadings);
                    context.SaveChanges(true);
                }
            }


        /// <summary>
        /// Gets the meter readings.
        /// </summary>
        /// <returns>List&lt;MeterReading&gt;.</returns>
        public List<MeterReading> GetMeterReadings()
        {
                using (var context = new ApiContext())
                {
                 var list = context.Readings.ToList();
                    return list;
                }
            }
        }

    }

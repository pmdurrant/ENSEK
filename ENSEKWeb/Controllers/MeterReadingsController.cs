// ***********************************************************************
// Assembly         : ENSEKWeb
// Author           : pdurr
// Created          : 04-21-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-23-2023
// ***********************************************************************
// <copyright file="MeterReadingsController.cs" company="ENSEKWeb">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using ENSEK.Contracts;
using ENSEKWeb.Models;
using Microsoft.AspNetCore.Mvc;
using ENSEK.Entities.Models;
using Newtonsoft.Json;

namespace ENSEKWeb.Controllers
{
    /// <summary>
    /// Class MeterReadingsController.
    /// Implements the <see cref="Controller" />
    /// </summary>
    /// <seealso cref="Controller" />
    public class MeterReadingsController : Controller
    {
        /// <summary>
        /// The meter reading repository
        /// </summary>
        private readonly IMeterReadingRepository _meterReadingRepository;
        /// <summary>
        /// The standard HTTP client
        /// </summary>
        private readonly IStandardHttpClient _standardHttpClient;
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Initializes a new instance of the <see cref="MeterReadingsController"/> class.
        /// </summary>
        /// <param name="readingRepository">The reading repository.</param>
        /// <param name="standardHttpClient">The standard HTTP client.</param>
        /// <param name="config">The configuration.</param>
        public MeterReadingsController(IMeterReadingRepository readingRepository, IStandardHttpClient standardHttpClient, IConfiguration config)
        {
            _meterReadingRepository = readingRepository;
            _configuration = config;
            _standardHttpClient = standardHttpClient;
        }



        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ViewResult.</returns>
        [Route("MeterReadings/Details/{id?}")]
        public async Task<ViewResult> Details(int? id)
        {
#pragma warning disable CS1998
            var meterReadings = await Task.Run(async () => _meterReadingRepository.GetMeterReadings());
#pragma warning restore CS1998
            var findrst = (from n in meterReadings where n.Id == id select n).FirstOrDefault();
            var rtn = new MeterReadingViewModel()
            {
#pragma warning disable CS8602
                RowNo = findrst.RowNo,
#pragma warning restore CS8602

                MeterReadValue = findrst.MeterReadValue,
                MeterReadingDateTime = findrst.MeterReadingDateTime,
                AccountId = findrst.AccountId,
                Id = findrst.Id
            };

            return View("Details", rtn);
        }

        /// <summary>
        /// Meters the readings.
        /// </summary>
        /// <returns>ViewResult.</returns>
        public async Task<ViewResult> MeterReadings()
        {
            #region API Call

            var headers = new Dictionary<string, string>(){
                {"content - type", "application/json; charset=utf-8" }
            };

            string appBaseUrl = "https://ensek.officeblox.co.uk:5004" + "/api/MeterReadings";
            var apiRst = _standardHttpClient.GetAsync(appBaseUrl);

             List<MeterReadingsViewModel> rtn = new List<MeterReadingsViewModel>();


            var meterReadingsStored = _meterReadingRepository.GetMeterReadings();

            var apiContent = await apiRst.Result.Content.ReadAsStringAsync();
            #endregion
            var readings = JsonConvert.DeserializeObject<List<MeterReading>>(apiContent);
         
            //Store readings 
#pragma warning disable CS8604
            _meterReadingRepository.UpdateMeterReadings(readings);
#pragma warning restore CS8604

            //Add  records from API
            foreach (var meterReading in readings)
            {

                rtn.Add(new MeterReadingsViewModel()
                {
                    RowNo = meterReading.RowNo,

                    MeterReadValue = meterReading.MeterReadValue,
                    MeterReadingDateTime = meterReading.MeterReadingDateTime,
                    AccountId = meterReading.AccountId,
                    Id = meterReading.Id
                });

            }

            // add records from store
            foreach (var meterReading in meterReadingsStored)
            {

                var findrec=(from p in rtn
                            where  p.AccountId == meterReading.AccountId &&
                                       p.MeterReadingDateTime == meterReading.MeterReadingDateTime &&
                                       p.MeterReadValue == meterReading.MeterReadValue &&
                                       p.RowNo == meterReading.RowNo && p.Id == meterReading.Id
                            select p).Single();

                if (findrec ==null)
                {
                    rtn.Add(new MeterReadingsViewModel()
                    {
                        MeterReadValue = meterReading.MeterReadValue,
                        MeterReadingDateTime = meterReading.MeterReadingDateTime,
                        AccountId = meterReading.AccountId,
                        Id = meterReading.Id,
                        RowNo= meterReading.RowNo

                    });
                }
            }

            return View("MeterReadings", rtn);
        }
    }
}

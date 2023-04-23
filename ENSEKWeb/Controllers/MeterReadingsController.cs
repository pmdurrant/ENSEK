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
            var meterReadings = _meterReadingRepository.GetMeterReadings();
            var findrst = (from n in meterReadings where n.Id == id select n).FirstOrDefault();
            var rtn = new MeterReadingViewModel()
            {
                RowNo = findrst.RowNo,

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

            var Headers = new Dictionary<string, string>(){
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
            _meterReadingRepository.UpdateMeterReadings(readings);

           //Add  records from API
            foreach (var meterreading in readings)
            {

                rtn.Add(new MeterReadingsViewModel()
                {
                    RowNo = meterreading.RowNo,

                    MeterReadValue = meterreading.MeterReadValue,
                    MeterReadingDateTime = meterreading.MeterReadingDateTime,
                    AccountId = meterreading.AccountId,
                    Id = meterreading.Id
                });

            }

            // add records from store
            foreach (var meterreading in meterReadingsStored)
            {

                var findrec=(from p in rtn
                            where  p.AccountId == meterreading.AccountId &&
                                       p.MeterReadingDateTime == meterreading.MeterReadingDateTime &&
                                       p.MeterReadValue == meterreading.MeterReadValue &&
                                       p.RowNo == meterreading.RowNo && p.Id == meterreading.Id
                            select p).Single();

                if (findrec ==null)
                {
                    rtn.Add(new MeterReadingsViewModel()
                    {
                        MeterReadValue = meterreading.MeterReadValue,
                        MeterReadingDateTime = meterreading.MeterReadingDateTime,
                        AccountId = meterreading.AccountId,
                        Id = meterreading.Id,
                        RowNo= meterreading.RowNo

                    });
                }
            }

            return View("MeterReadings", rtn);
        }
    }
}

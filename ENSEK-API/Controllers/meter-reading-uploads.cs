// ***********************************************************************
// Assembly         : ENSEK-API
// Author           : pdurr
// Created          : 04-12-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-16-2023
// ***********************************************************************
// <copyright file="meter-reading-uploads.cs" company="ENSEK-API">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using AutoMapper;
using ENSEK.Contracts;
using ENSEK.Entities.DTO;
using ENSEK.Entities.Extensions;
using ENSEK.Entities.Models;
using ENSEK.Entities.Models.Enum;
using Microsoft.AspNetCore.Mvc;


namespace ENSEK_API.Controllers;

//readonly IAuthorRepository _authorRepository;
//public AuthorController(IAuthorRepository authorRepository)
//{
//    _authorRepository = authorRepository;
//}

/// <summary>
/// Class MeterReadingUploadsController.
/// Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class MeterReadingUploadsController : ControllerBase
{
    /// <summary>
    /// The configuration
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    /// The mapper
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// The meter reading repository
    /// </summary>
    private readonly IMeterReadingRepository _meterReadingRepository;


    /// <summary>
    /// Initializes a new instance of the <see cref="MeterReadingUploadsController" /> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="meterReadingRepository">The meter reading repository.</param>
    /// <param name="mapper">The mapper.</param>
    public MeterReadingUploadsController(IConfiguration configuration, IMeterReadingRepository meterReadingRepository,
        IMapper mapper)
    {
        _meterReadingRepository = meterReadingRepository;
        _configuration = configuration;
        _mapper = mapper;
    }


    /// <summary> 
    /// Uploads the specified file.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns>IActionResult.</returns>
    [HttpPost("single-file")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var metersReading = ExtensionMethods.LoadExcelFile(file, _configuration);


        if (metersReading.FileImportResponse == ImportResponse.Success)
        {
#pragma warning disable CS1998
            await Task.Run(async () => _meterReadingRepository.UpdateMeterReadings(metersReading.MetersReadings));
#pragma warning restore CS1998


            var rtnData = new List<MeterReadingDto>();

            var tmpReadings = _meterReadingRepository.GetMeterReadings();
            // var tmp = _mapper.Map<List<MeterReadingDto>>(tmpReadings);


            foreach (var item in tmpReadings)
            {
                var newReading = new MeterReadingDto
                {
                    AccountIddto = item.AccountId,
                    MeterReadingDateTimedto = item.MeterReadingDateTime.ToString(),
                    MeterReadValuedto = item.MeterReadValue,
                    RowNodto = item.RowNo
                };
                rtnData.Add(newReading);
            }
        }

        if (metersReading.FileImportResponse == ImportResponse.Failed) return BadRequest(metersReading.Errors);


        return Ok(metersReading.MetersReadings);
    }


    /// <summary>
    /// Uploads the data.
    /// </summary>
    /// <param name="meterreading">The meterreading.</param>
    /// <returns>IActionResult.</returns>
    [HttpPost("meterreading")]
    public async Task<IActionResult> UploadData(MeterReadingDto meterreading)
    {
        var metersReading = new List<MeterReading>();
        var input = new MeterReading();

        DateTime dateValue;
        var _dateGood = DateTime.TryParse(meterreading.MeterReadingDateTimedto, out dateValue);


        if (_dateGood)
        {
            input = new MeterReading
            {
                AccountId = meterreading.AccountIddto,
                MeterReadingDateTime = dateValue,
                MeterReadValue = meterreading.MeterReadValuedto
            };
            metersReading.Add(input);
#pragma warning disable CS1998
            await Task.Run(async () => _meterReadingRepository.UpdateMeterReadings(metersReading));
#pragma warning restore CS1998
        }

        return Ok(input);
    }
}
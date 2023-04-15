// ***********************************************************************
// Assembly         : ENSEK-API
// Author           : pdurr
// Created          : 04-12-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-15-2023
// ***********************************************************************
// <copyright file="meter-reading-uploads.cs" company="ENSEK-API">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Data;
using AutoMapper;
using ENSEK.Contracts;
using ENSEK.Entities.DTO;
using ENSEK.Entities.Extensions;
using ENSEK.Entities.Models;
using IronXL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;


namespace ENSEK_API.Controllers;



//readonly IAuthorRepository _authorRepository;
//public AuthorController(IAuthorRepository authorRepository)
//{
//    _authorRepository = authorRepository;
//}


/// <summary>
/// Class MeterReadingUploadsController.
/// Implements the <see cref="ControllerBase" />
/// </summary>
/// <seealso cref="ControllerBase" />
[Route("api/[controller]")]
[ApiController]
public class MeterReadingUploadsController : ControllerBase
{
    /// <summary>
    /// The configuration
    /// </summary>
    private readonly IConfiguration _configuration;
    /// <summary>
    /// The meter reading repository
    /// </summary>
    readonly IMeterReadingRepository _meterReadingRepository;

    /// <summary>
    /// The mapper
    /// </summary>
    private readonly IMapper _mapper;


    /// <summary>
    /// Initializes a new instance of the <see cref="MeterReadingUploadsController"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="meterReadingRepository">The meter reading repository.</param>
    /// <param name="mapper">The mapper.</param>
    public MeterReadingUploadsController(IConfiguration configuration, IMeterReadingRepository meterReadingRepository, IMapper mapper)
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
        var metersReading = new List<MeterReading>();

        var workBook = WorkBook.Load(file.OpenReadStream());

        // Select worksheet at index 0
        var workSheet = workBook.WorkSheets[0];

        var dataSet = workBook.ToDataSet();

        foreach (DataTable table in dataSet.Tables)
        {
            Console.WriteLine(table.TableName);
            var rowNo = 0;
            foreach (DataRow row in table.Rows)
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

                meterReading.RowNo = rowNo;
                rowNo++;

                metersReading.Add(meterReading);
            }

            if (_configuration["RemoveHeaderRow"] == "True")
                metersReading.RemoveAt(0);
        }

        _meterReadingRepository.UpdateMeterReadings(metersReading);


        List<MeterReadingDto> rtnData = new List<MeterReadingDto>();

        var fed = _meterReadingRepository.GetMeterReadings();
       // var fred = _mapper.Map<List<MeterReadingDto>>(fed);

        

        foreach (var item in fed)
        {
            MeterReadingDto ttemp = new MeterReadingDto() { AccountIddto = item.AccountId, MeterReadingDateTimedto = item.MeterReadingDateTime.ToString(), MeterReadValuedto = item.MeterReadValue, RowNodto = item.RowNo };
            //    MeterReadingDto ttemp = new MeterReadingDto() {}; //todoAccountIddto = item.AccountId 
            rtnData.Add(ttemp);
            
            //var tt = _mapper.Map<MeterReadingDto>(ttemp);
            //rtnData.Add(tt);
        }

     //   var rtn1 = _mapper.Map<List<MeterReadingDto>>(fed);




        return Ok(rtnData);
    }


    /// <summary>
    /// Uploads the data.
    /// </summary>
    /// <param name="meterreading">The meterreading.</param>
    /// <returns>IActionResult.</returns>
    [HttpPost("meterreading")]
    public async Task<IActionResult> UploadData(MeterReadingDto meterreading)
    {
        List<MeterReading> metersReading = new List<MeterReading>();
        var input = new MeterReading();
      
        DateTime dateValue;
        var _dateGood = DateTime.TryParse(meterreading.MeterReadingDateTimedto, out dateValue);


        if (_dateGood)
        {
             input = new MeterReading()
            {
                AccountId = meterreading.AccountIddto,
                MeterReadingDateTime = dateValue,
                MeterReadValue = meterreading.MeterReadValuedto
            };
            metersReading.Add(input);
            _meterReadingRepository.UpdateMeterReadings(metersReading);
        }

        return Ok(input);
    }
}
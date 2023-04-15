using System.Data;
using AutoMapper;
using ENSEK.Contracts;
using ENSEK.Entities.DTO;
using ENSEK.Entities.Extensions;
using ENSEK.Entities.Models;
using IronXL;

using Microsoft.AspNetCore.Mvc;


namespace ENSEK_API.Controllers;



//readonly IAuthorRepository _authorRepository;
//public AuthorController(IAuthorRepository authorRepository)
//{
//    _authorRepository = authorRepository;
//}


[Route("api/[controller]")]
[ApiController]
public class MeterReadingUploadsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    readonly IMeterReadingRepository _meterReadingRepository;

    private readonly IMapper _mapper;

    
    public MeterReadingUploadsController(IConfiguration configuration, IMeterReadingRepository meterReadingRepository, IMapper mapper)
    {
        _meterReadingRepository= meterReadingRepository;
        _configuration = configuration;
        _mapper = mapper;
    }


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

        List<MeterReadingDto> rtn = new List<MeterReadingDto>();

        var fed = _meterReadingRepository.GetMeterReadings();
            var fred = _mapper.Map<List<MeterReadingDto>>(fed);



        foreach (var item in fed)
        {
            MeterReadingDto ttemp= new MeterReadingDto() { AccountIddto = item.AccountId, MeterReadingDateTimedto = item.MeterReadingDateTime.ToString(), MeterReadValuedto = item.MeterReadValue, RowNodto = item.RowNo };
       //    MeterReadingDto ttemp = new MeterReadingDto() {}; //todoAccountIddto = item.AccountId 
            rtn.Add(ttemp);
        //    var tt = _mapper.Map<MeterReadingDto>(ttemp);

        }

        var rtn1=    _mapper.Map<List<MeterReadingDto>>(fed);


   
        return Ok(rtn);
    }


    [HttpPost("meterreading")]
    public async Task<Task> v(MeterReadingDto meterreading)
    {
        var metersReading = new List<MeterReading>();


        //bool success = Int64.TryParse(meterReadValue, out MeterReadValue);
        // if (success)
        // {
        //   //  Console.WriteLine("Converted '{0}' to {1}.", value, number);
        // }

        return Task.CompletedTask;
    }
}
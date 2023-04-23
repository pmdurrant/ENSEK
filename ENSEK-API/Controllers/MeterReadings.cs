using System.Runtime.InteropServices.ComTypes;
using ENSEK.Contracts;
using ENSEK.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENSEK_API.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    [Route("api/[controller]")]
    public class MeterReadings : Controller
    {
        private readonly IMeterReadingRepository _meterReadingsRepository;
        public MeterReadings(IMeterReadingRepository meterReadingRepository)
        {
            _meterReadingsRepository = meterReadingRepository;
        }

        [HttpGet ]
        // GET: MeterReadings
        public ActionResult GetReadings()
        {
            var rtn = _meterReadingsRepository.GetMeterReadings();

            return Ok(rtn);
        }

        // GET: MeterReadings/Details/5
        [HttpGet ("{id}", Name = "Details")]
        public ActionResult Details(int id)
        {

            var rtnlst = _meterReadingsRepository.GetMeterReadings();

           var rtn= from n in rtnlst where n.Id == id select n;
           return Ok(rtn);
        }
        
    }
}

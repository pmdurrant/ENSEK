using AutoMapper;
using BitMiracle.LibTiff.Classic;
using ENSEK.Contracts;
using ENSEK.Entities.Models;
using ENSEK_API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Moq;
using Npoi.Mapper;


namespace TestProjectAPI.Controller
{
    public class meterreadingControllerTest
    {
        private readonly MeterReadingUploadsController _controller;
        private readonly IMeterReadingRepository _service;
        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;

 

        public meterreadingControllerTest()
        {
            _service = new MeterReadingServiceFake();

    
            _configuration = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            _controller = new MeterReadingUploadsController(_configuration, _service, _mapper);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {

            var content = "AccountId,MeterReadingDateTime,MeterReadValue,\r\n2344,22/04/2019 09:24,1002,\r\n2233,22/04/2019 12:25:00,323,\r\n8766,22/04/2019 12:25:00,3440";
            var fileName = "test.xsl";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile file = new FormFile(stream, 0, stream.Length, "test_from_form", fileName);  // Act
            var okResult = _controller.Upload(file);


            Assert.NotNull(okResult);


            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result );
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
          

            var content = "AccountId,MeterReadingDateTime,MeterReadValue,\r\n2344,22/04/2019 09:24:00,1002,\r\n2233,22/04/2019 12:25,323,\r\n8766,22/04/2019 12:25:00,3440";
            var fileName = "test.xsl";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile file = new FormFile(stream, 0, stream.Length, "test_from_form", fileName);

            // Act
            var result = _controller.Upload(file);
       
            // Assert
            Assert.NotNull(result);
          

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
          

     
            Assert.IsType<List<MeterReading>>(okResult.Value);
            //Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
             var items = Assert.IsType<List<MeterReading>>(okResult.Value);
            Assert.Equal(3, items.Count);

        }
 
    }
}

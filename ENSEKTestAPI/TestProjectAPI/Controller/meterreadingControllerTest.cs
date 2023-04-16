using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using ENSEK.Contracts;
using ENSEK.Entities.Models;
using ENSEK_API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.InMemory.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Moq;
using NUnit.Framework;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Assert = Xunit.Assert;

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

            _controller = new MeterReadingUploadsController(_configuration, _service,_mapper);
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

          
           
            
            // Assert
            Assert.IsType<OkObjectResult>(okResult );
        }

        [Fact]
        public object Get_WhenCalled_ReturnsAllItems()
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
            var okResult = _controller.Upload(file) ;

            // Assert
            var items = Assert.IsType<List<MeterReading>>(okResult.Result);
            Assert.Equal(3, items.Count);

            return okResult.Result;
        }
     
        [SetUp]
        public void Setup()
        { 

        }
    }
}

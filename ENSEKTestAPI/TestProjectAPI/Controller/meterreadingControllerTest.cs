// ***********************************************************************
// Assembly         : TestProjectAPI
// Author           : pdurr
// Created          : 04-16-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-17-2023
// ***********************************************************************
// <copyright file="meterreadingControllerTest.cs" company="TestProjectAPI">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using ENSEK.Contracts;
using ENSEK.Entities.Models;
using ENSEK_API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp.Metadata.Profiles.Xmp;


namespace TestProjectAPI.Controller
{
    /// <summary>
    /// Class meterreadingControllerTest.
    /// </summary>
    public class meterreadingControllerTest
    {
        /// <summary>
        /// The controller
        /// </summary>
        private readonly MeterReadingUploadsController _controller;
        /// <summary>
        /// The service
        /// </summary>
        private readonly IMeterReadingRepository _service;
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;



        /// <summary>
        /// Initializes a new instance of the <see cref="meterreadingControllerTest"/> class.
        /// </summary>
        public meterreadingControllerTest()
        {
            _service = new MeterReadingServiceFake();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
             _mapper = config.CreateMapper();

            _configuration = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            _controller = new MeterReadingUploadsController(_configuration, _service, _mapper);
        }

        /// <summary>
        /// Defines the test method Get_WhenCalled_ReturnsOkResult.
        /// </summary>
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
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        /// <summary>
        /// Defines the test method Get_WhenCalled_ReturnsAllItems.
        /// </summary>
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

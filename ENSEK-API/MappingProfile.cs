// ***********************************************************************
// Assembly         : ENSEK-API
// Author           : pdurr
// Created          : 04-13-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-15-2023
// ***********************************************************************
// <copyright file="MappingProfile.cs" company="ENSEK-API">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Data;
using AutoMapper;
using ENSEK.Entities.DTO;
using ENSEK.Entities.Models;
using Microsoft.AspNet.Identity;
using NUnit.Framework;
using SixLabors.ImageSharp.ColorSpaces.Companding;

namespace ENSEK_API
{
    /// <summary>
    /// Class MappingProfile.
    /// Implements the <see cref="Profile" />
    /// </summary>
    /// <seealso cref="Profile" />
    public class MappingProfile : Profile
    {
        // This is the approach starting with version 5

        // This is the approach starting with version 5
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile" /> class.
        /// </summary>
        public MappingProfile()
        {
            //CreateMap<MeterReadingDto, MeterReading>().ForMember(d => d.Id, opt => opt.MapFrom(src => src.Iddto));
            //CreateMap<MeterReadingDto, MeterReading>().ReverseMap();

            CreateMap<MeterReading, MeterReadingDto>().ForMember(d => d.Iddto, opt => opt.MapFrom(src => src.Id)).ForMember(d => d.MeterReadingDateTimedto, opt => opt.MapFrom(src => src.MeterReadingDateTime));
            CreateMap<MeterReading, MeterReadingDto>().ReverseMap();

            //   CreateMap<List<MeterReading>, List<MeterReadingDto>>();
            //   CreateMap<List<MeterReading>, List<MeterReadingDto>>().ReverseMap();



        }



    }
}

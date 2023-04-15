using System.Data;
using AutoMapper;
using ENSEK.Entities.DTO;
using ENSEK.Entities.Models;


namespace ENSEK_API
{
     public class MappingProfile : Profile
    {
        // This is the approach starting with version 5
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

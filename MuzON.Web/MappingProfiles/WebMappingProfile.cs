using AutoMapper;
using MuzON.BLL.DTO;
using MuzON.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MuzON.Web.MappingProfiles
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<ArtistDTO, ArtistViewModel>(MemberList.None)
                .ForMember(f=>f.Image,opt=>opt.MapFrom(src=>Convert.ToBase64String(src.Image)))
                .ForMember(f=>f.BirthDate, opt=>opt.MapFrom(src=>DateTime.Parse(src.BirthDate.ToString("yyyy-MM-dd")))).ReverseMap();
            CreateMap<CountryDTO, CountryViewModel>(MemberList.None).ReverseMap();
        }
    }
}
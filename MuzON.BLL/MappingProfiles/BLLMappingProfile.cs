using AutoMapper;
using MuzON.BLL.DTO;
using MuzON.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.MappingProfiles
{
    public class BLLMappingProfile : Profile
    {
        public BLLMappingProfile()
        {
            CreateMap<Artist, ArtistDTO>(MemberList.None).ReverseMap();

            CreateMap<Band, BandDTO>(MemberList.None).ReverseMap();

            CreateMap<Song, SongDTO>(MemberList.None).ReverseMap();

            CreateMap<Genre, GenreDTO>(MemberList.None).ReverseMap();

            CreateMap<Country, CountryDTO>(MemberList.None).ReverseMap();

            CreateMap<Comment, CommentDTO>(MemberList.None).ReverseMap();

            CreateMap<Rating, RatingDTO>(MemberList.None).ReverseMap();

            CreateMap<Playlist, PlaylistDTO>(MemberList.None).ReverseMap();
        }
    }
}

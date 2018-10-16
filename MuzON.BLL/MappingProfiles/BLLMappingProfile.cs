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
            CreateMap<Artist, ArtistDetailsDTO>(MemberList.None).ReverseMap();
            CreateMap<Artist, ArtistIndexDTO>(MemberList.None).ReverseMap();

            CreateMap<Band, BandDTO>(MemberList.None).ReverseMap();
            CreateMap<Band, BandIndexDTO>(MemberList.None).ReverseMap();

            CreateMap<Song, SongDTO>(MemberList.None).ReverseMap();
            CreateMap<Song, SongToIndexDTO>(MemberList.None).ReverseMap();
            CreateMap<Song, SongDetailsDTO>(MemberList.None).ReverseMap();

            CreateMap<Country, CountryDTO>(MemberList.None).ReverseMap();

            CreateMap<Comment, CommentDTO>(MemberList.None).ReverseMap();

            CreateMap<Rating, RatingDTO>(MemberList.None).ReverseMap();

            CreateMap<Playlist, PlaylistDTO>(MemberList.None).ReverseMap();

            CreateMap<BandSong, BandSongDTO>(MemberList.None).ReverseMap();
        }
    }
}

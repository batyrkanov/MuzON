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
            CreateMap<Artist, ArtistDTO>(MemberList.None);
            CreateMap<ArtistDTO, Artist>(MemberList.None);
            CreateMap<Artist, ArtistDetailsDTO>(MemberList.None).ReverseMap();

            CreateMap<Band, BandDTO>(MemberList.None);
            CreateMap<BandDTO, Band>(MemberList.None);

            CreateMap<Song, SongDTO>(MemberList.None);
            CreateMap<SongDTO, Song>(MemberList.None);

            CreateMap<Country, CountryDTO>(MemberList.None);
            CreateMap<CountryDTO, Country>(MemberList.None);

            CreateMap<Comment, CommentDTO>(MemberList.None);
            CreateMap<CommentDTO, Comment>(MemberList.None);

            CreateMap<Rating, RatingDTO>(MemberList.None);
            CreateMap<RatingDTO, Rating>(MemberList.None);

            CreateMap<Playlist, PlaylistDTO>(MemberList.None);
            CreateMap<PlaylistDTO, Playlist>(MemberList.None);

            CreateMap<BandSong, BandSongDTO>(MemberList.None).ReverseMap();
        }
    }
}

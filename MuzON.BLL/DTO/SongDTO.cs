using MuzON.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.DTO
{
    public class SongDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public ICollection<GenreDTO> Genres { get; set; }
        public ICollection<PlaylistSongDTO> PlaylistSongs { get; set; }
        public ICollection<BandSongDTO> BandSongs { get; set; }
        public ICollection<ArtistDTO> Artists { get; set; }
        public ICollection<BandDTO> Bands { get; set; }
    }
}
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
        public virtual Playlist Playlist { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<BandSong> BandSongs { get; set; }
        public virtual ICollection<Playlist> Playlists { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.Domain.Entities
{
    public class Playlist
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public int Index { get; set; }
        public virtual ICollection<PlaylistSong> PlaylistSongs { get; set; }

        public Playlist()
        {
            PlaylistSongs = new List<PlaylistSong>();
        }
    }
}

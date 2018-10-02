using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.Domain.Entities
{
    public class PlaylistSong
    {
        public Guid Id { get; set; }
        public Guid SongId { get; set; }
        public Guid PlaylistId { get; set; }
        public int Index { get; set; }
        public virtual Song Song { get; set; }
        public virtual Playlist Playlist { get; set; }
    }
}

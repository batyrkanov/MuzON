using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.DTO
{
    public class PlaylistSongDTO
    {
        public Guid SongId { get; set; }
        public Guid PlaylistId { get; set; }
        public int Index { get; set; }
    }
}

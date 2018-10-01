using MuzON.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.DTO
{
    public class BandSongDTO
    {
        public Guid? BandId { get; set; }
        public Guid? ArtistId { get; set; }
        public Guid? SongId { get; set; }
        public virtual Band Band { get; set; }
        public virtual Song Song { get; set; }
        public virtual Artist Artist { get; set; }
    }
}

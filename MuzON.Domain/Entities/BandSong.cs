using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.Domain.Entities
{
    public class BandSong
    {
        public BandSong()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid? BandId { get; set; }
        public Guid? ArtistId { get; set; }
        public Guid SongId { get; set; }
        public virtual Band Band { get; set; }
        public virtual Song Song { get; set; }
        public virtual Artist Artist { get; set; }
    }
}

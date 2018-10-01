using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.Domain.Entities
{
    public class Artist
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
        public Guid? CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<BandSong> BandSongs { get; set; }
        public virtual ICollection<Band> Bands { get; set; }

        public Artist()
        {
            Bands = new List<Band>();
            BandSongs = new List<BandSong>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.Domain.Entities
{
    public class Band
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte[] Image { get; set; }
        public Guid CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<BandSong> BandSongs { get; set; }
        public virtual ICollection<Member> Members { get; set; }

        public Band()
        {
            Members = new List<Member>();
            BandSongs = new List<BandSong>();
        }
    }
}

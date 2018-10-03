using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.Domain.Entities
{
    public class Artist
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public byte[] Image { get; set; }
        public Guid CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<BandSong> BandSongs { get; set; }
        public virtual ICollection<Member> Members { get; set; }

        public Artist()
        {
            Members = new List<Member>();
            BandSongs = new List<BandSong>();
        }
    }
}

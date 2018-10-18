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
        public virtual ICollection<Song> Songs { get; set; }
        public virtual ICollection<Artist> Artists { get; set; }

        public Band()
        {
            Songs = new List<Song>();
            Artists = new List<Artist>();
        }
    }
}

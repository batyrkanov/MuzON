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
        public virtual ICollection<Song> Songs { get; set; }
        public virtual ICollection<Band> Bands { get; set; }

        public Artist()
        {
            Songs = new List<Song>();
            Bands = new List<Band>();
        }
    }
}

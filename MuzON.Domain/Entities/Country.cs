using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.Domain.Entities
{
    public class Country
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Band> Bands { get; set; }
        public virtual ICollection<Artist> Artists { get; set; }

        public Country()
        {
            Bands = new List<Band>();
            Artists = new List<Artist>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.Domain.Entities
{
    public class Member
    {
        public Guid Id { get; set; }
        public Guid ArtistId { get; set; }
        public Guid BandId { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual Band Band { get; set; }
    }
}

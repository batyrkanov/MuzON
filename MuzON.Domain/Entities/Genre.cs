using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.Domain.Entities
{
    public class Genre
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Song> Songs { get; set; }

        public Genre()
        {
            Songs = new List<Song>();
        }
    }
}

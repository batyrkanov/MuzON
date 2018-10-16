using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.DTO
{
    public class SongDetailsDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public ICollection<BandDTO> Bands { get; set; }
        public ICollection<ArtistDTO> Artists { get; set; }
    }
}

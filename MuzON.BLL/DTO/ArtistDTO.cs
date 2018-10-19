using MuzON.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.DTO
{
    public class ArtistDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public byte[] Image { get; set; }
        public CountryDTO Country { get; set; }
        public List<Guid> SelectedBands { get; set; }
        public bool IsSelected { get; set; }
        public ICollection<BandDTO> Bands { get; set; }
        public ICollection<SongDTO> Songs { get; set; }
    }
}

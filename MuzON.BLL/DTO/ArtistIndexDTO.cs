using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.DTO
{
    public class ArtistIndexDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public byte[] Image { get; set; }
        public string CountryName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MuzON.Web.Models
{
    public class BandIndexViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        public string Image { get; set; }
        public string CountryName { get; set; }
    }

    public class BandViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        public string Image { get; set; }
        public Guid CountryId { get; set; }
        public List<Guid> SelectedArtists { get; set; }
        public ICollection<ArtistViewModel> Artists { get; set; }
        
    }
}
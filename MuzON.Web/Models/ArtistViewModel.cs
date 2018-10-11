using MuzON.Common.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MuzON.Web.Models
{
    public class ArtistViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name cannot be null!")]
        [Display(Name = "Artist full name")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Date cannot be null!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Artist birth date")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Profile photo")]
        public string Image { get; set; }
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country cannot be null!")]
        public Guid CountryId { get; set; }
        [Display(Name = "Bands")]
        public List<Guid> SelectedBands { get; set; }
    }

    public class ArtistIndexViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
        public string CountryName { get; set; }
    }

    public class ArtistDetailsViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Artist full name")]
        public string FullName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
        public string CountryName { get; set; }
        public ICollection<BandDetailsViewModel> Bands { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MuzON.Web.Models
{
    public class BandViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name cannot be null!")]
        [Display(Name = "Band name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date cannot be null!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Band created date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Profile photo")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Country cannot be null!")]
        [Display(Name = "Country")]
        public Guid CountryId { get; set; }
        public CountryViewModel Country { get; set; }

        [Required(ErrorMessage = "You must select artists!")]
        [Display(Name = "Artists")]
        public List<Guid> SelectedArtists { get; set; }
        public bool IsSelected { get; set; }
        public ICollection<ArtistViewModel> Artists { get; set; }
        public ICollection<SongViewModel> Songs { get; set; }
    }
}
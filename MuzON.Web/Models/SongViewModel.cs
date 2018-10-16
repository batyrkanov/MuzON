using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MuzON.Web.Models
{
    public class SongViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Song name cannot be null")]
        public string Name { get; set; }
        public string FileName { get; set; }
        public Guid BandSongId { get; set; }
    }

    public class SongDetailsViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Song name cannot be null")]
        public string Name { get; set; }
        public string FileName { get; set; }
        public ICollection<ArtistViewModel> Artists { get; set; }
        public ICollection<BandViewModel> Bands { get; set; }
    }
}
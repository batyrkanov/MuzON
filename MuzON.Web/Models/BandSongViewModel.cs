using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MuzON.Web.Models
{
    public class BandSongViewModel
    {
        public Guid Id { get; set; }
        public Guid? BandId { get; set; }
        public Guid? ArtistId { get; set; }
        [Required(ErrorMessage = "Choose file to upload")]
        public Guid SongId { get; set; }
        public BandViewModel Band { get; set; }
        public ArtistViewModel Artist { get; set; }
        public SongViewModel Song { get; set; }
    }
}
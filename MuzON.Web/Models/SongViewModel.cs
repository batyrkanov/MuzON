using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MuzON.Web.Models
{
    public class SongViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public ICollection<BandSongViewModel> Songs { get; set; }
    }
}
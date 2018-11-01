using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MuzON.Web.Models
{
    public class PlaylistViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Index { get; set; }
        public List<Guid> SelectedSongs { get; set; }
        public double Rating { get; set; }
        public ICollection<SongViewModel> Songs { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }
    }
}
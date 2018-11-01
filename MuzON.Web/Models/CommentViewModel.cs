using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MuzON.Web.Models
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid? SongId { get; set; }
        public Guid UserId { get; set; }
        public Guid? PlaylistId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MuzON.Web.Models
{
    public class RatingViewModel
    {
        public Guid Id { get; set; }
        public double Value { get; set; }
        public Guid? SongId { get; set; }
        public Guid? PlaylistId { get; set; }
        public Guid UserId { get; set; }
    }
}
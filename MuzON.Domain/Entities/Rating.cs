using MuzON.Domain.Identity;
using System;

namespace MuzON.Domain.Entities
{
    public class Rating
    {
        public Guid Id { get; set; }
        public double Value { get; set; }
        public Guid? SongId { get; set; }
        public Guid? PlaylistId { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
        public virtual Song Song { get; set; }
        public virtual Playlist Playlist { get; set; }
    }
}

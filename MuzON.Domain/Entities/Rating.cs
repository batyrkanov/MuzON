using System;

namespace MuzON.Domain.Entities
{
    public class Rating
    {
        public Guid Id { get; set; }
        public int RatingValue { get; set; }
        public Guid SongId { get; set; }
        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Song Song { get; set; }
    }
}

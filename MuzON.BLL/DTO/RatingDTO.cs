using MuzON.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.DTO
{
    public class RatingDTO
    {
        public Guid Id { get; set; }
        public double Value { get; set; }
        public Guid? SongId { get; set; }
        public Guid? PlaylistId { get; set; }
        public Guid UserId { get; set; }
    }
}

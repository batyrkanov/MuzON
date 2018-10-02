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
        public int RatingValue { get; set; }
        public Guid SongId { get; set; }
        public Guid UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.DTO
{
    public class PlaylistDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public int Index { get; set; }
        public ICollection<SongDTO> Songs { get; set; }
        public ICollection<CommentDTO> Comments { get; set; }
    }
}

using MuzON.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.Interfaces
{
    public interface ICommentService
    {
        IEnumerable<CommentDTO> GetComments();
        IEnumerable<CommentDTO> GetCommentsBySongId(Guid Id);
        IEnumerable<CommentDTO> GetCommentsByUserId(Guid Id);
        void AddComment(CommentDTO commentDTO);
        void Dispose();
    }
}

using AutoMapper;
using MuzON.BLL.DTO;
using MuzON.BLL.Interfaces;
using MuzON.Domain.Entities;
using MuzON.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.Services
{
    public class CommentService : ICommentService
    {
        private IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public void AddComment(CommentDTO commentDTO)
        {
            Comment comment = Mapper.Map<CommentDTO, Comment>(commentDTO);
            _unitOfWork.Comments.Create(comment);
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<CommentDTO> GetComments()
        {
            var comments = _unitOfWork.Comments.GetAll().ToList();
            return Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(comments);
        }

        public IEnumerable<CommentDTO> GetCommentsBySongId(Guid Id)
        {
            var allComments = _unitOfWork.Comments.GetAll();
            var comments = (from c in allComments
                            where c.SongId == Id
                            select c).ToList();
            return Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(comments);
        }

        public IEnumerable<CommentDTO> GetCommentsByUserId(Guid Id)
        {
            var allComments = _unitOfWork.Comments.GetAll();
            var comments = (from c in allComments
                            where c.UserId == Id
                            select c).ToList();
            return Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(comments);
        }
    }
}

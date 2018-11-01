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
    public class GenreService : IGenreService
    {
        private IUnitOfWork _unitOfWork;

        public GenreService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public void AddGenre(GenreDTO genreDTO)
        {
            Genre genre = Mapper.Map<GenreDTO, Genre>(genreDTO);
            _unitOfWork.Genres.Create(genre);
            _unitOfWork.Save();
        }

        public void DeleteGenre(GenreDTO genreDTO)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public GenreDTO GetGenreById(Guid Id)
        {
            Genre genre = _unitOfWork.Genres.Get(Id);
            return Mapper.Map<Genre, GenreDTO>(genre);
        }

        public IEnumerable<GenreDTO> GetGenres()
        {
            IEnumerable<Genre> genres = _unitOfWork.Genres.GetAll().ToList();
            return Mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>(genres);
        }

        public void UpdateGenre(GenreDTO genreDTO)
        {
            Genre genre = Mapper.Map<GenreDTO, Genre>(genreDTO);
            _unitOfWork.Genres.Update(genre);
            _unitOfWork.Save();
        }
    }
}

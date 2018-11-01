using MuzON.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.Interfaces
{
    public interface IGenreService
    {
        IEnumerable<GenreDTO> GetGenres();
        GenreDTO GetGenreById(Guid Id);
        void AddGenre(GenreDTO genreDTO);
        void DeleteGenre(GenreDTO genreDTO);
        void UpdateGenre(GenreDTO genreDTO);
        void Dispose();
    }
}

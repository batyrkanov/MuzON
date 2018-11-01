using MuzON.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.Interfaces
{
    public interface ISongService
    {
        //IEnumerable<SongDTO> GetArtistRepertoire(Guid id);
        //IEnumerable<SongDTO> GetBandRepertoire(Guid id);
        IEnumerable<SongDTO> GetSongs();
        //List<Guid> GetSelectedBands(Guid songId);
        //List<Guid> GetSelectedArtists(Guid songId);
        SongDTO GetSongById(Guid id);
        void DeleteSong(SongDTO songDTO);
        void AddSong(SongDTO songDTO);
        void UpdateSong(SongDTO songDTO);
        void Dispose();
    }
}

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
        void AddBandSong(BandSongDTO bandSongDTO);
        void AddSong(SongDTO songDTO);
        IEnumerable<SongDTO> GetSongs();
        void DeleteSong(SongDTO songDTO);
        void Dispose();
        SongDTO GetSongById(Guid id);
    }
}

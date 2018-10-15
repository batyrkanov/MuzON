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
        IEnumerable<BandSongDTO> GetArtistRepertoire(Guid id);
        void AddBandSong(BandSongDTO bandSongDTO);
        IEnumerable<SongDTO> GetSongs();
        void DeleteSong(SongDTO songDTO);
        void Dispose();
        SongDTO GetSongById(Guid id);
        BandSongDTO GetBandSongById(Guid id);
        BandSongDTO GetBandSongBySongId(Guid id);
        void UpdateSong(BandSongDTO bandSongDTO);
        IEnumerable<BandSongDTO> GetBandRepertoire(Guid id);
    }
}

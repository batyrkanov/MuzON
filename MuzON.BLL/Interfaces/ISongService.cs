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
        IEnumerable<BandSongDTO> GetBandRepertoire(Guid id);
        IEnumerable<SongDTO> GetSongs();
        IEnumerable<SongToIndexDTO> GetSongsToIndex();
        SongDTO GetSongById(Guid id);
        BandSongDTO GetBandSongById(Guid id);
        BandSongDTO GetBandSongBySongId(Guid id);
        SongDetailsDTO GetDetailSong(Guid id);
        void DeleteSong(SongDTO songDTO);
        void AddBandSong(BandSongDTO bandSongDTO);
        void UpdateSong(BandSongDTO bandSongDTO);
        void Dispose();
    }
}

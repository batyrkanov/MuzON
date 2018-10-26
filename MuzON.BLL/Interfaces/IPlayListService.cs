using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MuzON.BLL.DTO;

namespace MuzON.BLL.Interfaces
{
    public interface IPlayListService
    {
        IEnumerable<PlaylistDTO> GetPlaylists();
        PlaylistDTO GetPlaylistById(Guid id);
        void AddPlayList(PlaylistDTO playlistDTO);
    }
}

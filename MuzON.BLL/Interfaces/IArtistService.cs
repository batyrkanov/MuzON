using MuzON.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MuzON.BLL.Interfaces
{
    public interface IArtistService
    {
        IEnumerable<ArtistDTO> GetArtists();
        ArtistDTO GetArtistById(Guid Id);
        void AddArtist(ArtistDTO artistDTO, List<HttpPostedFileBase> songs);
        IEnumerable<SongDTO> GetSongs();
        void DeleteArtist(ArtistDTO artistDTO);
        void UpdateArtist(ArtistDTO artistDTO, Guid[] selectedBands);
        void Dispose();
    }
}

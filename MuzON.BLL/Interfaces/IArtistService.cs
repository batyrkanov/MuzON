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
        List<Guid> GetSelectedBands(Guid artistId);
        ArtistDTO GetArtistById(Guid Id);
        void AddArtist(ArtistDTO artistDTO);
        void DeleteArtist(ArtistDTO artistDTO);
        void UpdateArtist(ArtistDTO artistDTO, Guid[] selectedBands);
        void Dispose();
    }
}

using MuzON.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.Interfaces
{
    public interface IArtistService
    {
        IEnumerable<ArtistDTO> GetArtists();
        ArtistDTO GetArtistById(Guid Id);
        void AddArtist(ArtistDTO artistDTO, Guid countryId);
        void DeleteArtist(ArtistDTO artistDTO);
        void UpdateArtist(ArtistDTO artistDTO, Guid countryId);
        void Dispose();
    }
}

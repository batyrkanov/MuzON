using MuzON.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.Interfaces
{
    public interface IBandService
    {
        IEnumerable<BandDTO> GetBands();
        BandDTO GetBandById(Guid id);
        void AddBand(BandDTO bandDTO, Guid[] selectedArtists);
        void DeleteBand(BandDTO bandDTO);
        void UpdateBand(BandDTO bandDTO, Guid[] selectedArtists);
    }
}

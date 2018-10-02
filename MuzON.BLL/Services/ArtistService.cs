using AutoMapper;
using MuzON.BLL.DTO;
using MuzON.BLL.Interfaces;
using MuzON.Domain.Entities;
using MuzON.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.Services
{
    public class ArtistService : IArtistService
    {
        private IUnitOfWork _unitOfWork;

        public ArtistService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public void AddArtist(ArtistDTO artistDTO)
        {
            Artist artist = Mapper.Map<ArtistDTO, Artist>(artistDTO);
            _unitOfWork.Artists.Create(artist);
            _unitOfWork.Save();
        }

        public void DeleteArtist(ArtistDTO artistDTO)
        {
            Artist artist = Mapper.Map<ArtistDTO, Artist>(artistDTO);
            _unitOfWork.Artists.Delete(artist.Id);
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public ArtistDTO GetArtistById(Guid Id)
        {
            Artist artist = _unitOfWork.Artists.Get(Id);
            return Mapper.Map<Artist, ArtistDTO>(artist);
        }

        public IEnumerable<ArtistDTO> GetArtists()
        {
            var artists = _unitOfWork.Artists.GetAll().ToList();
            return Mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistDTO>>(artists);
        }

        public void UpdateArtist(ArtistDTO artistDTO)
        {
            Artist artist = Mapper.Map<ArtistDTO, Artist>(artistDTO);
            _unitOfWork.Artists.Update(artist);
            _unitOfWork.Save();
        }
    }
}

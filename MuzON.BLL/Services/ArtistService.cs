using AutoMapper;
using MuzON.BLL.DTO;
using MuzON.BLL.Interfaces;
using MuzON.Domain.Entities;
using MuzON.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MuzON.BLL.Services
{
    public class ArtistService : IArtistService
    {
        private IUnitOfWork _unitOfWork;

        public ArtistService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public void AddArtist(ArtistDTO artistDTO, Guid[] selectedBands)
        {
            Artist artist = Mapper.Map<ArtistDTO, Artist>(artistDTO);
            Country country = _unitOfWork.Countries
                .SearchFor(x => x.Id == artist.CountryId).First();
            if (selectedBands != null)
            {
                artist.Bands = new List<Band>();

                foreach (var c in _unitOfWork.Bands.SearchFor(co => selectedBands.Contains(co.Id)))
                {
                    artist.Bands.Add(c);
                }
            }
            artist.CountryId = artistDTO.Country.Id;
            artist.Country = country;
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

        public void UpdateArtist(ArtistDTO artistDTO, Guid[] selectedBands)
        {
            Artist artist = _unitOfWork.Artists.Get(artistDTO.Id);
            var countryId = artist.CountryId;
            
            Mapper.Map(artistDTO, artist);
            
            if (selectedBands != null)
            {
                if (artist.Bands == null)
                    artist.Bands = new List<Band>();
                artist.Bands.Clear();
                foreach (var c in _unitOfWork.Bands.SearchFor(co => selectedBands.Contains(co.Id)))
                {
                    artist.Bands.Add(c);
                }
            }

            if (artist.Id != countryId)
            {
                Country country = _unitOfWork.Countries
                    .SearchFor(x => x.Id == artist.CountryId).First();
                artist.Country = country;
            }

            _unitOfWork.Artists.Update(artist);
            _unitOfWork.Save();
        }
    }
}

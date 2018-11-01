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

        public void AddArtist(ArtistDTO artistDTO)
        {
            Artist artist = Mapper.Map<ArtistDTO, Artist>(artistDTO);
            Country country = _unitOfWork.Countries
                .SearchFor(x => x.Id == artistDTO.Country.Id).First();

            if (artistDTO.SelectedBands != null)
            {
                artist.Bands = new List<Band>();

                foreach (var c in _unitOfWork.Bands.SearchFor(co => artistDTO.SelectedBands.Contains(co.Id)))
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

        public List<Guid> GetSelectedBands(Guid artistId)
        {
            List<Guid> guids = new List<Guid>();
            var artist = _unitOfWork.Artists.Get(artistId);
            if (artist.Bands.Count > 0)
            {
                foreach (var band in artist.Bands)
                    guids.Add(band.Id);
            }
            return guids;
        }

        public void UpdateArtist(ArtistDTO artistDTO)
        {
            Artist artist = _unitOfWork.Artists.Get(artistDTO.Id);
            List<Song> songs = new List<Song>();
            foreach (var song in artist.Songs)
            {
                songs.Add(song);
            }
            var countryId = artist.CountryId == artistDTO.Country.Id ? artist.CountryId : artistDTO.Country.Id;
            artistDTO.Country = null;
            
            Mapper.Map(artistDTO, artist);
            if (artistDTO.SelectedBands.Count > 0)
            {
                if (artist.Bands == null)
                    artist.Bands = new List<Band>();
                artist.Bands.Clear();
                foreach (var c in _unitOfWork.Bands.SearchFor(co => artistDTO.SelectedBands.Contains(co.Id)))
                {
                    artist.Bands.Add(c);
                }
            }

            Country country = _unitOfWork.Countries.SearchFor(x => x.Id == countryId).Single();
            artist.Country = country;
            artist.CountryId = country.Id;
            artist.Songs = songs;
            _unitOfWork.Artists.Update(artist);
            _unitOfWork.Save();
        }
    }
}

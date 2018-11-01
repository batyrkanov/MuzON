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
    public class SongService : ISongService
    {
        private IUnitOfWork _unitOfWork;

        public SongService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public void AddSong(SongDTO songDTO)
        {
            Song song = Mapper.Map<Song>(songDTO);
            if (songDTO.Artists.Count > 0)
            {
                song.Artists = new List<Artist>();
                foreach (var artist in _unitOfWork.Artists.SearchFor(
                                        a => songDTO.Artists.Select(x => x.Id).ToList().Contains(a.Id)))
                {
                    song.Artists.Add(artist);
                }
            }

            if (songDTO.Bands.Count > 0)
            {
                song.Bands = new List<Band>();
                foreach (var band in _unitOfWork.Bands.SearchFor(
                                        b => songDTO.Bands.Select(x => x.Id).ToList().Contains(b.Id)))
                {
                    song.Bands.Add(band);
                }
            }

            if (songDTO.Genres.Count > 0)
            {
                song.Genres = new List<Genre>();
                foreach (var genre in _unitOfWork.Genres.SearchFor(
                                        b => songDTO.Genres.Select(x => x.Id).ToList().Contains(b.Id)))
                {
                    song.Genres.Add(genre);
                }
            }
            _unitOfWork.Songs.Create(song);
            _unitOfWork.Save();
        }

        public void DeleteSong(SongDTO songDTO)
        {
            Song song = Mapper.Map<Song>(songDTO);
            _unitOfWork.Songs.Delete(song.Id);
            _unitOfWork.Save();
        }

        public SongDTO GetSongById(Guid id)
        {
            var song = _unitOfWork.Songs.Get(id);
            List<Artist> artists = new List<Artist>();
            List<Band> bands = new List<Band>();
            foreach (var artist in song.Artists)
            {
                if (artist != null)
                    artists.Add(artist);
            }
            foreach (var band in song.Bands)
            {
                if (band != null)
                    bands.Add(band);
            }
            var songDTO = Mapper.Map<SongDTO>(song);
            songDTO.Artists = Mapper.Map<List<ArtistDTO>>(artists);
            songDTO.Bands = Mapper.Map<List<BandDTO>>(bands);
            return songDTO;
        }

        public IEnumerable<SongDTO> GetSongs()
        {
            return Mapper.Map<IEnumerable<SongDTO>>(_unitOfWork.Songs.GetAll().ToList());
        }

        public void UpdateSong(SongDTO songDTO)
        {
            Song song = _unitOfWork.Songs.Get(songDTO.Id);
            if (songDTO.FileName == null)
                songDTO.FileName = song.FileName;
            Mapper.Map(songDTO, song);

            if (songDTO.Artists.Count > 0)
            {
                if (song.Artists == null)
                    song.Artists = new List<Artist>();
                song.Artists.Clear();
                foreach (var artist in _unitOfWork.Artists.SearchFor(
                                        a => songDTO.Artists.Select(x => x.Id).ToList().Contains(a.Id)))
                {
                    song.Artists.Add(artist);
                }
            }

            if (songDTO.Bands.Count > 0)
            {
                if (song.Bands == null)
                    song.Bands = new List<Band>();
                song.Bands.Clear();
                foreach (var band in _unitOfWork.Bands.SearchFor(
                                        a => songDTO.Bands.Select(x => x.Id).ToList().Contains(a.Id)))
                {
                    song.Bands.Add(band);
                }
            }

            if (songDTO.Genres.Count > 0)
            {
                if (song.Genres == null)
                    song.Genres = new List<Genre>();
                song.Genres.Clear();
                foreach (var genre in _unitOfWork.Genres.SearchFor(
                                        a => songDTO.Genres.Select(x => x.Id).ToList().Contains(a.Id)))
                {
                    song.Genres.Add(genre);
                }
            }
            _unitOfWork.Songs.Update(song);
            _unitOfWork.Save();
        }
        
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}

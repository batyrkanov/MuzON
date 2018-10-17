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

        // get max count value from two lists for using in GetBandSongDTOs method
        public int GetMax(List<ArtistDTO> artists, List<BandDTO> bands)
        {
            return artists.Count >= bands.Count ? artists.Count : bands.Count;
        }

        // returned List<BandSongDTO> for saving
        public List<BandSongDTO> GetBandSongDTOs(SongDTO songDTO, int maxItemsInList)
        {
            List<BandSongDTO> bandSongDTOs = new List<BandSongDTO>();
            for (int i = 0; i < maxItemsInList; i++)
            {
                var bandSong = new BandSongDTO
                {
                    Id = Guid.NewGuid()
                };

                if (bandSong.ArtistId == null && songDTO.Artists.ToList().Count > i)
                {
                    bandSong.Artist = songDTO.Artists.ToList()[i];
                    bandSong.ArtistId = bandSong.Artist.Id;
                }

                if (bandSong.BandId == null && songDTO.Bands.ToList().Count > i)
                {
                    bandSong.Band = songDTO.Bands.ToList()[i];
                    bandSong.BandId = bandSong.Band.Id;
                }
                bandSong.Song = songDTO;
                bandSong.SongId = songDTO.Id;
                bandSongDTOs.Add(bandSong);
            }
            return bandSongDTOs;
        }

        

        public void AddSong(SongDTO songDTO)
        {
            List<BandSongDTO> bandSongDTOs = new List<BandSongDTO>();
            int maxItemsInList = GetMax(songDTO.Artists.ToList(), songDTO.Bands.ToList());
            Song song = Mapper.Map<Song>(songDTO);
            List<BandSong> bandSongs = Mapper.Map<List<BandSong>>(GetBandSongDTOs(songDTO, maxItemsInList));
            _unitOfWork.Songs.Create(song);
            _unitOfWork.Save();
            foreach (var bandSong in bandSongs)
            {
                if (bandSong.ArtistId != null)
                    bandSong.Artist = _unitOfWork.Artists.Get(bandSong.ArtistId);
                if (bandSong.BandId != null)
                    bandSong.Band = _unitOfWork.Bands.Get(bandSong.BandId);
                if (bandSong.SongId != null)
                    bandSong.Song = _unitOfWork.Songs.Get(bandSong.SongId);
                _unitOfWork.BandSongs.Create(bandSong);
                _unitOfWork.Save();
            }
            
        }

        public void DeleteSong(SongDTO songDTO)
        {
            Song song = Mapper.Map<Song>(songDTO);
            var bandSongs = _unitOfWork.BandSongs.SearchFor(x => x.SongId == song.Id).ToList();
            foreach (var bandSong in bandSongs)
            {
                _unitOfWork.BandSongs.Delete(bandSong.Id);
            }
            _unitOfWork.Songs.Delete(song.Id);
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public BandSongDTO GetBandSongById(Guid id)
        {
            var bandSong = _unitOfWork.BandSongs.Get(id);
            return Mapper.Map<BandSongDTO>(bandSong);
        }

        public IEnumerable<BandSongDTO> GetArtistRepertoire(Guid id)
        {
            var songs = _unitOfWork.BandSongs.SearchFor(x => x.ArtistId == id).ToList();
            return Mapper.Map<IEnumerable<BandSongDTO>>(songs);
        }

        public SongDTO GetSongById(Guid id)
        {
            var song = _unitOfWork.Songs.Get(id);
            List<Artist> artists = new List<Artist>();
            List<Band> bands = new List<Band>();
            foreach (var bandSong in song.BandSongs)
            {
                if (bandSong.Artist != null)
                    artists.Add(bandSong.Artist);
                if (bandSong.Band != null)
                    bands.Add(bandSong.Band);
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

        // method for getting updated bandsong data 
        public BandSong GetBandSongToUpdate(BandSong bandSong, Guid songId, Guid? artistId = null, Guid? bandId = null)
        {
            if (bandSong.ArtistId != artistId)
                bandSong.ArtistId = artistId;
            if (bandSong.BandId != bandId)
                bandSong.BandId = bandId;
            if (bandSong.SongId != songId)
                bandSong.SongId = songId;
            return bandSong;
        }

        public void UpdateSong(SongDTO songDTO)
        {
            List<BandSong> bandSongsList = new List<BandSong>();
            var bandSongs = _unitOfWork.BandSongs.SearchFor(x => x.SongId == songDTO.Id).ToList();
            Song song = _unitOfWork.Songs.Get(songDTO.Id);
            if (songDTO.FileName == null)
                songDTO.FileName = song.FileName;
            int maxItemInList = GetMax(songDTO.Artists.ToList(), songDTO.Bands.ToList());
            
            song.BandSongs.Clear();
            Mapper.Map(songDTO, song);
            song.BandSongs = new List<BandSong>();
            _unitOfWork.Songs.Update(song);

            int valueForLoop = bandSongs.Count > maxItemInList ? bandSongs.Count : maxItemInList;
            for (int i = 0; i < valueForLoop; i++)
            {
                if(songDTO.Bands.ToList().Count > i && songDTO.Artists.ToList().Count > i && bandSongs.Count > i)
                    bandSongsList.Add(GetBandSongToUpdate(bandSongs[i], songDTO.Id, songDTO.Artists.ToList()[i].Id, songDTO.Bands.ToList()[i].Id));
                else if (songDTO.Bands.ToList().Count > i && bandSongs.Count > i)
                    bandSongsList.Add(GetBandSongToUpdate(bandSongs[i], songDTO.Id, null, songDTO.Bands.ToList()[i].Id));
                else if (songDTO.Artists.ToList().Count > i && bandSongs.Count > i)
                    bandSongsList.Add(GetBandSongToUpdate(bandSongs[i], songDTO.Id, songDTO.Artists.ToList()[i].Id));
                else
                {
                    var bandSong = new BandSong
                    {
                        Id = Guid.NewGuid()
                    };
                    if (songDTO.Artists.ToList().Count > i)
                        bandSong.ArtistId = songDTO.Artists.ToList()[i].Id;
                    if (songDTO.Bands.ToList().Count > i)
                        bandSong.BandId = songDTO.Bands.ToList()[i].Id;
                    bandSong.SongId = songDTO.Id;
                    bandSongsList.Add(bandSong);
                }
            }

            foreach (var bandSong in bandSongsList)
            {
                if (bandSong.ArtistId != null)
                    bandSong.Artist = _unitOfWork.Artists.Get(bandSong.ArtistId);
                if (bandSong.BandId != null)
                    bandSong.Band = _unitOfWork.Bands.Get(bandSong.BandId);
                bandSong.Song = _unitOfWork.Songs.Get(bandSong.SongId);
                if(!bandSongs.Contains(bandSong))
                    _unitOfWork.BandSongs.Create(bandSong);
                else
                    _unitOfWork.BandSongs.Update(bandSong);
                _unitOfWork.Save();
            }
        }

        public IEnumerable<BandSongDTO> GetBandRepertoire(Guid id)
        {
            var songs = _unitOfWork.BandSongs.SearchFor(x => x.BandId == id).ToList();
            return Mapper.Map<IEnumerable<BandSongDTO>>(songs);
        }

        public List<Guid> GetSelectedArtists(Guid songId)
        {
            List<Guid> artistGuids = new List<Guid>();
            var song = _unitOfWork.Songs.SearchFor(x => x.Id == songId).FirstOrDefault();
            foreach (var bandSong in song.BandSongs)
            {
                if (bandSong.ArtistId != null)
                    artistGuids.Add(bandSong.ArtistId.Value);
            }
            return artistGuids;
        }

        public List<Guid> GetSelectedBands(Guid songId)
        {
            List<Guid> bandGuids = new List<Guid>();
            var song = _unitOfWork.Songs.SearchFor(x => x.Id == songId).FirstOrDefault();
            foreach (var bandSong in song.BandSongs)
            {
                if (bandSong.BandId != null)
                    bandGuids.Add(bandSong.BandId.Value);
            }
            return bandGuids;
        }
    }
}

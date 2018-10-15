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
    public class SongService : ISongService
    {
        private IUnitOfWork _unitOfWork;

        public SongService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public void AddBandSong(BandSongDTO bandSongDTO)
        {
            var bandSong = Mapper.Map<BandSong>(bandSongDTO);
            if (bandSong.ArtistId != null)
                bandSong.Artist = _unitOfWork.Artists.Get(bandSong.ArtistId);
            if (bandSong.BandId != null)
                bandSong.Band = _unitOfWork.Bands.Get(bandSong.BandId);
            _unitOfWork.Songs.Create(bandSong.Song);
            _unitOfWork.BandSongs.Create(bandSong);
            _unitOfWork.Save();
        }

        public void DeleteSong(SongDTO songDTO)
        {
            Song song = Mapper.Map<Song>(songDTO);
            BandSong bandSong = _unitOfWork.BandSongs.SearchFor(x => x.SongId == song.Id).FirstOrDefault();
            _unitOfWork.BandSongs.Delete(bandSong.Id);
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

        public BandSongDTO GetBandSongBySongId(Guid id)
        {
            var bandSong = _unitOfWork.BandSongs.SearchFor(x => x.SongId == id).FirstOrDefault();
            return Mapper.Map<BandSongDTO>(bandSong);
        }

        public IEnumerable<BandSongDTO> GetArtistRepertoire(Guid id)
        {
            var songs = _unitOfWork.BandSongs.SearchFor(x=>x.ArtistId==id).ToList();
            return Mapper.Map<IEnumerable<BandSongDTO>>(songs);
        }

        public SongDTO GetSongById(Guid id)
        {
            return Mapper.Map<SongDTO>(_unitOfWork.Songs.Get(id));
        }

        public IEnumerable<SongDTO> GetSongs()
        {
            return Mapper.Map<IEnumerable<SongDTO>>(_unitOfWork.Songs.GetAll().ToList());
        }

        public void UpdateSong(BandSongDTO bandSongDTO)
        {
            var bandSong = _unitOfWork.BandSongs.Get(bandSongDTO.Id);
            //var song = _unitOfWork.Songs.Get(bandSong.SongId);

            Mapper.Map(bandSongDTO, bandSong);
           // Mapper.Map(bandSongDTO.Song, song);
            _unitOfWork.BandSongs.Update(bandSong);
            _unitOfWork.Save(); 
        }

        public IEnumerable<BandSongDTO> GetBandRepertoire(Guid id)
        {
            var songs = _unitOfWork.BandSongs.SearchFor(x => x.BandId == id).ToList();
            return Mapper.Map<IEnumerable<BandSongDTO>>(songs);
        }

        //public List<BandSong> AddSong(List<HttpPostedFileBase> songs, Artist artist)
        //{
        //    List<BandSong> bandSongs = new List<BandSong>();

        //    foreach (var songItem in songs)
        //    {
        //        BandSong bandSong = new BandSong();
        //        Song song = new Song()
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = Path.GetFileName(songItem.FileName),
        //            FileName = songItem.FileName
        //        };
        //        bandSong.Song = song;
        //        bandSong.SongId = song.Id;
        //        bandSong.Artist = artist;
        //        bandSong.ArtistId = artist.Id;

        //        bandSongs.Add(bandSong);
        //    }

        //    return bandSongs;
        //}
    }
}

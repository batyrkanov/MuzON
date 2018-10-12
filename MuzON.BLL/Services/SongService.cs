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

        public void AddSong(SongDTO songDTO)
        {
            throw new NotImplementedException();
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

        public SongDTO GetSongById(Guid id)
        {
            return Mapper.Map<SongDTO>(_unitOfWork.Songs.Get(id));
        }

        public IEnumerable<SongDTO> GetSongs()
        {
            return Mapper.Map<IEnumerable<SongDTO>>(_unitOfWork.Songs.GetAll().ToList());
        }
    }
}

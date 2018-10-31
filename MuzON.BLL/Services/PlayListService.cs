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
    public class PlayListService : IPlayListService
    {
        private IUnitOfWork _unitOfWork;

        public PlayListService(IUnitOfWork uow) =>
            _unitOfWork = uow;

        public void AddPlayList(PlaylistDTO playlistDTO)
        {
            var playList = Mapper.Map<Playlist>(playlistDTO);
            if (playlistDTO.Songs.Count > 0)
            {
                playList.Songs = new List<Song>();
                foreach (var song in _unitOfWork.Songs.SearchFor(
                                        a => playlistDTO.Songs.Select(x => x.Id).ToList().Contains(a.Id)))
                {
                    playList.Songs.Add(song);
                }
            }
            _unitOfWork.Playlists.Create(playList);
            _unitOfWork.Save();
        }

        public void DeletePlaylist(PlaylistDTO playlistDTO)
        {
            var playlist = Mapper.Map<Playlist>(playlistDTO);
            var ratings = _unitOfWork.Ratings.SearchFor(x => x.PlaylistId == playlist.Id).ToList();
            if(ratings.Count > 0)
            {
                foreach (var rating in ratings)
                {
                    _unitOfWork.Ratings.Delete(rating.Id);
                }
            }
            _unitOfWork.Playlists.Delete(playlist.Id);
            _unitOfWork.Save();
        }

        public PlaylistDTO GetPlaylistById(Guid id)
        {
            var playList = _unitOfWork.Playlists.Get(id);
            return Mapper.Map<PlaylistDTO>(playList);
        }

        public IEnumerable<PlaylistDTO> GetPlaylists()
        {
            IEnumerable<Playlist> playLists = _unitOfWork.Playlists.GetAll().ToList();
            return Mapper.Map<IEnumerable<PlaylistDTO>>(playLists); ;
        }

        public double PlaylistRating(Guid id)
        {
            var avarageRate = _unitOfWork.Ratings.SearchFor(x => x.PlaylistId == id).Select(x => x.Value);
            return avarageRate.Count() == 0 ? 0 : avarageRate.Average();
        }

        public void RatePlayList(RatingDTO ratingDTO)
        {
            var rating = Mapper.Map<Rating>(ratingDTO);
            if (rating.PlaylistId != null)
                rating.Playlist = _unitOfWork.Playlists.Get(rating.PlaylistId);
            if (rating.SongId != null)
                rating.Song = _unitOfWork.Songs.Get(rating.SongId);
            rating.User = _unitOfWork.Users.Get(rating.UserId);
            _unitOfWork.Ratings.Create(rating);
            _unitOfWork.Save();
        }
    }
}

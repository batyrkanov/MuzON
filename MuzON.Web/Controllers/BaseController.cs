using MuzON.BLL.Interfaces;
using MuzON.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MuzON.Web.Controllers
{
    public class BaseController : Controller
    {
        public IArtistService artistService;
        public ICountryService countryService;
        public IUserService userService;
        public IBandService bandService;
        public ISongService songService;
        public Utility.Util util = new Utility.Util();

        // Artists and Bands controller constructor
        public BaseController(IBandService bandServ,
                              ICountryService countryServ, 
                              IArtistService artistServ,
                              ISongService songServ)
        {
            artistService = artistServ;
            bandService = bandServ;
            countryService = countryServ;
            songService = songServ;
        }

        // Home controller constructor
        public BaseController(IBandService bandServ,
                              ICountryService countryServ,
                              IArtistService artistServ)
        {
            artistService = artistServ;
            bandService = bandServ;
            countryService = countryServ;
        }

        // Account controller constructor
        public BaseController(
                    IUserService userServ)
        {
            userService = userServ;
        }

        public void SaveSongs(List<HttpPostedFileBase> songs, string Name)
        {
            foreach (var song in songs)
            {
                foreach (var artistSong in songService.GetSongs())
                {
                    if (song.FileName == artistSong.FileName)
                    {
                        if (!Directory.Exists(Server.MapPath($"~/songs/{Name}/{artistSong.Id}")))
                        {
                            Directory.CreateDirectory(Server.MapPath($"~/songs/{Name}/{artistSong.Id}"));
                        }
                        var path = Path.Combine(Server.MapPath($"~/songs/{Name}/{artistSong.Id}"), song.FileName);
                        song.SaveAs(path);
                    }
                }

            }
        }

        public List<BandSongViewModel> AddSongToArtist(List<HttpPostedFileBase> songs, string name)
        {
            List<BandSongViewModel> bandSongs = new List<BandSongViewModel>();
            foreach (var songItem in songs)
            {
                BandSongViewModel bandSong = new BandSongViewModel();
                SongViewModel song = new SongViewModel()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    FileName = songItem.FileName
                };
                ArtistViewModel artist = new ArtistViewModel();
                bandSong.Song = song;
                bandSong.SongId = song.Id;
                bandSong.Artist = artist;
                bandSong.ArtistId = artist.Id;

                bandSongs.Add(bandSong);
            }
            return bandSongs;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    artistService.Dispose();
                    bandService.Dispose();
                    countryService.Dispose();
                }
                catch { }
            }
            base.Dispose(disposing);
        }
    }
}
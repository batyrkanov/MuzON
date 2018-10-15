using MuzON.BLL.DTO;
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

        public void SaveSongs(List<HttpPostedFileBase> songs)
        {
            foreach (var song in songs)
            {
                foreach (var artistSong in songService.GetSongs())
                {
                    if (song.FileName == artistSong.FileName)
                    {
                        if (!Directory.Exists(Server.MapPath($"~/songs/{artistSong.Id}")))
                        {
                            Directory.CreateDirectory(Server.MapPath($"~/songs/{artistSong.Id}"));
                        }
                        var path = Path.Combine(Server.MapPath($"~/songs/{artistSong.Id}"), song.FileName);
                        song.SaveAs(path);
                    }
                }

            }
        }

        //don't used yet
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
                bandSong.SongId = song.Id;
                bandSong.ArtistId = artist.Id;

                bandSongs.Add(bandSong);
            }
            return bandSongs;
        }

        public SongDTO SongToUpdate(SongDTO song)
        {
            song.Name = Request.Form["Song.Name"] != song.Name ? Request.Form["Song.Name"] : song.Name;
            if (Request.Files.Count > 0)
            {
                song.FileName = Request.Files["Songs"].FileName != song.FileName ? Request.Files["Songs"].FileName : song.FileName;
            }
            return song;
            
        }

        public BandSongDTO SetArtistAndBandId(BandSongDTO bandSongDTO)
        {
            if (Request.Form["Artists"] != "")
            {
                bandSongDTO.ArtistId = Guid.Parse(Request.Form["Artists"]);
            }
            if (Request.Form["Bands"] != "")
            {
                bandSongDTO.BandId = Guid.Parse(Request.Form["Bands"]);
            }
            return bandSongDTO;
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
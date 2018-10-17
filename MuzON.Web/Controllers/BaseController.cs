using MuzON.BLL.DTO;
using MuzON.BLL.Interfaces;
using System;
using System.IO;
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

        public void SaveSong(HttpPostedFileBase song, Guid Id)
        {
            if (!Directory.Exists(Server.MapPath($"~/songs/{Id}")))
            {
                Directory.CreateDirectory(Server.MapPath($"~/songs/{Id}"));
            }
            var path = Path.Combine(Server.MapPath($"~/songs/{Id}"), song.FileName);
            song.SaveAs(path);
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
                    songService.Dispose();
                }
                catch { }
            }
            base.Dispose(disposing);
        }
    }
}
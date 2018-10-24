using Microsoft.AspNet.Identity;
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
        public IGenreService genreService;
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

        // Songs controller constructor
        public BaseController(IBandService bandServ,
                              ICountryService countryServ,
                              IArtistService artistServ,
                              ISongService songServ,
                              IGenreService genreServ)
        {
            artistService = artistServ;
            bandService = bandServ;
            countryService = countryServ;
            songService = songServ;
            genreService = genreServ;
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

        public void DeleteSong(Guid Id)
        {
            System.IO.DirectoryInfo path = new System.IO.DirectoryInfo(Server.MapPath($"~/songs/{Id}"));
            if (path.GetFiles().Length != 0)
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(path.ToString());

                //delete file
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                //delete directory
                di.Delete(true);

            }
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
using MuzON.BLL.Interfaces;
using System;
using System.Collections.Generic;
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
        public Utility.Util util = new Utility.Util();

        // Artists and Bands controller constructor
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
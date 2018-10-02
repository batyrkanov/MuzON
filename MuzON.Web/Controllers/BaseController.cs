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
        // Artists controller constructor
        public BaseController(IArtistService artistServ,
                              ICountryService countryServ)
        {
            artistService = artistServ;
            countryService = countryServ;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    artistService.Dispose();
                }
                catch { }
            }
            base.Dispose(disposing);
        }
    }
}
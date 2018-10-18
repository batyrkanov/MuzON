using AutoMapper;
using MuzON.BLL.Interfaces;
using MuzON.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MuzON.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IBandService artistServ,
                                ICountryService countryServ,
                                IArtistService bandServ)
            : base(artistServ, countryServ, bandServ) { }

        public ActionResult Index()
        {
            var artistDTOs = artistService.GetArtists();

            ViewBag.Artists = Mapper.Map<IEnumerable<ArtistViewModel>>(artistDTOs);

            var bandDTOs = bandService.GetBands();
            ViewBag.Bands = Mapper.Map<IEnumerable<BandViewModel>>(bandDTOs);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
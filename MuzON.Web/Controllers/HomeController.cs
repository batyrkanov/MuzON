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

        public ActionResult MoreAboutArtist(Guid id)
        {
            var artistDTO = artistService.GetArtistById(id);
            var artist = Mapper.Map<ArtistViewModel>(artistDTO);
            return PartialView("_PartialArtist", artist);
        }

        public ActionResult MoreAboutBand(Guid id)
        {
            var bandDTO = bandService.GetBandById(id);
            var band = Mapper.Map<BandViewModel>(bandDTO);
            return PartialView("_PartialBand", band);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult AboutArtist(Guid id)
        {
            var artistDto = artistService.GetArtistById(id);
            var artist = Mapper.Map<ArtistViewModel>(artistDto);
            return View(artist);
        }

        public ActionResult AboutBand(Guid id)
        {
            var bandDto = bandService.GetBandById(id);
            var band = Mapper.Map<BandViewModel>(bandDto);
            return View(band);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
using AutoMapper;
using MuzON.BLL.DTO;
using MuzON.BLL.Interfaces;
using MuzON.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MuzON.Web.Controllers
{
    public class ArtistsController : BaseController
    {
        public ArtistsController(IArtistService artistServ, ICountryService countryServ) 
            : base(artistServ, countryServ) { }

        // GET: Artists
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult GetList()
        {
            var artistDTOs = artistService.GetArtists();
            var artists = Mapper.Map<IEnumerable<ArtistViewModel>>(artistDTOs);
            
            return Json(new { data = artists }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            var countryDTOs = countryService.GetCountries();
            var countries = Mapper.Map<IEnumerable<CountryDTO>, IEnumerable<CountryViewModel>>(countryDTOs);
            ViewBag.CountryId = new SelectList(countries, "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(ArtistViewModel artistViewModel, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                var artistDTO = Mapper.Map<ArtistViewModel, ArtistDTO>(artistViewModel);
                artistDTO.Id = Guid.NewGuid();
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    artistDTO.Image = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                artistService.AddArtist(artistDTO);
                return RedirectToAction("Index");
            }
            var countryDTOs = countryService.GetCountries();
            var countries = Mapper.Map<IEnumerable<CountryDTO>, IEnumerable<CountryViewModel>>(countryDTOs);
            ViewBag.CountryId = new SelectList(countries, "Id", "Name");
            return View(artistViewModel);
        }
    }
}
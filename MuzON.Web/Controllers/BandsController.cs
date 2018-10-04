using AutoMapper;
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
    public class BandsController : BaseController
    {
        public BandsController(IBandService artistServ, 
                                ICountryService countryServ, 
                                IArtistService bandServ)
            : base(artistServ, countryServ, bandServ) { }

        // GET: Bands
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult GetList()
        {
            var bandsDTO = bandService.GetBands();
            var bands = Mapper.Map<IEnumerable<BandIndexViewModel>>(bandsDTO);

            foreach (var item in bands)
            {
                item.CountryName = bandService.GetBandById(item.Id).Country.Name;
            }

            return Json(new { data = bands }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Details(Guid id)
        {
            var bandDTO = bandService.GetBandById(id);
            var band = Mapper.Map<BandViewModel>(bandDTO);
            var countryDTO = countryService.GetCountryById(band.CountryId);
            ViewBag.Country = Mapper.Map<CountryViewModel>(countryDTO).Name;
            ViewBag.Artists = band.Artists;
            if (Request.IsAjaxRequest())
                return PartialView("_DetailsPartial", band);

            return PartialView("_DetailsPartial", band);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            var artistsDTO = from a in artistService.GetArtists()
                          select a;
            var artists = Mapper.Map<IEnumerable<ArtistViewModel>>(artistsDTO);
            ViewBag.Artists = new MultiSelectList(artists, "Id", "FullName");
            var model = new BandViewModel();
            model.CreatedDate = DateTime.Today;
            var countryDTOs = countryService.GetCountries();
            var countries = Mapper.Map<IEnumerable<CountryDTO>, IEnumerable<CountryViewModel>>(countryDTOs);
            ViewBag.CountryId = new SelectList(countries, "Id", "Name");
            return View("_CreatePartial", model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BandViewModel bandViewModel, Guid[] SelectedArtists, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                var bandDTO = Mapper.Map<BandDTO>(bandViewModel);
                bandDTO.Id = Guid.NewGuid();
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    bandDTO.Image = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                bandService.AddBand(bandDTO, SelectedArtists);
                return RedirectToAction("Index");
            }
            var countryDTOs = countryService.GetCountries();
            var countries = Mapper.Map<IEnumerable<CountryDTO>, IEnumerable<CountryViewModel>>(countryDTOs);
            ViewBag.CountryId = new SelectList(countries, "Id", "Name");
            return View("_CreatePartial", bandViewModel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(Guid id)
        {
            var band = bandService.GetBandById(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            TempData["Artists"] = band.Artists;
            BandViewModel bandViewModel = Mapper.Map<BandViewModel>(band);
            

            var countryDTOs = countryService.GetCountries();
            var countries = Mapper.Map<IEnumerable<CountryDTO>, IEnumerable<CountryViewModel>>(countryDTOs);
            ViewBag.CountryId = new SelectList(countries, "Id", "Name", bandViewModel.CountryId);

            var artistsDTO = from a in artistService.GetArtists()
                             select a;
            var artists = Mapper.Map<IEnumerable<ArtistViewModel>>(artistsDTO);
            ViewBag.Artists = new MultiSelectList(artists, "Id", "FullName");
            if (Request.IsAjaxRequest())
                return PartialView("_EditPartial", bandViewModel);
            return View("_EditPartial", bandViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BandViewModel bandViewModel, Guid[] SelectedArtists, HttpPostedFileBase uploadImage, string image)
        {

            if (ModelState.IsValid)
            {
                var bandDTO = Mapper.Map<BandViewModel, BandDTO>(bandViewModel);
                bandDTO.Artists = TempData["Artists"] as List<ArtistDTO>;
                if (uploadImage != null)
                {
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        bandDTO.Image = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                }
                else
                    bandDTO.Image = Convert.FromBase64String(image);
                bandService.UpdateBand(bandDTO, SelectedArtists);
                return RedirectToAction("Index");
            }
            var countryDTOs = countryService.GetCountries();
            var countries = Mapper.Map<IEnumerable<CountryDTO>, IEnumerable<CountryViewModel>>(countryDTOs);
            ViewBag.CountryId = new SelectList(countries, "Id", "Name", bandViewModel.CountryId);
            return PartialView("_EditPartial", bandViewModel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(Guid id)
        {
            var band = bandService.GetBandById(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            BandViewModel bandViewModel = Mapper.Map<BandViewModel>(band);
            if (Request.IsAjaxRequest())
                return PartialView("_DeletePartial", bandViewModel);
            return View("_DeletePartial", bandViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var bandDTO = bandService.GetBandById(id);
            bandService.DeleteBand(bandDTO);
            if (Request.IsAjaxRequest())
                return View("Index");
            return RedirectToAction("Index");
        }
    }
}
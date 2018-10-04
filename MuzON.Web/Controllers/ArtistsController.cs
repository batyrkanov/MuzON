using AutoMapper;
using MuzON.BLL.DTO;
using MuzON.BLL.Interfaces;
using MuzON.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult Details(Guid id)
        {
            var artistDTO = artistService.GetArtistById(id);
            var artist = Mapper.Map<ArtistViewModel>(artistDTO);

            if (Request.IsAjaxRequest())
                return PartialView("_DetailsPartial", artist);

            return PartialView("_DetailsPartial",artist);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            var model = new ArtistViewModel();
            model.BirthDate = DateTime.Today;
            var countryDTOs = countryService.GetCountries();
            var countries = Mapper.Map<IEnumerable<CountryDTO>, IEnumerable<CountryViewModel>>(countryDTOs);
            ViewBag.CountryId = new SelectList(countries, "Id", "Name");
            return View("_CreatePartial", model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
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
            return View("_CreatePartial", artistViewModel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(Guid id)
        {
            var artist = artistService.GetArtistById(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            ArtistViewModel artistViewModel = Mapper.Map<ArtistViewModel>(artist);
            var countryDTOs = countryService.GetCountries();
            var countries = Mapper.Map<IEnumerable<CountryDTO>, IEnumerable<CountryViewModel>>(countryDTOs);
            ViewBag.CountryId = new SelectList(countries, "Id", "Name", artistViewModel.CountryId);
            if (Request.IsAjaxRequest())
                return PartialView("_EditPartial", artistViewModel);
            return View("_EditPartial", artistViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArtistViewModel artistViewModel, HttpPostedFileBase uploadImage, string image)
        {

            if (ModelState.IsValid)
            {
                var artistDTO = Mapper.Map<ArtistViewModel, ArtistDTO>(artistViewModel);
                if (uploadImage != null)
                {
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        artistDTO.Image = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                }
                else
                    artistDTO.Image = Convert.FromBase64String(image);
                artistService.UpdateArtist(artistDTO);
                return RedirectToAction("Index");
            }
            var countryDTOs = countryService.GetCountries();
            var countries = Mapper.Map<IEnumerable<CountryDTO>, IEnumerable<CountryViewModel>>(countryDTOs);
            ViewBag.CountryId = new SelectList(countries, "Id", "Name", artistViewModel.CountryId);
            return PartialView("_EditPartial", artistViewModel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(Guid id)
        {
            var artist = artistService.GetArtistById(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            ArtistViewModel artistViewModel = Mapper.Map<ArtistViewModel>(artist);
            if (Request.IsAjaxRequest())
                return PartialView("_DeletePartial", artistViewModel);
            return View("_DeletePartial", artistViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var artistDTO = artistService.GetArtistById(id);
            artistService.DeleteArtist(artistDTO);
            if (Request.IsAjaxRequest())
                return View("Index");
            return RedirectToAction("Index");
        }
    }
}
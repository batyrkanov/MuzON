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
    public class ArtistsController : BaseController
    {
        public ArtistsController(IArtistService artistServ, 
                                ICountryService countryServ,
                                IBandService bandServ)
            : base(bandServ, countryServ, artistServ) { }

        // GET: Artists
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult GetList()
        {
            var artistDTOs = artistService.GetArtists();
            var artists = Mapper.Map<IEnumerable<ArtistIndexViewModel>>(artistDTOs);

            foreach (var item in artists)
            {
                item.CountryName = artistService.GetArtistById(item.Id).Country.Name;
            }

            return Json(new { data = artists }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Details(Guid id)
        {
            var artistDTO = artistService.GetArtistById(id);
            var artist = Mapper.Map<ArtistDetailsViewModel>(artistDTO);
           
            return PartialView("_DetailsPartial",artist);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            var model = new ArtistViewModel();
            model.BirthDate = DateTime.Today;

            ViewBag.CountryId = util.GetSelectListItems<CountryDTO, CountryViewModel>(countryService.GetCountries());
            ViewBag.Bands = util.GetMultiSelectListItems<BandDTO, BandViewModel>(bandService.GetBands());

            // viewbag for post
            ViewBag.Action = "create";
            return PartialView("_CreateAndEditPartial", model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArtistViewModel artistViewModel, HttpPostedFileBase uploadImage, Guid[] SelectedBands)
        {
            if (ModelState.IsValid)
            {
                var artistDTO = Mapper.Map<ArtistViewModel, ArtistDTO>(artistViewModel);
                artistDTO.Id = Guid.NewGuid();
                artistDTO.Image = util.SetImage(uploadImage, artistDTO.Image);
                artistDTO.Country = countryService.GetCountryById(artistViewModel.CountryId);
                artistService.AddArtist(artistDTO, SelectedBands);
                return RedirectToAction("Index");
            }
            ViewBag.Bands = util.GetMultiSelectListItems<BandDTO, BandViewModel>(bandService.GetBands());
            ViewBag.CountryId = util.GetSelectListItems<CountryDTO, CountryViewModel>(countryService.GetCountries());
            return PartialView("_CreateAndEditPartial", artistViewModel);
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
            ViewBag.CountryId = util.GetSelectListItems<CountryDTO, CountryViewModel>(countryService.GetCountries(), artistViewModel.CountryId);
            if (artist.Bands != null)
            {
                artistViewModel.SelectedBands = new List<Guid>();
                foreach (var item in artist.Bands)
                {
                    artistViewModel.SelectedBands.Add(item.Id);
                }
            }
            ViewBag.Bands = util.GetMultiSelectListItems<BandDTO, BandViewModel>(bandService.GetBands(), artistViewModel.SelectedBands);

            // viewbag for post
            ViewBag.Action = "edit";
            return PartialView("_CreateAndEditPartial", artistViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArtistViewModel artistViewModel, HttpPostedFileBase uploadImage, string image, Guid[] selectedBands)
        {

            if (ModelState.IsValid)
            {
                var artistDTO = Mapper.Map<ArtistViewModel, ArtistDTO>(artistViewModel);
                artistDTO.Image = util.SetImage(uploadImage, artistDTO.Image, image);
                artistDTO.Country = countryService.GetCountryById(artistViewModel.CountryId);
                artistService.UpdateArtist(artistDTO, selectedBands);
                return RedirectToAction("Index");
            }
            ViewBag.Bands = util.GetMultiSelectListItems<BandDTO, BandViewModel>(bandService.GetBands(), artistViewModel.SelectedBands);

            ViewBag.CountryId = util.GetSelectListItems<CountryDTO, CountryViewModel>(countryService.GetCountries(), artistViewModel.CountryId);
            return PartialView("_CreateAndEditPartial", artistViewModel);
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
            return PartialView("_DeletePartial", artistViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var artistDTO = artistService.GetArtistById(id);
            artistService.DeleteArtist(artistDTO);
            return RedirectToAction("Index");
        }
    }
}
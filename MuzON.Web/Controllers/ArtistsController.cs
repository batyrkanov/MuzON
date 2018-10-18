using AutoMapper;
using MuzON.BLL.DTO;
using MuzON.BLL.Interfaces;
using MuzON.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace MuzON.Web.Controllers
{
    public class ArtistsController : BaseController
    {
        public ArtistsController(IArtistService artistServ,
                                ICountryService countryServ,
                                IBandService bandServ,
                                ISongService songServ)
            : base(bandServ, countryServ, artistServ, songServ) { }

        [Authorize(Roles = "admin")]
        // GET: Artists
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public JsonResult GetList()
        {
            var artists = Mapper.Map<IEnumerable<ArtistViewModel>>(
                                        artistService.GetArtists());
            foreach (var artist in artists)
            {
                artist.Bands = null;
            }
            return Json(new { data = artists },JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Details(Guid id)
        {
            var artistDTO = artistService.GetArtistById(id);
            var artist = Mapper.Map<ArtistViewModel>(artistDTO);

            return PartialView("_DetailsPartial", artist);
        }

        public ActionResult MoreAboutArtist(Guid id)
        {
            var artistDTO = artistService.GetArtistById(id);
            var artist = Mapper.Map<ArtistViewModel>(artistDTO);
            return PartialView("_Partial", artist);
        }


        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            var model = new ArtistViewModel
            {
                BirthDate = DateTime.Today
            };

            ViewBag.CountryId = util.GetSelectListItems<CountryDTO, CountryViewModel>(countryService.GetCountries());
            ViewBag.Bands = util.GetMultiSelectListItems<BandDTO, BandViewModel>(bandService.GetBands());

            // viewbag for post
            ViewBag.Action = "create";
            return PartialView("_CreateAndEditPartial", model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public JsonResult Create(ArtistViewModel artistViewModel)
        {
            if (ModelState.IsValid)
            {
                List<HttpPostedFileBase> songs = util.GetSongsFromRequest(Request.Files);
                var artistDTO = Mapper.Map<ArtistViewModel, ArtistDTO>(artistViewModel);
                artistDTO.Id = Guid.NewGuid();
                artistDTO.Image = util.SetImage(Request.Files["uploadImage"], artistDTO.Image);
                artistService.AddArtist(artistDTO);
                return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
            }
            ViewBag.Bands = util.GetMultiSelectListItems<BandDTO, BandViewModel>(bandService.GetBands());
            ViewBag.CountryId = util.GetSelectListItems<CountryDTO, CountryViewModel>(countryService.GetCountries());
            return Json(new { artistViewModel, errorMessage = util.GetErrorList(ModelState.Values) }, JsonRequestBehavior.AllowGet);
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
            artistViewModel.SelectedBands = artistService.GetSelectedBands(artistViewModel.Id);
            ViewBag.CountryId = util.GetSelectListItems<CountryDTO, CountryViewModel>(countryService.GetCountries(), artistViewModel.CountryId);
           
            ViewBag.Bands = util.GetMultiSelectListItems<BandDTO, BandViewModel>(bandService.GetBands(), artistViewModel.SelectedBands);

            // viewbag for post
            ViewBag.Action = "edit";
            return PartialView("_CreateAndEditPartial", artistViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(ArtistViewModel artistViewModel, HttpPostedFileBase uploadImage, string image, Guid[] selectedBands)
        {

            if (ModelState.IsValid)
            {
                var artistDTO = Mapper.Map<ArtistViewModel, ArtistDTO>(artistViewModel);
                artistDTO.Image = util.SetImage(uploadImage, artistDTO.Image, image);
                artistDTO.Country = countryService.GetCountryById(artistViewModel.CountryId);
                artistService.UpdateArtist(artistDTO, selectedBands);
                return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
            }
           
            ViewBag.Bands = util.GetMultiSelectListItems<BandDTO, BandViewModel>(bandService.GetBands(), artistViewModel.SelectedBands);

            ViewBag.CountryId = util.GetSelectListItems<CountryDTO, CountryViewModel>(countryService.GetCountries(), artistViewModel.CountryId);

            return Json(new { artistViewModel, errorMessage = util.GetErrorList(ModelState.Values) }, JsonRequestBehavior.AllowGet);
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
        public JsonResult DeleteConfirmed(Guid id)
        {
            var artistDTO = artistService.GetArtistById(id);
            artistService.DeleteArtist(artistDTO);
            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }
    }
}
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
                                IArtistService bandServ,
                                ISongService songServ)
            : base(artistServ, countryServ, bandServ, songServ) { }

        // GET: Bands
        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult GetList()
        {
            var bands = Mapper.Map<IEnumerable<BandViewModel>>(
                                                bandService.GetBands());
            foreach (var band in bands)
            {
                band.Artists = null;
            }
            return Json(new { data = bands }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Details(Guid id)
        {
            var bandDTO = bandService.GetBandById(id);
            var band = Mapper.Map<BandViewModel>(bandDTO);
            return PartialView("_DetailsPartial", band);
        }

        public ActionResult MoreAboutBand(Guid id)
        {
            var bandDTO = bandService.GetBandById(id);
            var band = Mapper.Map<BandViewModel>(bandDTO);
            return PartialView("_Partial", band);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            var model = new BandViewModel
            {
                CreatedDate = DateTime.Today
            };

            ViewBag.CountryId = util.GetSelectListItems<CountryDTO, CountryViewModel>(countryService.GetCountries());

            ViewBag.Artists = util.GetMultiSelectListArtists(artistService.GetArtists());
            
            // viewbag for post
            ViewBag.Action = "create";
            return PartialView("_CreateAndEditPartial", model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public JsonResult Create(BandViewModel bandViewModel, Guid[] SelectedArtists, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                var bandDTO = Mapper.Map<BandDTO>(bandViewModel);
                bandDTO.Id = Guid.NewGuid();

                bandDTO.Image = util.SetImage(uploadImage, bandDTO.Image);
                bandService.AddBand(bandDTO);
                return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
            }
            ViewBag.Artists = util.GetMultiSelectListArtists(artistService.GetArtists());
            ViewBag.CountryId = util.GetSelectListItems<CountryDTO, CountryViewModel>(countryService.GetCountries());
            return Json(new { bandViewModel, errorMessage = util.GetErrorList(ModelState.Values) }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(Guid id)
        {
            var band = bandService.GetBandById(id);

            if (band == null)
            {
                return HttpNotFound();
            }

            BandViewModel bandViewModel = Mapper.Map<BandViewModel>(band);
            
            if (band.Artists != null)
            {
                bandViewModel.SelectedArtists = new List<Guid>();
                foreach (var item in band.Artists)
                {
                    bandViewModel.SelectedArtists.Add(item.Id);
                }
            }

            ViewBag.Artists = util.GetMultiSelectListArtists(artistService.GetArtists(), bandViewModel.SelectedArtists);
            ViewBag.CountryId = util.GetSelectListItems<CountryDTO, CountryViewModel>(countryService.GetCountries(), bandViewModel.CountryId);

            // viewbag for post
            ViewBag.Action = "edit";
            return PartialView("_CreateAndEditPartial", bandViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(BandViewModel bandViewModel, HttpPostedFileBase uploadImage, string image)
        {

            if (ModelState.IsValid)
            {
                var bandDTO = Mapper.Map<BandViewModel, BandDTO>(bandViewModel);
                bandDTO.Image = util.SetImage(uploadImage, bandDTO.Image, image);
                bandService.UpdateBand(bandDTO);
                return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
            }
            ViewBag.Artists = util.GetMultiSelectListArtists(artistService.GetArtists(), bandViewModel.SelectedArtists);
            ViewBag.CountryId = util.GetSelectListItems<CountryDTO, CountryViewModel>(countryService.GetCountries(), bandViewModel.CountryId);
            return Json(new { bandViewModel, errorMessage = util.GetErrorList(ModelState.Values) }, JsonRequestBehavior.AllowGet);
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
            return PartialView("_DeletePartial", bandViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(Guid id)
        {
            var bandDTO = bandService.GetBandById(id);
            bandService.DeleteBand(bandDTO);
            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }
    }
}
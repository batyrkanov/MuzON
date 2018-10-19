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
            model.Artists = GetAllArtists();
            
            // viewbag for post
            ViewBag.Action = "create";
            return PartialView("_CreateAndEditPartial", model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public JsonResult Create(BandViewModel bandViewModel)
        {
            if (ModelState.IsValid)
            {
                var bandDTO = Mapper.Map<BandDTO>(bandViewModel);
                bandDTO.Id = Guid.NewGuid();

                bandDTO.Image = util.SetImage(Request.Files["uploadImage"], bandDTO.Image);
                bandService.AddBand(bandDTO);
                return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
            }
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
            bandViewModel.Artists = GetAllArtists(band.Artists.Select(x=>x.Id));
            ViewBag.CountryId = util.GetSelectListItems<CountryDTO, CountryViewModel>(countryService.GetCountries(), bandViewModel.CountryId);

            // viewbag for post
            ViewBag.Action = "edit";
            return PartialView("_CreateAndEditPartial", bandViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(BandViewModel bandViewModel)
        {

            if (ModelState.IsValid)
            {
                var bandDTO = Mapper.Map<BandViewModel, BandDTO>(bandViewModel);
                bandDTO.Image = util.SetImage(Request.Files["uploadImage"], bandDTO.Image, Request.Form["image"]);
                bandService.UpdateBand(bandDTO);
                return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
            }
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

        protected List<ArtistViewModel> GetAllArtists(IEnumerable<Guid> selectedIds = null)
        {
            IEnumerable<ArtistDTO> artistDTOs = artistService.GetArtists();

            IEnumerable<ArtistViewModel> artistModels = Mapper.Map<IEnumerable<ArtistViewModel>>(artistDTOs);

            if (selectedIds != null)
            {
                foreach (ArtistViewModel model in artistModels)
                {
                    model.IsSelected = selectedIds.Contains(model.Id);
                }
            }

            return artistModels.OrderBy(x => x.FullName).ToList();
        }
    }
}
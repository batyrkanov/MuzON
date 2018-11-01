using AutoMapper;
using MuzON.BLL.DTO;
using MuzON.BLL.Interfaces;
using MuzON.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MuzON.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class ArtistsController : BaseController
    {
        public ArtistsController(IArtistService artistServ,
                                ICountryService countryServ,
                                IBandService bandServ,
                                ISongService songServ)
            : base(bandServ, countryServ, artistServ, songServ) { }

        
        // GET: Artists
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetList()
        {
            var artists = Mapper.Map<IEnumerable<ArtistViewModel>>(
                                        artistService.GetArtists());
            foreach (var artist in artists)
            {
                artist.Bands = null;
                artist.Songs = null;
            }
            return Json(new { data = artists },JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(Guid id)
        {
            var artistDTO = artistService.GetArtistById(id);
            var artist = Mapper.Map<ArtistViewModel>(artistDTO);

            return PartialView("_DetailsPartial", artist);
        }

        public ActionResult Create()
        {
            var model = new ArtistViewModel
            {
                BirthDate = DateTime.Today
            };

            ViewBag.CountryId = util.GetSelectListItems<CountryDTO, CountryViewModel>(countryService.GetCountries());
            model.Bands = GetAllBands();

            // viewbag for post
            ViewBag.Action = "create";
            return PartialView("_CreateAndEditPartial", model);
        }

        [HttpPost]        
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
                logger.InfoLog("Artist",
                            "created",
                            artistDTO.FullName,
                            artistDTO.Id,
                            User.Identity.Name);
                return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
            }
            ViewBag.CountryId = util.GetSelectListItems<CountryDTO, CountryViewModel>(countryService.GetCountries());
            return Json(new { artistViewModel, errorMessage = util.GetErrorList(ModelState.Values) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            var artist = artistService.GetArtistById(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            ArtistViewModel artistViewModel = Mapper.Map<ArtistViewModel>(artist);
            artistViewModel.Bands = GetAllBands(artist.Bands.Select(x => x.Id));
            ViewBag.CountryId = util.GetSelectListItems<CountryDTO, CountryViewModel>(countryService.GetCountries(), artistViewModel.CountryId);
           
            // viewbag for post
            ViewBag.Action = "edit";
            return PartialView("_CreateAndEditPartial", artistViewModel);
        }

        [HttpPost]        
        [ValidateAntiForgeryToken]
        public JsonResult Edit(ArtistViewModel artistViewModel)
        {

            if (ModelState.IsValid)
            {
                var artistDTO = Mapper.Map<ArtistViewModel, ArtistDTO>(artistViewModel);
                artistDTO.Image = util.SetImage(Request.Files["uploadImage"], artistDTO.Image, Request.Form["image"]);
                artistService.UpdateArtist(artistDTO);
                logger.InfoLog("Artist",
                            "edited",
                            artistDTO.FullName,
                            artistDTO.Id,
                            User.Identity.Name);
                return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
            }
            ViewBag.CountryId = util.GetSelectListItems<CountryDTO, CountryViewModel>(countryService.GetCountries(), artistViewModel.CountryId);

            return Json(new { artistViewModel, errorMessage = util.GetErrorList(ModelState.Values) }, JsonRequestBehavior.AllowGet);
        }

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
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(Guid id)
        {
            var artistDTO = artistService.GetArtistById(id);
            artistService.DeleteArtist(artistDTO);
            logger.InfoLog("Artist",
                            "deleted",
                            artistDTO.FullName,
                            artistDTO.Id,
                            User.Identity.Name);
            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }

        protected List<BandViewModel> GetAllBands(IEnumerable<Guid> selectedIds = null)
        {
            IEnumerable<BandDTO> bandDTOs = bandService.GetBands();

            IEnumerable<BandViewModel> bandModels = Mapper.Map<IEnumerable<BandViewModel>>(bandDTOs);

            if (selectedIds != null)
            {
                foreach (BandViewModel model in bandModels)
                {
                    model.IsSelected = selectedIds.Contains(model.Id);
                }
            }

            return bandModels.OrderBy(x => x.Name).ToList();
        }
    }
}
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

            var countryDTOs = countryService.GetCountries();
            var countries = Mapper.Map<IEnumerable<CountryDTO>, IEnumerable<CountryViewModel>>(countryDTOs);
            ViewBag.CountryId = new SelectList(countries, "Id", "Name");
            
            var bandsDTO = bandService.GetBands();
            var bands = Mapper.Map<IEnumerable<BandViewModel>>(bandsDTO);
            ViewBag.Bands = new MultiSelectList(bands, "Id", "Name");

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
                if(uploadImage != null)
                {
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        artistDTO.Image = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                }
                artistDTO.Country = countryService.GetCountryById(artistViewModel.CountryId);
                artistService.AddArtist(artistDTO, SelectedBands);
                return RedirectToAction("Index");
            }
            var bandsDTO = bandService.GetBands();
            var bands = Mapper.Map<IEnumerable<BandViewModel>>(bandsDTO);
            ViewBag.Bands = new MultiSelectList(bands, "Id", "Name");

            var countryDTOs = countryService.GetCountries();
            var countries = Mapper.Map<IEnumerable<CountryDTO>, IEnumerable<CountryViewModel>>(countryDTOs);
            ViewBag.CountryId = new SelectList(countries, "Id", "Name");

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
            var countryDTOs = countryService.GetCountries();
            var countries = Mapper.Map<IEnumerable<CountryViewModel>>(countryDTOs);
            ViewBag.CountryId = new SelectList(countries, "Id", "Name", artistViewModel.CountryId);
            if(artist.Bands != null)
            {
                artistViewModel.SelectedBands = new List<Guid>();
                foreach (var item in artist.Bands)
                {
                    artistViewModel.SelectedBands.Add(item.Id);
                }
            }
            var bandsDTO = bandService.GetBands();
            var bands = Mapper.Map<IEnumerable<BandViewModel>>(bandsDTO);
            ViewBag.Bands = new MultiSelectList(bands, "Id", "Name");

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
                if (uploadImage != null)
                {
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        artistDTO.Image = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                }
                else
                    artistDTO.Image = Convert.FromBase64String(image);
                artistDTO.Country = countryService.GetCountryById(artistViewModel.CountryId);
                artistService.UpdateArtist(artistDTO, selectedBands);
                return RedirectToAction("Index");
            }
            var bandsDTO = bandService.GetBands();
            var bands = Mapper.Map<IEnumerable<BandViewModel>>(bandsDTO);
            ViewBag.Bands = new MultiSelectList(bands, "Id", "Name");

            var countryDTOs = countryService.GetCountries();
            var countries = Mapper.Map<IEnumerable<CountryDTO>, IEnumerable<CountryViewModel>>(countryDTOs);
            ViewBag.CountryId = new SelectList(countries, "Id", "Name", artistViewModel.CountryId);
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
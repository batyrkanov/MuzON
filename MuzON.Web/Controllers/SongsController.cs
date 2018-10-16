using AutoMapper;
using MuzON.BLL.DTO;
using MuzON.BLL.Interfaces;
using MuzON.Web.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace MuzON.Web.Controllers
{
    public class SongsController : BaseController
    {
        public SongsController(IBandService bandServ,
            ICountryService countryServ,
            IArtistService artistServ,
            ISongService songServ)
            : base(bandServ, countryServ, artistServ, songServ) { }

        // GET: Songs
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetList()
        {
            var songs = Mapper.Map<IEnumerable<SongViewModel>>(songService.GetSongs());

            return Json(new { data = songs }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Details(Guid id)
        {
            var songDTO = songService.GetDetailSong(id);
            var song = Mapper.Map<SongDetailsViewModel>(songDTO);
            return PartialView("_DetailsPartial", song);
        }

        public ActionResult Create()
        {
            var song = new BandSongViewModel();
            ViewBag.Artists = util.GetMultiSelectListArtists(artistService.GetArtists());
            ViewBag.Bands = util.GetMultiSelectListItems<BandDTO, BandViewModel>(bandService.GetBands());
            ViewBag.Action = "create";
            return PartialView("_CreateAndEditPartial", song);
        }

        [HttpPost]
        public JsonResult Create(BandSongViewModel bandSongViewModel, List<Guid> Artists, List<Guid> Bands)
        {
            if (ModelState.IsValid)
            {
                List<HttpPostedFileBase> songs = util.GetSongsFromRequest(Request.Files);
                foreach (var song in songs)
                {
                    var songViewModel = new SongViewModel
                    {
                        Name = Request.Form["Name"],
                        FileName = song.FileName,
                        Id = Guid.NewGuid()
                    };
                    SaveBandSong(bandSongViewModel, Artists, Bands, songViewModel);
                }
                
                SaveSongs(songs);
                return Json(new { data = "success" });
            }
            ViewBag.Artists = util.GetMultiSelectListArtists(artistService.GetArtists());
            ViewBag.Bands = util.GetMultiSelectListItems<BandDTO, BandViewModel>(bandService.GetBands());
            return Json(new { bandSongViewModel, errorMessage = util.GetErrorList(ModelState.Values) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            var song = Mapper.Map<BandSongViewModel>(songService.GetBandSongById(id));

            //ViewBag.Artists = util.GetMultiSelectListArtists(artistService.GetArtists(), song.ArtistId);
            //ViewBag.Bands = util.GetMultiSelectListItems<BandDTO, BandViewModel>(bandService.GetBands(), song.BandId);
            ViewBag.Action = "edit";
            return PartialView("_CreateAndEditPartial", song);
        }

        [HttpPost]
        public JsonResult Edit(BandSongViewModel bandSongViewModel)
        {
            if (ModelState.IsValid)
            {
                var song = songService.GetSongById(bandSongViewModel.SongId);
                List<HttpPostedFileBase> songs = util.GetSongsFromRequest(Request.Files);
                var bandSongDTO = Mapper.Map<BandSongDTO>(bandSongViewModel);
                bandSongDTO.Song = SongToUpdate(song);

                //bandSongDTO = SetArtistAndBandId(bandSongDTO);

                songService.UpdateSong(bandSongDTO);
                SaveSongs(songs);
                return Json(new { data = "success" });
            }
            //ViewBag.Artists = util.GetMultiSelectListArtists(artistService.GetArtists(), bandSongViewModel.ArtistId);
            //ViewBag.Bands = util.GetMultiSelectListItems<BandDTO, BandViewModel>(bandService.GetBands(), bandSongViewModel.BandId);
            return Json(new { bandSongViewModel, errorMessage = util.GetErrorList(ModelState.Values) }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(Guid id)
        {
            var song = songService.GetBandSongById(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            var songViewModel = Mapper.Map<BandSongViewModel>(song);
            return PartialView("_DeletePartial", songViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(Guid id)
        {
            var songDTO = songService.GetBandSongById(id);
            songService.DeleteSong(songDTO.Song);
            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }
    }
}
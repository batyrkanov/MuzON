using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MuzON.BLL.DTO;
using MuzON.BLL.Interfaces;
using MuzON.Web.Models;

namespace MuzON.Web.Controllers
{
    public class SongsController : BaseController
    {
        public SongsController(IBandService bandServ, 
            ICountryService countryServ, 
            IArtistService artistServ, 
            ISongService songServ) 
            : base(bandServ, countryServ, artistServ, songServ)  { }

        // GET: Songs
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetList()
        {
            var songs = Mapper.Map<IEnumerable<SongViewModel>>(songService.GetSongs());
            foreach (var song in songs)
            {
                song.BandSongId = songService.GetBandSongBySongId(song.Id).Id;
            }
            return Json(new { data = songs }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Details(Guid id)
        {
            var bandSongDTO = songService.GetBandSongById(id);
            var bandSong = Mapper.Map<BandSongViewModel>(bandSongDTO);
            bandSong.Artist = Mapper.Map<ArtistViewModel>(artistService.GetArtistById(bandSongDTO.ArtistId.Value));
            return PartialView("_DetailsPartial", bandSong);
        }

        public ActionResult Create()
        {
            var song = new BandSongViewModel();
            ViewBag.Artists = util.GetSelectListArtistItems(artistService.GetArtists());
            ViewBag.Bands = util.GetSelectListItems<BandDTO, BandViewModel>(bandService.GetBands());
            ViewBag.Action = "create";
            return PartialView("_CreateAndEditPartial", song);
        }

        [HttpPost]
        public JsonResult Create(BandSongViewModel bandSongViewModel)
        {
            if (ModelState.IsValid)
            {
                List<HttpPostedFileBase> songs = util.GetSongsFromRequest(Request.Files);
                var song = new SongViewModel
                {
                    Name = Request.Form["Name"],
                    FileName = Request.Files["Songs"].FileName,
                    Id = Guid.NewGuid()
                };
                var bandSongDTO = Mapper.Map<BandSongDTO>(bandSongViewModel);
                bandSongDTO.Id = Guid.NewGuid();
                bandSongDTO.Song = Mapper.Map<SongDTO>(song);
                bandSongDTO.SongId = song.Id;
                bandSongDTO = SetArtistAndBandId(bandSongDTO);
                songService.AddBandSong(bandSongDTO);
                SaveSongs(songs);
                return Json(new { data = "success" });
            }
            return Json(new { bandSongViewModel, errorMessage = util.GetErrorList(ModelState.Values) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            var song = Mapper.Map<BandSongViewModel>(songService.GetBandSongById(id));
            
            ViewBag.Artists = util.GetSelectListArtistItems(artistService.GetArtists(), song.ArtistId);
            ViewBag.Bands = util.GetSelectListItems<BandDTO, BandViewModel>(bandService.GetBands(), song.BandId);
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

                bandSongDTO = SetArtistAndBandId(bandSongDTO);
               
                songService.UpdateSong(bandSongDTO);
                SaveSongs(songs);
                return Json(new { data = "success" });
            }
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
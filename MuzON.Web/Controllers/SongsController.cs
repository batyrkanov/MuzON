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
            return Json(new { data = songs }, JsonRequestBehavior.AllowGet);
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
        public JsonResult Create(BandSongViewModel songViewModel)
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
                var bandSongDTO = Mapper.Map<BandSongDTO>(songViewModel);
                bandSongDTO.Id = Guid.NewGuid();
                bandSongDTO.Song = Mapper.Map<SongDTO>(song);
                bandSongDTO.SongId = song.Id;

                if (Request.Form["Artists"] != "")
                {
                    bandSongDTO.ArtistId = Guid.Parse(Request.Form["Artists"]);
                }
                if(Request.Form["Bands"] != "")
                {
                    bandSongDTO.BandId = Guid.Parse(Request.Form["Bands"]);
                }
                

                songService.AddBandSong(bandSongDTO);
                SaveSongs(songs, "Name");
                return Json(new { data = "success" });
            }
            return Json(new { songViewModel, errorMessage = util.GetErrorList(ModelState.Values) }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(Guid id)
        {
            var song = songService.GetSongById(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            SongViewModel songViewModel = Mapper.Map<SongViewModel>(song);
            return PartialView("_DeletePartial", songViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(Guid id)
        {
            var songDTO = songService.GetSongById(id);
            songService.DeleteSong(songDTO);
            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }
    }
}
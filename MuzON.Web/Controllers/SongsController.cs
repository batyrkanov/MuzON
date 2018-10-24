using AutoMapper;
using MuzON.BLL.DTO;
using MuzON.BLL.Interfaces;
using MuzON.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MuzON.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class SongsController : BaseController
    {
        public SongsController(IBandService bandServ,
            ICountryService countryServ,
            IArtistService artistServ,
            ISongService songServ,
            IGenreService genreServ)
            : base(bandServ, countryServ, artistServ, songServ, genreServ) { }

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

        public ActionResult Details(Guid id)
        {
            var songDTO = songService.GetSongById(id);
            var song = Mapper.Map<SongViewModel>(songDTO);
            song.Artists = GetAllArtists(songDTO.Artists.Select(x => x.Id)).Where(x => x.IsSelected).ToList();
            song.Bands = GetAllBands(songDTO.Bands.Select(x => x.Id)).Where(x => x.IsSelected).ToList();
            song.Genres = GetAllGenres(songDTO.Genres.Select(x => x.Id)).Where(x=>x.IsSelected).ToList();
            return PartialView("_DetailsPartial", song);
        }

        public ActionResult Create()
        {
            var song = new SongViewModel
            {
                Artists = GetAllArtists(),
                Bands = GetAllBands(),
                Genres = GetAllGenres()
            };
            ViewBag.Action = "create";
            return PartialView("_CreateAndEditPartial", song);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(SongViewModel songViewModel)
        {
            if (ModelState.IsValid)
            {
                songViewModel.Artists = GetAllArtists(songViewModel.SelectedArtists).Where(x=>x.IsSelected).ToList();
                songViewModel.Bands = GetAllBands(songViewModel.SelectedBands).Where(x => x.IsSelected).ToList();
                songViewModel.Genres = GetAllGenres(songViewModel.SelectedGenres).Where(x => x.IsSelected).ToList();
                List<HttpPostedFileBase> songs = util.GetSongsFromRequest(Request.Files);
                foreach (var song in songs)
                {
                    songViewModel.Id = Guid.NewGuid();
                    songViewModel.FileName = song.FileName;
                    var songDTO = Mapper.Map<SongDTO>(songViewModel);
                    songService.AddSong(songDTO);
                    SaveSong(song, songDTO.Id);
                }
                return Json(new { data = "success" });
            }
            return Json(new { songViewModel, errorMessage = util.GetErrorList(ModelState.Values) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            var songDTO = songService.GetSongById(id);
            var song = Mapper.Map<SongViewModel>(songDTO);
            song.Artists = GetAllArtists(songDTO.Artists.Select(x => x.Id));
            song.Bands = GetAllBands(songDTO.Bands.Select(x => x.Id));
            song.Genres = GetAllGenres(songDTO.Genres.Select(x => x.Id));
            ViewBag.Action = "edit";
            return PartialView("_CreateAndEditPartial", song);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(SongViewModel songViewModel)
        {
            if (ModelState.IsValid)
            {
                List<HttpPostedFileBase> songs = util.GetSongsFromRequest(Request.Files);
                songViewModel.Artists = GetAllArtists(songViewModel.SelectedArtists).Where(x => x.IsSelected).ToList();
                songViewModel.Bands = GetAllBands(songViewModel.SelectedBands).Where(x => x.IsSelected).ToList();
                songViewModel.Genres = GetAllGenres(songViewModel.SelectedGenres).Where(x => x.IsSelected).ToList();
                var songDTO = Mapper.Map<SongDTO>(songViewModel);
                songService.UpdateSong(songDTO);
                foreach (var song in songs)
                {
                    SaveSong(song, songDTO.Id);
                }
                return Json(new { data = "success" });
            }
            return Json(new { songViewModel, errorMessage = util.GetErrorList(ModelState.Values) }, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Delete(Guid id)
        {
            var song = songService.GetSongById(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            var songViewModel = Mapper.Map<SongViewModel>(song);
            return PartialView("_DeletePartial", songViewModel);
        }

        [HttpPost, ActionName("Delete")]        
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(Guid id)
        {
            var songDTO = songService.GetSongById(id);
            songService.DeleteSong(songDTO);
            DeleteSong(songDTO.Id);
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

        protected List<GenreViewModel> GetAllGenres(IEnumerable<Guid> selectedIds = null)
        {
            IEnumerable<GenreDTO> genreDTOs = genreService.GetGenres();

            IEnumerable<GenreViewModel> genreModels = Mapper.Map<IEnumerable<GenreViewModel>>(genreDTOs);

            if (selectedIds != null)
            {
                foreach (GenreViewModel model in genreModels)
                {
                    model.IsSelected = selectedIds.Contains(model.Id);
                }
            }

            return genreModels.OrderBy(x => x.Name).ToList();
        }
    }
}
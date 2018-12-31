using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Models;
using UserInterface.Filters;
using System.Net.Http;

namespace UserInterface.Controllers
{
    public class LibraryController : AServiceController
    {
        
        public LibraryController(HttpClient client) : base(client)
        {
            // TODO: Using test data. Replace with 
            //  database data
            Random rand = new Random(DateTime.Now.Millisecond);
            if (SongViewModel.Songs.Count == 0)
                for (int i = 1; i <= 10; ++i)
                {
                    new SongViewModel()
                    {
                        Album  = "Album "  + (rand.Next() % 100).ToString(),
                        Artist = "Artist " + (rand.Next() % 100).ToString(),
                        Name   = "Name "   + (rand.Next() % 100).ToString(),
                        Link   = "Link "   + (rand.Next() % 100).ToString()
                    };
                }
        }
        
        public IActionResult Index()
        {
            return View(SongViewModel.Songs);
        }

        [HttpGet]
        public ActionResult AddSong()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSong(SongViewModel formSong)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public ActionResult RemoveSong(int id)
        {
            return View(SongViewModel.GetById(id));
        }
        [HttpPost]
        public ActionResult RemoveSong(SongViewModel songToRemove)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                SongViewModel.Songs.Remove(SongViewModel.GetById(songToRemove.Id));
                SongViewModel.Songs.Remove(songToRemove);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(SongViewModel.GetById(id));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(SongViewModel.GetById(id));
        }
        [HttpPost]
        public ActionResult Edit(SongViewModel formSong)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }
                SongViewModel libSong = SongViewModel.GetById(formSong.Id);
                SongViewModel.Songs.Remove(formSong);

                libSong.Name = formSong.Name;
                libSong.Album = formSong.Album;
                libSong.Artist = formSong.Artist;
                libSong.Link = formSong.Link;

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public ActionResult SortByName()
        {
            SongViewModel.UpdateSongSort(SongViewModel.SortMethod.Name);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public ActionResult SortByArtist()
        {
            SongViewModel.UpdateSongSort(SongViewModel.SortMethod.Artist);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public ActionResult SortByAlbum()
        {
            SongViewModel.UpdateSongSort(SongViewModel.SortMethod.Album);

            return RedirectToAction(nameof(Index));
        }
    }
}
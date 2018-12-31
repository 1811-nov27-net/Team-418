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
            {
                // Actual songs.
                new SongViewModel()
                {
                    Album = "Trench",
                    Artist = "Twenty One Pilots",
                    Name = "Chlorine",
                    PlayTime = new TimeSpan(0, 5, 24),
                    Link = "Wc79sjzjNuo"
                };
                new SongViewModel()
                {
                    Album = "Trench",
                    Artist = "Twenty One Pilots",
                    Name = "Pet Cheetah",
                    PlayTime = new TimeSpan(0, 3, 18),
                    Link = "VGMmSOsNAdc"
                };
                new SongViewModel()
                {
                    Album = "Blurryface",
                    Artist = "Twenty One Pilots",
                    Name = "Ride",
                    PlayTime = new TimeSpan(0, 3, 34),
                    Link = "Pw-0pbY9JeU"
                };
                new SongViewModel()
                {
                    Album = "Blurryface",
                    Artist = "Twenty One Pilots",
                    Name = "Polarize",
                    PlayTime = new TimeSpan(0, 3, 46),
                    Link = "MiPBQJq49xk"
                };

                // Dummy songs
                for (int i = 1; i <= 100; ++i)
                {
                    new SongViewModel()
                    {
                        Album = "Album " + (rand.Next() % 100).ToString(),
                        Artist = "Artist " + (rand.Next() % 100).ToString(),
                        Name = "Name " + (rand.Next() % 100).ToString(),
                        PlayTime = new TimeSpan(0, rand.Next() % 5, rand.Next() % 59),
                        Link = "invalid link."
                    };
                }
            }
        }
        
        public ActionResult SongView(bool playSong = false)
        {
            TempData["VideoPlaying"] = playSong;
            return View(SongViewModel.Songs);
        }

        public ActionResult PlaySong()
        {
            bool playSong = true;
            return RedirectToAction(nameof(SongView), new { playSong });
        }
        

        [HttpGet]
        public ActionResult SortByName()
        {
            SongViewModel.UpdateSongSort(SongViewModel.SortMethod.Name);

            return RedirectToAction(nameof(SongView));
        }
        [HttpGet]
        public ActionResult SortByArtist()
        {
            SongViewModel.UpdateSongSort(SongViewModel.SortMethod.Artist);

            return RedirectToAction(nameof(SongView));
        }
        [HttpGet]
        public ActionResult SortByAlbum()
        {
            SongViewModel.UpdateSongSort(SongViewModel.SortMethod.Album);

            return RedirectToAction(nameof(SongView));
        }
    }
}
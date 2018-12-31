using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Models;
using UserInterface.Filters;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

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
                // Artist
                // name
                // city
                // date formed
                // date of latest release
                // website

                // Album
                // name
                // artist
                // release date
                // genre

                // Requests
                // Artist

                // Actual songs.
                new SongViewModel()
                {
                    Album = "Trench",
                    Artist = "Twenty One Pilots",
                    Name = "Chlorine",
                    PlayTime = new TimeSpan(0, 5, 24),
                    Link = "Wc79sjzjNuo"
                    // genre
                    // release
                    // cover

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
        
        public ActionResult SongView()
        {
            return View(SongViewModel.Songs);
        }

        public async Task<ActionResult> SongView2()
        {
            // send "GET api/Temperature" to service, get headers of response
            HttpRequestMessage request = CreateRequestToService(HttpMethod.Get, "api/song");
            HttpResponseMessage response = await Client.SendAsync(request);

            // (if status code is not 200-299 (for success))
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Library", "SongView");
            }

            // get the whole response body (second await)
            var responseBody = await response.Content.ReadAsStringAsync();

            // this is a string, so it must be deserialized into a C# object.
            // we could use DataContractSerializer, .NET built-in, but it's more awkward
            // than the third-party Json.NET aka Newtonsoft JSON.
            List<SongViewModel> songs = JsonConvert.DeserializeObject<List<SongViewModel>>(responseBody);
            SongViewModel.Songs = songs;
            return RedirectToAction(nameof(SongView));
        }

        public ActionResult PlaySong()
        {
            return RedirectToAction(nameof(SongView));
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
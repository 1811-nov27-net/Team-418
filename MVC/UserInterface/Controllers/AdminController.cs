using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserInterface.Models;

namespace UserInterface.Controllers
{
    public class AdminController : AServiceController
    {
        public AdminController(HttpClient client) : base(client)
        {
        }

        [HttpGet]
        public ActionResult CreateSong(int id)
        {
            PendingSongViewModel pendingSong = PendingSongViewModel.GetById(id);
            CreateSongViewModel song = new CreateSongViewModel
            {
                Name = pendingSong.Name,
                Artist = pendingSong.Artist,
                Album = pendingSong.Album
            };

            return View(song);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSong(CreateSongViewModel songForm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return RedirectToAction("SongIndex", "Song");
                /*
                {
                    "name": "S",
        "artist": "K/DA",
        "album": "",
        "playTime": null,
        "link": "UOxkGD8qRB4",
        "genre": "KPOP",
        "release": null,
        "cover": false
    }
    */
                var obj = new
                {
                    name = songForm.Name,
                    artist = songForm.Artist,
                    album = songForm.Album,
                    playTime = songForm.Length,
                    link = songForm.Link,
                    genre = songForm.Genre,
                    release = songForm.ReleaseDate,
                    cover = songForm.Cover
                };
                var s = JsonConvert.SerializeObject(obj);
                HttpRequestMessage request = CreateRequestToService(HttpMethod.Post, "api/song", obj);
                HttpResponseMessage response = await Client.SendAsync(request);
                    
                // (if status code is not 200-299 (for success))
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SongIndex", "Song");
                }

                // Song successfully added
                // Add new SongViewModel to front-end
                new SongViewModel
                {
                    Name = songForm.Name,
                    Artist = songForm.Artist,
                    Album = songForm.Album,
                    PlayTime = songForm.Length,
                    Link = songForm.Link,
                    Genre = songForm.Genre,
                    ReleaseDate = songForm.ReleaseDate,
                    Cover = songForm.Cover
                };


                // Remove pending song
                request = AServiceController.CreateRequestToServiceNoCookie(HttpMethod.Delete, "https://localhost:44376/api/request", 
                    new { songName = songForm.Name, artistName = songForm.Artist });

                response = await Client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    // Error. 
                    return RedirectToAction("SongIndex", "Song");
                }

                await PendingSongViewModel.SyncPendingSongsAsync(Client);
                          

                return RedirectToAction("SongIndex", "Song");
            }
            catch (Exception)
            {
                return RedirectToAction("SongIndex", "Song");
            }
        }

    }
}
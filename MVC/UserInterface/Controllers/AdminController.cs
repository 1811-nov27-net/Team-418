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
                if(!ModelState.IsValid)
                    return RedirectToAction("Song", "SongIndex");
                
                HttpRequestMessage request = CreateRequestToService(HttpMethod.Post, "api/song");
                HttpResponseMessage response = await Client.SendAsync(request);

                // (if status code is not 200-299 (for success))
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Song", "SongIndex");
                }
                
                // Song successfully added
                // TODO: Resync database here?

                return RedirectToAction("Song", "SongIndex");
            }
            catch (Exception)
            {
                return RedirectToAction("Song", "SongIndex");
            }
        }

    }
}
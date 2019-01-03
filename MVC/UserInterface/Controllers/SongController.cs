using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Models;

namespace UserInterface.Controllers
{
    public class SongController : AServiceController
    {
        public SongController(HttpClient client) : base(client)
        {
        }

        [HttpGet]
        public async Task<ActionResult> SongIndex()
        {
            await SongViewModel.SyncSongsAsync(Client);

            return View(SongViewModel.Songs);
        }
        [HttpGet]
        public ActionResult PendingSongIndex()
        {
            return View(PendingSongViewModel.PendingSongs);
        }

        [HttpGet]
        public ActionResult RequestSong()
        {
            return View();
        }

        //TODO: Replace with RequestSongDB
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestSongold(PendingSongViewModel songForm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return RedirectToAction("SongIndex", "Song");

                PendingSongViewModel.PendingSongs.Add(songForm);

                return RedirectToAction("PendingSongIndex", "Song");
            }
            catch (Exception)
            {
                return RedirectToAction("SongIndex", "Song");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RequestSong(PendingSongViewModel song)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Song", "SongIndex");
                }

                PendingSongViewModel.PendingSongs.Add(song);

                // use POST method, not GET, based on the route the service has defined
                {
    }
                HttpRequestMessage request = CreateRequestToService(HttpMethod.Post, "api/request", 
                    new { artistname = song.Artist, albumname = song.Album, songname = song.Name });
                HttpResponseMessage response = await Client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SongIndex", "Song");
                }

                return RedirectToAction("SongIndex", "Song");

            }
            catch
            {
                return RedirectToAction("SongIndex", "Song");
            }
        }

    }
}
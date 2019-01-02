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
            if (SongViewModel.Songs.Count == 0)
            {
                ArtistViewModel artist = new ArtistViewModel
                {
                    Name = "Twenty One Pilots"
                };
                AlbumViewModel trench = new AlbumViewModel
                {
                    Name = "Trench"
                };
                AlbumViewModel blurryface = new AlbumViewModel
                {
                    Name = "Blurryface"
                };
                trench.Artist = artist.Name;
                blurryface.Artist = artist.Name;
                artist.Albums.Add(trench);
                artist.Albums.Add(blurryface);

                // Actual songs.
                SongViewModel song = new SongViewModel()
                {
                    Album = trench.Name,
                    Artist = artist.Name,
                    Name = "Chlorine",
                    PlayTime = new TimeSpan(0, 5, 24),
                    Link = "Wc79sjzjNuo"

                };
                trench.Songs.Add(song);
                song = new SongViewModel()
                {
                    Album = trench.Name,
                    Artist = artist.Name,
                    Name = "Pet Cheetah",
                    PlayTime = new TimeSpan(0, 3, 18),
                    Link = "VGMmSOsNAdc"
                };
                trench.Songs.Add(song);
                song = new SongViewModel()
                {
                    Album = blurryface.Name,
                    Artist = artist.Name,
                    Name = "Ride",
                    PlayTime = new TimeSpan(0, 3, 34),
                    Link = "Pw-0pbY9JeU"
                };
                blurryface.Songs.Add(song);
                song = new SongViewModel()
                {
                    Album = blurryface.Name,
                    Artist = artist.Name,
                    Name = "Polarize",
                    PlayTime = new TimeSpan(0, 3, 46),
                    Link = "MiPBQJq49xk"
                };
                blurryface.Songs.Add(song);
            }
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
        public ActionResult RequestSong(PendingSongViewModel songForm)
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
        public async Task<ActionResult> RequestSongDB(PendingSongViewModel song)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Song", "SongIndex");
                }


                // use POST method, not GET, based on the route the service has defined
                HttpRequestMessage request = CreateRequestToService(HttpMethod.Post, "api/request", song);
                HttpResponseMessage response = await Client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Song", "SongIndex");
                }

                return RedirectToAction("Song", "SongIndex");

            }
            catch
            {
                return RedirectToAction("Song", "SongIndex");
            }
        }

    }
}
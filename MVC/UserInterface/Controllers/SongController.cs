using System;
using System.Collections.Generic;
using System.Linq;
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
                trench.Artist = artist;
                blurryface.Artist = artist;
                artist.Albums.Add(trench);
                artist.Albums.Add(blurryface);

                // Actual songs.
                SongViewModel song = new SongViewModel()
                {
                    Album = trench,
                    Artist = artist,
                    Name = "Chlorine",
                    PlayTime = new TimeSpan(0, 5, 24),
                    Link = "Wc79sjzjNuo"

                };
                trench.Songs.Add(song);
                song = new SongViewModel()
                {
                    Album = trench,
                    Artist = artist,
                    Name = "Pet Cheetah",
                    PlayTime = new TimeSpan(0, 3, 18),
                    Link = "VGMmSOsNAdc"
                };
                trench.Songs.Add(song);
                song = new SongViewModel()
                {
                    Album = blurryface,
                    Artist = artist,
                    Name = "Ride",
                    PlayTime = new TimeSpan(0, 3, 34),
                    Link = "Pw-0pbY9JeU"
                };
                blurryface.Songs.Add(song);
                song = new SongViewModel()
                {
                    Album = blurryface,
                    Artist = artist,
                    Name = "Polarize",
                    PlayTime = new TimeSpan(0, 3, 46),
                    Link = "MiPBQJq49xk"
                };
                blurryface.Songs.Add(song);
            }
        }

        [HttpGet]
        public ActionResult SongIndex()
        {
            return View(SongViewModel.Songs);
        }
    }
}
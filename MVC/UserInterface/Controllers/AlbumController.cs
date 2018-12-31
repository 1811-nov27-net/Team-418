using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Models;

namespace UserInterface.Controllers
{
    public class AlbumController : AServiceController
    {
        public AlbumController(HttpClient client) : base(client)
        {
            if (AlbumViewModel.Albums.Count == 0)
            {
                Random rand = new Random(DateTime.Now.TimeOfDay.Milliseconds);
                for (int i = 0; i < 100; ++i)
                {
                    AlbumViewModel album = new AlbumViewModel
                    {
                        Name = "AlbumName " + (rand.Next() % 100).ToString(),
                    };

                    ArtistViewModel artist = new ArtistViewModel
                    {
                        Name = "ArtistName " + (rand.Next() % 100).ToString(),
                    };

                    List<SongViewModel> songs = new List<SongViewModel>();
                    for (int j = 0; j < 10; ++j)
                    {
                        songs.Add(new SongViewModel
                        {
                            Album = album.Name,
                            Artist = artist.Name,
                            Name = "Name " + (rand.Next() % 100).ToString(),
                            PlayTime = new TimeSpan(0, rand.Next() % 5, rand.Next() % 59),
                            Link = "invalid link."
                        });
                    }
                    album.Songs = songs;
                    artist.Albums.Add(album);
                    album.Artist = artist;
                }
            }
        }

        // View of all albums
        [HttpGet]
        public ActionResult Index()
        {
            return View(AlbumViewModel.Albums);
        }
    }
}
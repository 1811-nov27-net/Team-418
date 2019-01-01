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
            SongViewModel.Songs.Clear();
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
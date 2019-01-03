using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Models;

namespace UserInterface.Controllers
{
    public class UserController : AServiceController
    {
        public UserController(HttpClient client) : base(client)
        {
        }

        [HttpGet]
        public ActionResult FavoritesView(int id)
        {
            return View(UserViewModel.GetById(id).FavoriteSongModels);
        }

        [HttpGet]
        public async Task<ActionResult> AddFavorite(int id)
        {
            try
            {
                if (!ModelState.IsValid || UserViewModel.CurrentUser == null)
                    return RedirectToAction("SongIndex", "Song");

                var song = SongViewModel.GetById(id);
                if (UserViewModel.CurrentUser.FavoriteSongs.Contains(song.Name))
                    return RedirectToAction("SongIndex", "Song");

                UserViewModel.CurrentUser.AddFavoriteById(id);

                HttpRequestMessage request = CreateRequestToService(HttpMethod.Post, "api/favorite",
                   new { UserName = UserViewModel.CurrentUser.Name, SongName = song.Name, SongArtist = song.Artist });
                HttpResponseMessage response = await Client.SendAsync(request);

                // (if status code is not 200-299 (for success))
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SongIndex", "Song");
                }

                return RedirectToAction(nameof(FavoritesView), new { id = UserViewModel.CurrentUser.Id });
            }
            catch (Exception)
            {
                return RedirectToAction("SongIndex", "Song");
            }
        }
        [HttpGet]
        public async Task<ActionResult> RemoveFavorite(int id)
        {
            try
            {
                if (!ModelState.IsValid || UserViewModel.CurrentUser == null)
                    return RedirectToAction("SongIndex", "Song");

                var song = SongViewModel.GetById(id);
                if (!UserViewModel.CurrentUser.FavoriteSongs.Contains(song.Name))
                    return RedirectToAction("SongIndex", "Song");

                UserViewModel.CurrentUser.FavoriteSongs.Remove(song.Name);

                HttpRequestMessage request = CreateRequestToService(HttpMethod.Delete, "api/favorite",
                   new { UserName = UserViewModel.CurrentUser.Name, SongName = song.Name, SongArtist = song.Artist });
                HttpResponseMessage response = await Client.SendAsync(request);

                // (if status code is not 200-299 (for success))
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SongIndex", "Song");
                }

                return RedirectToAction(nameof(FavoritesView), new { id = UserViewModel.CurrentUser.Id });
            }
            catch (Exception)
            {
                return RedirectToAction("SongIndex", "Song");
            }
        }



    }
}
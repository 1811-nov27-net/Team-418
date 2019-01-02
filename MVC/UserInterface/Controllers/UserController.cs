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
        
        public ActionResult AddFavorite(int id)
        {
            UserViewModel.CurrentUser.AddFavoriteById(id);

            return RedirectToAction(nameof(FavoritesView), new { id = UserViewModel.CurrentUser.Id });
        }
        


    }
}
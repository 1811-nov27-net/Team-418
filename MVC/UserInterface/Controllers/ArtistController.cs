using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Models;

namespace UserInterface.Controllers
{
    public class ArtistController : AServiceController
    {
        public ArtistController(HttpClient client) : base(client)
        {
        }

        [HttpGet]
        public ActionResult ArtistIndex()
        {
            return View(ArtistViewModel.Artists);
        }

        [HttpGet]
        public ActionResult ArtistView(int id)
        {
            return View(ArtistViewModel.GetById(id));
        }
    }
}
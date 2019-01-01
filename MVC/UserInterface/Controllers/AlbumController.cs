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
        }

        // View of all albums
        [HttpGet]
        public ActionResult AlbumIndex()
        {
            return View(AlbumViewModel.Albums);
        }

        [HttpGet]
        public ActionResult AlbumView(int id)
        {
            return View(AlbumViewModel.GetById(id));
        }
    }
}
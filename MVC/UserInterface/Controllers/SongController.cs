using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UserInterface.Controllers
{
    public class SongController : AServiceController
    {
        public SongController(HttpClient client) : base(client)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
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
        //public async Task<ActionResult> Profile(int id)
        public ActionResult Profile(UserViewModel user)
        {
            return View(user);
        }

    }
}
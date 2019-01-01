using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IMusicRepo Repo { get; }

        public UserController(IMusicRepo repo)
        {
            Repo = repo;
        }

        public static List<UserModel> Data = new List<UserModel>
        {
            new UserModel
            {
                Id = 1,
                Name = "Jane Doe",
                Admin = false
            },
            new UserModel
            {
                Id = 2,
                Name = "John Doe",
                Admin = true
            }
        };

        // GET: api/User
        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> Get()
        {
            List<UserModel> dispUsers = null;
            // need a get all users method in Repo
            try
            {
                return Data;
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

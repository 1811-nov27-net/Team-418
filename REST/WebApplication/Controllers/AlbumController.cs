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
    public class AlbumController : ControllerBase
    {
        public IMusicRepo Repo { get; }

        public AlbumController(IMusicRepo repo)
        {
            Repo = repo;
        }
        /*
        // GET: api/Album
        [HttpGet]
        public ActionResult<IEnumerable<AlbumModel>> Get()
        {
            
        }
        */
        // GET: api/Album/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Album
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Album/5
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

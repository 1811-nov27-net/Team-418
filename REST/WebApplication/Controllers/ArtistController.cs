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
    public class ArtistController : ControllerBase
    {
        public IMusicRepo Repo { get; }

        public ArtistController(IMusicRepo repo)
        {
            Repo = repo;
        }
        /*
        // do we have a get all artist function?
        // GET: api/Artist
        [HttpGet]
        public ActionResult<IEnumerable<ArtistModel>> Get()
        {
            List<ArtistModel> dispArtists = null;

            try
            {
                dispArtists = Repo.Get
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        */
        // GET: api/Artist/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Artist
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Artist/5
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

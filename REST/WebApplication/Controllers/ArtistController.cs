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
        
        // GET: api/Artist
        [HttpGet]
        public ActionResult<IEnumerable<ArtistModel>> Get()
        {
            List<ArtistModel> dispArtists = null;

            try
            {
                dispArtists = Repo.GetAllArtists().Select(x => new ArtistModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    City = x.City,
                    Stateprovice = x.Stateprovince,
                    Country = x.Country,
                    Formed = x.Formed,
                    LatestRelease = x.LatestRelease,
                    Website = x.Website
                }).ToList();
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }

            return dispArtists;
        }
        
        // GET: api/Artist/5
        [HttpGet("{id}")]
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

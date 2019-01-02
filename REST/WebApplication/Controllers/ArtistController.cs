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
                dispArtists = Repo.GetAllArtists().Result.Select(x => new ArtistModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    City = x.City,
                    Stateprovince = x.Stateprovince,
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
        public ActionResult<ArtistModel> Get(int id)
        {
            ArtistModel dispArtist = null;
            try
            {
                Library.Artists getArtist = Repo.GetArtistById(id).Result;
                dispArtist = new ArtistModel
                {
                    Id = getArtist.Id,
                    Name = getArtist.Name,
                    City = getArtist.City,
                    Stateprovince = getArtist.Stateprovince,
                    Country = getArtist.Country,
                    Formed = getArtist.Formed,
                    LatestRelease = getArtist.LatestRelease,
                    Website = getArtist.Website
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

            return dispArtist;
        }

        // POST: api/Artist
        [HttpPost]
        public void Post([FromBody] Library.Artists value)
        {
            try
            {
                Repo.AddArtist(value);
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
            }         
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
            try
            {
                Repo.RemoveArtist(id);
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
            }
        }
    }
}

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

        public static List<AlbumModel> Data = new List<AlbumModel>
        {
            new AlbumModel
            {
                Id = 1,
                Name = "DummyTestAlbum",
                Artist = "Dunno",
                Release = new DateTime(2001, 2, 20),
                Genre = "Country"
            },
            new AlbumModel
            {
                Id = 2,
                Name = "DummyTestAlbum2",
                Artist = "Dunno Jr.",
                Release = new DateTime(2018, 9, 6),
                Genre = "Country"
            }
        };
        
        // GET: api/Album
        [HttpGet]
        public ActionResult<IEnumerable<AlbumModel>> Get()
        {
            List<AlbumModel> dispAlbums = null;
            // need a get all albums method in Repo
            try
            {
                //dispAlbums = Repo.GetAllAlbumsByArtist

                return Data;
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }
        }
        
        // GET: api/Album/5
        [HttpGet("{id}")]
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

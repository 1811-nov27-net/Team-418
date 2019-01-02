using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        // dummy data for testing purposes right now, will eliminate later
        public static List<SongModel> Data = new List<SongModel>
        {
            new SongModel
            {
                Id = 1,
                Name = "POP/STARS",
                Artist = "ft Madison Beer, (G)I-DLE, Jaira Burns",
                Album = "",
                PlayTime = new TimeSpan(0, 3, 22),
                Link = "UOxkGD8qRB4",
                Genre = "KPop",
                Release = new DateTime(2018, 11, 3),
                Cover = false
            },
            new SongModel
            {
                Id = 2,
                Name = "Last Surprise",
                Artist = "Shoji Meguro, Lyn Inaizumi",
                Album = "Persona 5 Original Soundtrack",
                PlayTime = new TimeSpan(0, 3, 55),
                Link = "eFVj0Z6ahcI",
                Genre = "Jazz/Jpop",
                Release = new DateTime(2017, 1, 17),
                Cover = false
            }
        };

        // GET: api/Song
        [HttpGet]
        public ActionResult<IEnumerable<SongModel>> Get()
        {
            try
            {
                return Data;    
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }
        }

        // GET: api/Song/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Song
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Song/5
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

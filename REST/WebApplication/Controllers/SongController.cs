using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        public IMusicRepo Repo { get; }

        public SongController(IMusicRepo repo)
        {
            Repo = repo;
        }

        // GET: api/Song
        [HttpGet]
        public ActionResult<IEnumerable<SongModel>> Get()
        {
            List<SongModel> dispSongs = null;
            try
            {
                dispSongs = Repo.GetAllSongs().Result.Select(x => new SongModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Artist = x.Artist,
                    Album = CheckAlbumName(x.Id).Result,
                    PlayTime = x.Length,
                    Genre = x.Genre,
                    Release = x.InitialRelease,
                    Cover = x.Cover,
                    Link = x.Link
                }).ToList();
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }

            return dispSongs;
        }

        // Checks if the song ID in question has an album name
        // attached to it
        public async Task<string> CheckAlbumName(int Id)
        {
            var getAlbums = await Repo.GetAllAlbumsBySong(Id);

            if (getAlbums == null)
            {
                return "";
            }
            return getAlbums.FirstOrDefault().Name;
        }

        // need to implement
        // GET: api/Song/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Song
        [HttpPost]
        public void Post([FromBody] Song value)
        {
            try
            {
                Repo.AddSong(value);
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
            }
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

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
        public async Task<ActionResult<IEnumerable<SongModel>>> Get()
        {
            List<SongModel> dispSongs = new List<SongModel>();

            try
            {
                IEnumerable<Song> songs = await Repo.GetAllSongs();

                foreach (Song song in songs)
                {
                    SongModel songModel = new SongModel
                    {
                        Id = song.Id,
                        Name = song.Name,
                        Artist = song.Artist,
                        PlayTime = song.Length,
                        Genre = song.Genre,
                        Release = song.InitialRelease,
                        Cover = song.Cover,
                        Link = song.Link
                    };
                    songModel.Album = await CheckAlbumName(song.Id);
                    dispSongs.Add(songModel);
                }
                return dispSongs;
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }

        }

        // Checks if the song ID in question has an album name
        // attached to it
        public async Task<string> CheckAlbumName(int Id)
        {
            var albums = await Repo.GetAllAlbumsBySong(Id);
            var albumName = albums.FirstOrDefault()?.Name;
            if (albumName == null)
                albumName = "";
            return null;
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

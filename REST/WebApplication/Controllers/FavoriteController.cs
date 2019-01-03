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
    public class FavoriteController : ControllerBase
    {
        public IMusicRepo Repo { get; }

        public FavoriteController(IMusicRepo repo)
        {
            Repo = repo;
        }

        // GET: api/Favorite/5
        [HttpGet("{userName}")]
        public async Task<ActionResult<IEnumerable<FavoriteModel>>> Get(string userName)
        {
            List<FavoriteModel> dispFaves = new List<FavoriteModel>();

            try
            {
                Library.Users getUser = await Repo.GetUserByName(userName);
                IEnumerable<Library.Favorites> faves = await Repo.GetAllFavoritesByUser(getUser.Id);

                Library.Users user = await Repo.GetUserById(getUser.Id);

                foreach (var item in faves)
                {
                    Library.Song song = await Repo.GetSongById(item.Song);

                    FavoriteModel fave = new FavoriteModel
                    {
                        Id = item.Id,
                        UserName = user.Name,
                        SongName = song.Name,
                        SongArtist = song.Artist
                    };

                    dispFaves.Add(fave);
                }

                return dispFaves;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // POST: api/Favorite
        [HttpPost]
        public async Task<string> Post([FromBody] string userName, string songName)
        {
            try
            {
                Library.Users user = await Repo.GetUserByName(userName);
                IEnumerable<Library.Song> getSongFromHere = await Repo.GetAllSongs();
                Library.Song song = getSongFromHere.Where(x => x.Name == songName).FirstOrDefault();

                string addMe = await Repo.AddFavorite(user.Id, song.Id);

                if (!bool.Parse(addMe))
                {
                    return addMe;
                }

                return "Added to favorites!";
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex).ToString();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<string> Delete(string userName, string songName)
        {
            try
            {
                Library.Users user = await Repo.GetUserByName(userName);
                IEnumerable<Library.Song> getSongFromHere = await Repo.GetAllSongs();
                Library.Song song = getSongFromHere.Where(x => x.Name == songName).FirstOrDefault();

                string removeMe = await Repo.RemoveFavorite(user.Id, song.Id);

                if (!bool.Parse(removeMe))
                {
                    return removeMe;
                }

                return "Song unfavorited!";
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex).ToString();
            }
        }
    }
}

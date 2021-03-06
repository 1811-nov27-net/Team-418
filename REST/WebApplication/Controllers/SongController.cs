﻿using System;
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
            if (albums == null)
                return "";

            var album = albums.FirstOrDefault();
            
            return album.Name;
        }

        // throws an exception error, needs to be fixed
        // GET: api/Song/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SongModel>> Get(int id)
        {
            SongModel dispSong = null;

            try
            {
                // null reference exception on DataAccess.Mapper.Map(Songs song) in Mapper.cs, line 19
                // AggregateException on Song getSong = Repo.GetSongById(id).Result;
                Song getSong = Repo.GetSongById(id).Result;
                dispSong = new SongModel
                {
                    Id = getSong.Id,
                    Name = getSong.Name,
                    Artist = getSong.Name,
                    PlayTime = getSong.Length,
                    Link = getSong.Link,
                    Genre = getSong.Genre,
                    Release = getSong.InitialRelease,
                    Cover = getSong.Cover
                };
                dispSong.Album = await CheckAlbumName(getSong.Id);
            }
            catch (Exception)
            {

                throw;
            }

            return dispSong;
        }

        // needs to check for an album name, artist name
        // POST: api/Song
        [HttpPost]
        public async Task Post([FromBody] SongWithAlbum value)
        {
            try
            {
                if (await Repo.GetArtistByName(value.Artist) == null)
                {
                    Library.Artists checkArtist = new Library.Artists
                    {
                        Id = 0,
                        Name = value.Artist,
                        City = "",
                        Stateprovince = "",
                        Country = "",
                        Formed = DateTime.Now,
                        LatestRelease = DateTime.Now,
                        Website = ""
                    };
                    string artistError = await Repo.AddArtist(checkArtist);
                    if (!bool.Parse(artistError))
                    {
                        Console.WriteLine(artistError);
                    }
                }

                Library.Artists getArtist = await Repo.GetArtistByName(value.Artist);
                Library.Albums getAlbum = await Repo.GetAlbumByNameAndArtist(value.Album, getArtist.Id);

                // check if album exists
                // if not, add it to the DB
                if (value.Album != null)
                {                
                    if (getAlbum == null)
                    {
                        Library.Albums checkAlbum = new Library.Albums
                        {
                            Id = 0,
                            Name = value.Album,
                            Artist = value.Artist,
                            Release = value.InitialRelease,
                            Genre = value.Genre
                        };
                        string albumError = await Repo.AddAlbum(checkAlbum);
                        if (!bool.Parse(albumError))
                        {
                            Console.WriteLine(albumError);
                        }

                        getAlbum = await Repo.GetAlbumByNameAndArtist(value.Album, getArtist.Id);
                    }                  
                }

                // since our DB song table does not have an album name
                // we are passing in from a different class that does have
                // an album name into the actual Song class that gets utilized
                // in the DB, then we will have to pass in the album name
                // elsewhere
                Song newSong = new Song
                {
                    Id = value.Id,
                    Name = value.Name,
                    Artist = value.Artist,
                    Genre = value.Genre,
                    Length = value.Length,
                    InitialRelease = value.InitialRelease,
                    Cover = value.Cover,
                    Link = value.Link
                };

                string songError = await Repo.AddSong(newSong);
                if (!bool.Parse(songError))
                {
                    Console.WriteLine(songError);
                }

                // Pull the song back out of the database to get the database-generated ID so it can be added to the album
                Song getSongForDbId = await Repo.GetSongByNameAndArtist(newSong.Name, getArtist.Id);

                string addToAlbum = await Repo.AddSongToAlbum(getSongForDbId.Id, getAlbum.Id);
                if (!bool.Parse(addToAlbum))
                {
                    Console.WriteLine(addToAlbum);
                }

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

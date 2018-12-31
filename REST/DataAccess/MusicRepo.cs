using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class MusicRepo : IMusicRepo
    {
        private readonly _1811proj1_5Context _db;

        public MusicRepo(_1811proj1_5Context db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }      

        /* 
          * ---------------------------------------------------
          * ---------| CREATE - ADD INFO TO DATABASE |---------
          * ---------------------------------------------------
         */

        // Add a new artist to the database
        public string AddArtist(Dictionary<string, string> formData)
        {
            // Method is accepting a dictionary where both the key and value are strings.  
            // This is assuming that the controller that will eventually call this method with accept the form data (probably/likely in an IFormCollection);
            // then it will process the information into a dictionary to feed to this method in the repo.  
            // The controller *will* need to pass null values into the dictionary it sends where information was not provided.
            // (assuming that the IFormCollection doesn't autmoatically inject nulls into missing values)

            // A string is being returned for user feedback, and also to return a value to end the method early if errors occur that we don't want committed to the database

            //----------------------------------------------------------------

            // Secondary check to ensure another operation hasn't inserted the same data as this task since the first check
            //if (GetArtistByName(newArtist.ArName) != null)
            //{
            //    return "ERROR: Artist already exists in the database.  Operation abandoned.";
            //}

            //// Add new artist to the database and save changes
            //_db.Artists.Add(newArtist);
            //_db.SaveChanges();

            //// Get the artist that was just added
            //Artists addedArtist = GetArtistByName(newArtist.ArName);

            //// Return string of the new artist's ID if it was successfully added
            //return addedArtist.ArId.ToString();
            return "true";
        }

        // Add a new album to the database
        public string AddAlbum(Dictionary<string, string> formData)
        {
            // Declare user to be set and returned immediately if there is any error
            string returnMessage = null;

            // Declare the string variables for album info, passing data in from form
            string name = formData["name"];
            string genre = formData["genre"];

            // Declare non-string variables for artist info, set as null until checks to parse them into the proper data type are performed
            int artistId = -1;
            DateTime? release = null;

            if (name == null)
            {
                // If the name from the form data is null, set error message and return it immediately
                returnMessage = "CRITICAL ERROR: Album name is required and was not provided.  Operation abandoned.  Please contact your system administrator immediately.";
                return returnMessage;
            }

            if (formData["artistId"] != null)
            {
                try
                {
                    artistId = Int32.Parse(formData["artistId"]);
                }
                catch (FormatException)
                {
                    returnMessage = "ERROR: Artist ID information is not recognizable as a valid ID.  Operation abandoned.";
                    return returnMessage;
                }
            }
            else
            {
                returnMessage = "CRITICAL ERROR: Artist ID is required and was not provided.  Operation abandoned.  Please contact your system administrator immediately.";
                return returnMessage;
            }

            if (GetAlbumByNameAndArtist(name, artistId) != null)
            {
                returnMessage = "ERROR: Album already exists in the database.  Operation abandoned.";
                return returnMessage;
            }              

            if (formData["release"] != null)
            {
                try
                {
                    release = DateTime.Parse(formData["release"]);
                }
                catch (FormatException)
                {
                    release = null;
                    returnMessage = "ERROR: Release date information is not recognizable as a valid date.  Operation abandoned.";
                    return returnMessage;
                }
            }

            Albums newAlbum = new Albums
            {
                AlName = name,
                AlArtist = artistId,
                AlRelease = release,
                AlGenre = genre
            };

            _db.Albums.Add(newAlbum);
            _db.SaveChanges();

            // Get the album that was just added
            Albums addedAlbum = GetAlbumByNameAndArtist(newAlbum.AlName, newAlbum.AlArtist);

            return addedAlbum.AlId.ToString();
        }

        // Add song to database
        public string AddSong(Dictionary<string, string> formData)
        {
            // Similar setup as AddArtistToDatabase()
            string returnMessage = null;

            string name = formData["name"];
            int artistId = -1;           
            string genre = formData["genre"];         
            string link = formData["link"];

            TimeSpan? length = null;
            DateTime? initialRelease = null;
            bool? cover = null;

            if (name == null)
            {
                // If the name from the form data is null, set error message and return it immediately
                returnMessage = "CRITICAL ERROR: Song name is required and was not provided.  Operation abandoned.  Please contact your system administrator immediately.";
                return returnMessage;
            }

            if (formData["artistId"] != null)
            {
                try
                {
                    artistId = Int32.Parse(formData["artistId"]);
                }
                catch (FormatException)
                {
                    returnMessage = "ERROR: Artist ID information is not recognizable as a valid ID.  Operation abandoned.";

                    return returnMessage;
                }
            }
            else
            {
                returnMessage = "CRITICAL ERROR: Artist ID is required and was not provided.  Operation abandoned.  Please contact your system administrator immediately.";
                return returnMessage;
            }

            if (GetSongByNameAndArtist(name, artistId) != null)
            {
                returnMessage = "ERROR: Song already exists in the database.  Operation abandoned.";
                return returnMessage;
            }

            if (formData["length"] != null)
            {
                try
                {
                    length = TimeSpan.Parse(formData["length"]);
                }
                catch (FormatException)
                {
                    length = null;
                    returnMessage = "ERROR: Song length information is not recognizable as a valid time.  Operation abandoned.";
                    return returnMessage;
                }
            }

            if (formData["initialRelease"] != null)
            {
                try
                {
                    initialRelease = DateTime.Parse(formData["initialRelease"]);
                }
                catch (FormatException)
                {
                    initialRelease = null;
                    returnMessage = "ERROR: Initial release date information is not recognizable as a valid date.  Operation abandoned.";
                    return returnMessage;
                }
            }

            if (formData["cover"] != null)
            {
                try
                {
                    cover = Boolean.Parse(formData["cover"]);
                }
                catch (FormatException)
                {
                    cover = null;
                    returnMessage = "ERROR: Cover information not recognizable as a boolean.  Operation abandoned.";
                    return returnMessage;
                }
            }

            Songs newSong = new Songs
            {
                SName = name,
                SArtist = artistId,
                SLength = length,
                SGenre = genre,
                SInitialrelease = initialRelease,
                SCover = cover,
                SLink = link
            };

            _db.Songs.Add(newSong);
            _db.SaveChanges();

            // Get the song that was just added
            Songs addedSong = GetSongByNameAndArtist(newSong.SName, newSong.SArtist);

            return addedSong.SId.ToString();
        }

        public string AddSongToAlbum(int songId, int albumId)
        {
            if (GetSongFromAlbum(songId, albumId) != null)
            {
                return "ERROR: Song already exists on this album.  Operation abandoned.";
            }

            if (GetSongById(songId) == null)
            {
                return "ERROR: No song exists in the database with the given ID.  Operation abandoned.";
            }

            if (GetAlbumById(albumId) == null)
            {
                return "ERROR: No album exists in the database with the given ID.  Operation abandoned.";
            }

            AlbumSongs songToAlbum = new AlbumSongs
            {
                AsSong = songId,
                AsAlbum = albumId
            };

            _db.AlbumSongs.Add(songToAlbum);
            _db.SaveChanges();

            // Return true so whatever calls this method can attempt to parse a boolean
            // If boolean can't be parsed, then an error was returned that can be shown to the user
            return "true";
        }

        public string AddUserFavorite(int userId, int songId)
        {
            if (GetOneFavoriteForUser(userId, songId) != null)
            {
                return "ERROR: User already has selected song listed as a favorite.  Operation abandoned.";
            }

            if (GetSongById(songId) == null)
            {
                return "ERROR: No song exists in the database with the given ID.  Operation abandoned.";
            }

            if (GetUserById(userId) == null)
            {
                return "ERROR: No user exists in the database with the given ID.  Operation abandoned.";
            }

            Favorites newFave = new Favorites
            {
                FSong = songId,
                FUser = userId
            };

            _db.Favorites.Add(newFave);
            _db.SaveChanges();

            // Return true so whatever calls this method can attempt to parse a boolean
            // If boolean can't be parsed, then an error was returned that can be shown to the user
            return "true";
        }

        public string AddCover(int originalId, int coverId)
        {
            if (GetCover(originalId, coverId) != null)
            {
                return "ERROR: There is already an entry in the database matching the cover version to the original.  Operation abandoned.";
            }

            if (GetSongById(coverId) == null)
            {
                return "ERROR: No song exists in the database with the given ID for the cover version.  Operation abandoned.";
            }

            if (GetSongById(originalId) == null)
            {
                return "ERROR: No song exists in the database with the given ID for the original version.  Operation abandoned.";
            }

            Covers newCover = new Covers
            {
                COriginal = originalId,
                CCover = coverId
            };

            _db.Covers.Add(newCover);
            _db.SaveChanges();

            // Return true so whatever calls this method can attempt to parse a boolean
            // If boolean can't be parsed, then an error was returned that can be shown to the user
            return "true";
        }

        public string AddUser(string name, bool admin)
        {
            if (name == null)
            {
                return "CRITICAL ERROR: User name is required and was not provided.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            if (GetUserByName(name) != null)
            {
                return "ERROR: There is already a user with this user name.  Please select a different user name.  Operation abandoned.";
            }

            Users newUser = new Users
            {
                UName = name,
                UAdmin = admin
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();

            return "true";
        }

        /* 
         * ---------------------------------------------------
         * ---------| READ - GET INFO FROM DATABASE |---------
         * ---------------------------------------------------
        */

        public AlbumSongs GetSongFromAlbum(int songId, int albumId)
        {
            try
            {
                return _db.AlbumSongs.Where(s => s.AsSong ==  songId).Where(al => al.AsAlbum == albumId).AsNoTracking().FirstOrDefault();
            }
            catch (ArgumentNullException)
            {

                return null;
            }
        }

        public Artists GetArtistByName(string name)
        {
            try
            {
                return _db.Artists.Where(a => a.ArName == name).AsNoTracking().FirstOrDefault();
            }
            catch (ArgumentNullException)
            {

                return null;
            }
        }

        public Artists GetArtistById(int id)
        {
            try
            {
                return _db.Artists.Where(a => a.ArId == id).AsNoTracking().FirstOrDefault();
            }
            catch (ArgumentNullException)
            {

                return null;
            }
        }

        public Albums GetAlbumByNameAndArtist(string name, int artistId)
        {
            try
            {
                return _db.Albums.Where(a => a.AlName == name).Where(ar => ar.AlArtist == artistId).AsNoTracking().FirstOrDefault();
            }
            catch (ArgumentNullException)
            {

                return null;
            }
        }

        public IEnumerable<Albums> GetAllAlbumsByArtist(int artistId)
        {
            try
            {
                return _db.Albums.Where(ar => ar.AlArtist == artistId).AsNoTracking().ToList();
            }
            catch (ArgumentNullException)
            {

                return null;
            }
        }

        public Albums GetAlbumById(int id)
        {
            try
            {
                return _db.Albums.Where(a => a.AlId == id).AsNoTracking().FirstOrDefault();
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public Songs GetSongByNameAndArtist(string name, int artistId)
        {
            try
            {
                return _db.Songs.Where(a => a.SName == name).Where(ar => ar.SArtist == artistId).AsNoTracking().FirstOrDefault();
            }
            catch (ArgumentNullException)
            {

                return null;
            }
        }

        public Songs GetSongById(int id)
        {
            try
            {
                 return _db.Songs.Where(s => s.SId == id).AsNoTracking().FirstOrDefault();
            }
            catch (ArgumentNullException)
            {
                return null;
            }          
        }

        public Users GetUserById(int id)
        {
            try
            {
                return _db.Users.Where(u => u.UId == id).AsNoTracking().FirstOrDefault();
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public Users GetUserByName(string name)
        {
            try
            {
                return _db.Users.Where(u => u.UName == name).AsNoTracking().FirstOrDefault();
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        // Get all songs - users can search all songs to find new music
        public IEnumerable<Library.Song> GetAllSongs()
        {
            return Mapper.Map(_db.Songs.AsNoTracking());
        }

        public IEnumerable<Songs> GetAllSongsByArtist(int artistId)
        {
            try
            {
                return _db.Songs.Where(ar => ar.SArtist == artistId).AsNoTracking().ToList();
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        // Get all favorites for a user based on User ID
        public IEnumerable<Favorites> GetFavoritesByUser(int userId)
        {
            try
            {
                return _db.Favorites.Where(u => u.FUser == userId).AsNoTracking().ToList();
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public IEnumerable<Favorites> GetFavoritesBySong(int songId)
        {
            try
            {
                return _db.Favorites.Where(u => u.FSong == songId).AsNoTracking().ToList();
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public Favorites GetOneFavoriteForUser(int userId, int songId)
        {
            try
            {
                return _db.Favorites.Where(u => u.FUser == userId).Where(s => s.FSong == songId).AsNoTracking().FirstOrDefault();
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public Covers GetCover(int originalId, int coverId)
        {
            try
            {
                return _db.Covers.Where(o => o.COriginal == originalId).Where(c => c.CCover == coverId).AsNoTracking().FirstOrDefault();
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public IEnumerable<Covers> GetCoversByOriginal(int originalId)
        {
            try
            {
                return _db.Covers.Where(c => c.COriginal == originalId).AsNoTracking().ToList();
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public Covers GetOriginalByCover(int coverId)
        {
            try
            {
                return _db.Covers.Where(c => c.CCover == coverId).AsNoTracking().FirstOrDefault();
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        /* 
        * ------------------------------------------------------
        * ---------| UPDATE - UPDATE INFO IN DATABASE |---------
        * ------------------------------------------------------
       */

        public string UpdateArtist(Artists libArtist)
        {
            Artists updateMe = null;
            try
            {
                 updateMe = _db.Artists.Where(ar => ar.ArName == libArtist.ArName).FirstOrDefault();
            }
            catch (ArgumentNullException)
            {
                return "ERROR: Artist could not be retrieved from database to update.  Operation abandoned.";
            }
            
            // updateMe = Mapper.Map(LibararyArtistToDataContextArtist);

            try
            {
                _db.Artists.Update(updateMe);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Artist could not be updated.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        /* 
        * --------------------------------------------------------
        * ---------| DELETE - REMOVE INFO FROM DATABASE |---------
        * --------------------------------------------------------
       */

        public string RemoveSongFromAlbum(int songId, int albumId)
        {
            AlbumSongs removeMe = GetSongFromAlbum(songId, albumId);

            if (removeMe == null)
            {
                return "ERROR: Song to be removed does not exist on album.  Operation abandoned.";
            }
            else
            {
                try
                {
                    _db.AlbumSongs.Remove(removeMe);
                    _db.SaveChanges();

                    return "true";
                }
                catch (Exception)
                {

                    return "CRITICAL ERROR: Song could not be removed from album.  Operation abandoned.  Please contact your system administrator immediately.";
                }
            } 
        }

        public string RemoveSongFromAllAlbums(int songId)
        {
            IEnumerable<AlbumSongs> removeUs = _db.AlbumSongs.Where(al => al.AsSong == songId).AsNoTracking().ToList();

            if (removeUs == null)
            {
                return "false";
            }

            try
            {
                _db.AlbumSongs.RemoveRange(removeUs);
                _db.SaveChanges();

                return "true";
            }
            catch (Exception)
            {

                return "CRITICAL ERROR: Song could not be removed from any albums.  Operation abandoned.  Please contact your system administrator immediately.";
            }
        
        }

        public string RemoveAllSongsFromAlbum(int albumId)
        {
            IEnumerable<AlbumSongs> removeUs = _db.AlbumSongs.Where(al => al.AsAlbum == albumId).AsNoTracking().ToList();

            if (removeUs == null)
            {
                return "false";
            }

            try
            {
                _db.AlbumSongs.RemoveRange(removeUs);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Songs could not be removed from album.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public string RemoveAlbum(int albumId)
        {
            Albums removeMe = GetAlbumById(albumId);

            if (removeMe == null)
            {
                return "ERROR: Album already does not exist.  Operation abandoned.";
            }

            string removeSongs = RemoveAllSongsFromAlbum(albumId);

            try
            {
                bool.Parse(removeSongs);
            }
            catch (FormatException)
            {
                return removeSongs;
            }

            try
            {
                _db.Albums.Remove(removeMe);
                _db.SaveChanges();   
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Album could not be removed.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public string RemoveUser(int userId)
        {
            Users removeMe = GetUserById(userId);

            if (removeMe == null)
            {
                return "ERROR: User already does not exist.  Operation abandoned.";
            }

            string removeFavorites = RemoveFavoritesByUser(userId);

            try
            {
                bool.Parse(removeFavorites);
            }
            catch (FormatException)
            {
                return removeFavorites;
            }

            try
            {
                _db.Users.Remove(removeMe);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: User could not be removed.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public string RemoveSong(int songId)
        {
            Songs removeMe = GetSongById(songId);

            if (removeMe == null)
            {
                return "ERROR: Song already does not exist.  Operation abandoned.";
            }

            string removeFromAlbums = RemoveSongFromAllAlbums(songId);

            try
            {
                bool.Parse(removeFromAlbums);
            }
            catch (FormatException)
            {
                return removeFromAlbums;
            }

            string removeFromCovers = RemoveSongFromCovers(songId);

            try
            {
                bool.Parse(removeFromCovers);
            }
            catch (FormatException)
            {
                return removeFromCovers;
            }

            string removeFromFavorites = RemoveFavoritesBySong(songId);

            try
            {
                bool.Parse(removeFromFavorites);
            }
            catch (FormatException)
            {
                return removeFromFavorites;
            }

            try
            {
                _db.Songs.Remove(removeMe);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Song could not be removed.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public string RemoveSongFromCovers(int songId)
        {
            IEnumerable<Covers> checkCovers = GetCoversByOriginal(songId);
            Covers checkOriginal = GetOriginalByCover(songId);

            if (checkCovers == null && checkOriginal == null)
            {
                return "false";
            }

            try
            {
                _db.Covers.RemoveRange(checkCovers);
                _db.Covers.Remove(checkOriginal);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Song could not be unlisted as a cover or original version.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public string RemoveFavoritesBySong(int songId)
        {
            IEnumerable<Favorites> removeUs = GetFavoritesBySong(songId);

            if (removeUs == null)
            {
                return "false";
            }

            try
            {
                _db.Favorites.RemoveRange(removeUs);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Song could not be removed from favorites.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public string RemoveFavoritesByUser(int userId)
        {
            IEnumerable<Favorites> removeUs = GetFavoritesByUser(userId);

            if (removeUs == null)
            {
                return "false";
            }

            try
            {
                _db.Favorites.RemoveRange(removeUs);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: User could not be removed from favorites.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public string RemoveArtist(int artistId)
        {
            Artists removeMe = GetArtistById(artistId);

            if (removeMe == null)
            {
                return "ERROR: Artist already does not exist.  Operation abandoned.";
            }

            IEnumerable<Albums> artistsAlbums = GetAllAlbumsByArtist(artistId);

            foreach (var item in artistsAlbums)
            {
                string removeAlbum = RemoveAlbum(item.AlId);

                try
                {
                    bool.Parse(removeAlbum);
                }
                catch (FormatException)
                {
                    return removeAlbum;
                }
            }

            IEnumerable<Songs> songsWithNoAlbum = GetAllSongsByArtist(artistId);

            foreach (var item in songsWithNoAlbum)
            {
                string removeSong = RemoveSong(item.SId);

                try
                {
                    bool.Parse(removeSong);
                }
                catch (FormatException)
                {
                    return removeSong;
                }
            }

            try
            {
                _db.Artists.Remove(removeMe);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Artist could not be removed.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }      
    }
}

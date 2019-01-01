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
        public string AddArtist(Library.Artists artist)
        {
            if (GetArtistByName(artist.Name) != null)
            {
                return "ERROR: Artist already exists in the database.  Operation abandoned.";
            }

            Artists newArtist = Mapper.Map(artist);

            try
            {
                _db.Artists.Add(newArtist);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Artist could not be added to the database.  Operation abandoned.  Please contact your system administrator";
            }
            
            return "true";
        }

        // Add a new album to the database
        public string AddAlbum(Library.Albums album)
        {

            if (GetAlbumByNameAndArtist(album.Name, GetArtistByName(album.Artist).Id) != null)
            {
                return "ERROR: Album already exists in the database.  Operation abandoned.";
            }

            Albums newAlbum = Mapper.Map(album);

            newAlbum.AlArtist = GetArtistByName(album.Artist).Id;

            try
            {
                _db.Albums.Add(newAlbum);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Artist could not be added to the database.  Operation abandoned.  Please contact your system administrator";
            }

            return "true";
        }

        // Add song to database
        public string AddSong(Library.Song song)
        {
            if (GetSongByNameAndArtist(song.Name, GetArtistByName(song.Artist).Id) != null)
            {
                return "ERROR: Song already exists in the database.  Operation abandoned.";
            }

            Songs newSong = Mapper.Map(song);

            newSong.SArtist = GetArtistByName(song.Artist).Id;

            try
            {
                _db.Songs.Add(newSong);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Song could not be added to the database.  Operation abandoned.  Please contact your system administrator";
            }

            return "true";
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
            if (_db.Favorites.Where(u => u.FUser == userId).Where(s => s.FSong == songId).AsNoTracking() != null)
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
            if (_db.Covers.Where(o => o.COriginal == originalId).Where(c => c.CCover == coverId).AsNoTracking() != null)
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

        public string AddUser(Library.Users user)
        {
            if(_db.Users.Where(u => u.UName == user.Name).AsNoTracking().FirstOrDefault() != null)
            {
                return "ERROR: User already exists in the database.  Operation abandoned.";
            }

            Users newUser = Mapper.Map(user);

            try
            {
                _db.Users.Add(newUser);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: User could not be added to the database.  Operation abandoned.  Please contact your system administrator";
            }

            return "true";
        }

        public string AddRequest(Library.PendingRequests request)
        {
            PendingRequests newRequest = Mapper.Map(request);

            try
            {
                _db.PendingRequests.Add(newRequest);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Request could not be added to the database.  Operation abandoned.  Please contact your system administrator";
            }

            return "true";
        }

        /* 
         * ---------------------------------------------------
         * ---------| READ - GET INFO FROM DATABASE |---------
         * ---------------------------------------------------
        */

        public Library.AlbumSongs GetSongFromAlbum(int songId, int albumId)
        {
            try
            {
                return Mapper.Map(_db.AlbumSongs.Where(s => s.AsSong ==  songId).Where(al => al.AsAlbum == albumId).AsNoTracking().FirstOrDefault());
            }
            catch (ArgumentNullException)
            {

                return null;
            }
        }

        public Library.Artists GetArtistByName(string name)
        {
            try
            {
                return Mapper.Map(_db.Artists.Where(a => a.ArName == name).AsNoTracking().FirstOrDefault());
            }
            catch (ArgumentNullException)
            {

                return null;
            }
        }

        public Library.Artists GetArtistById(int id)
        {
            try
            {
                return Mapper.Map(_db.Artists.Where(a => a.ArId == id).AsNoTracking().FirstOrDefault());
            }
            catch (ArgumentNullException)
            {

                return null;
            }
        }

        public Library.Albums GetAlbumByNameAndArtist(string name, int artistId)
        {
            try
            {
                return Mapper.Map(_db.Albums.Where(a => a.AlName == name).Where(ar => ar.AlArtist == artistId).AsNoTracking().FirstOrDefault());
            }
            catch (ArgumentNullException)
            {

                return null;
            }
        }

        public IEnumerable<Library.Albums> GetAllAlbumsByArtist(int artistId)
        {
            try
            {
                return Mapper.Map(_db.Albums.Where(ar => ar.AlArtist == artistId).AsNoTracking().ToList());
            }
            catch (ArgumentNullException)
            {

                return null;
            }
        }

        public Library.Albums GetAlbumById(int id)
        {
            try
            {
                return Mapper.Map(_db.Albums.Where(a => a.AlId == id).AsNoTracking().FirstOrDefault());
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public Library.Song GetSongByNameAndArtist(string name, int artistId)
        {
            try
            {
                return Mapper.Map(_db.Songs.Where(a => a.SName == name).Where(ar => ar.SArtist == artistId).AsNoTracking().FirstOrDefault());
            }
            catch (ArgumentNullException)
            {

                return null;
            }
        }

        public Library.Song GetSongById(int id)
        {
            try
            {
                 return Mapper.Map(_db.Songs.Where(s => s.SId == id).AsNoTracking().FirstOrDefault());
            }
            catch (ArgumentNullException)
            {
                return null;
            }          
        }

        public Library.Users GetUserById(int id)
        {
            try
            {
                return Mapper.Map(_db.Users.Where(u => u.UId == id).AsNoTracking().FirstOrDefault());
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public Library.Users GetUserByName(string name)
        {
            try
            {
                return Mapper.Map(_db.Users.Where(u => u.UName == name).AsNoTracking().FirstOrDefault());
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        // Get all songs - users can search all songs to find new music
        public IEnumerable<Library.Song> GetAllSongs()
        {
            return Mapper.Map(_db.Songs.Include(a => a.SArtistNavigation).AsNoTracking());
        }

        public IEnumerable<Library.Artists> GetAllArtists()
        {
            return Mapper.Map(_db.Artists.AsNoTracking());
        }

        public IEnumerable<Library.Song> GetAllSongsByArtist(int artistId)
        {
            try
            {
                return Mapper.Map(_db.Songs.Where(ar => ar.SArtist == artistId).AsNoTracking().ToList());
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        // Get all favorites for a user based on User ID
        public IEnumerable<Library.Favorites> GetFavoritesByUser(int userId)
        {
            try
            {
                return Mapper.Map(_db.Favorites.Where(u => u.FUser == userId).AsNoTracking().ToList());
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public IEnumerable<Library.Favorites> GetFavoritesBySong(int songId)
        {
            try
            {
                return Mapper.Map(_db.Favorites.Where(u => u.FSong == songId).AsNoTracking().ToList());
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public IEnumerable<Library.Song> GetCoversByOriginal(int originalId)
        {
            try
            {
                return _db.Covers.Where(c => c.COriginal == originalId).AsNoTracking().Select(x => GetSongById(x.CCover)).ToList();
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public Library.Song GetOriginalByCover(int coverId)
        {
            try
            {
                return _db.Covers.Where(c => c.CCover == coverId).AsNoTracking().Select(x => GetSongById(x.CCover)).FirstOrDefault();
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

        public string UpdateArtist(Library.Artists libArtist)
        {
            Artists updateMe = _db.Artists.Where(ar => ar.ArName == libArtist.Name).FirstOrDefault(); ;

            if (updateMe == null)
            {
                return "ERROR: Artist could not be retrieved from database to update.  Operation abandoned.";
            }      

            try
            {
                _db.Artists.Update(Mapper.Map(libArtist));
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Artist could not be updated.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public string UpdateAlbum(Library.Albums libAlbum)
        {
            Albums updateMe = _db.Albums.Where(al => al.AlName == libAlbum.Name).FirstOrDefault(); ;

            if (updateMe == null)
            {
                return "ERROR: Album could not be retrieved from database to update.  Operation abandoned.";
            }

            try
            {
                _db.Albums.Update(Mapper.Map(libAlbum));
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Album could not be updated.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public string UpdateSong(Library.Song libSong)
        {
            Songs updateMe = _db.Songs.Where(s => s.SName == libSong.Name).FirstOrDefault(); ;

            if (updateMe == null)
            {
                return "ERROR: Song could not be retrieved from database to update.  Operation abandoned.";
            }

            try
            {
                _db.Songs.Update(Mapper.Map(libSong));
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Song could not be updated.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public string UpdateUser(Library.Users libUser)
        {
            Users updateMe = _db.Users.Where(u => u.UName == libUser.Name).FirstOrDefault(); ;

            if (updateMe == null)
            {
                return "ERROR: User could not be retrieved from database to update.  Operation abandoned.";
            }

            try
            {
                _db.Users.Update(Mapper.Map(libUser));
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: User could not be updated.  Operation abandoned.  Please contact your system administrator immediately.";
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
            AlbumSongs removeMe = _db.AlbumSongs.Where(s => s.AsSong == songId).Where(al => al.AsAlbum == albumId).FirstOrDefault();

            if (removeMe == null)
            {
                return "ERROR: Song to be removed does not exist on album.  Operation abandoned.";
            }

            try
            {
                _db.AlbumSongs.Remove(removeMe);
                _db.SaveChanges();              
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Song could not be removed from album.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public string RemoveSongFromAllAlbums(int songId)
        {
            IEnumerable<AlbumSongs> removeUs = _db.AlbumSongs.Where(al => al.AsSong == songId).ToList();

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
            IEnumerable<AlbumSongs> removeUs = _db.AlbumSongs.Where(al => al.AsAlbum == albumId).ToList();

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
            Albums removeMe = _db.Albums.Where(al => al.AlId == albumId).FirstOrDefault();

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
            Users removeMe = _db.Users.Where(u => u.UId == userId).FirstOrDefault();

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
            Songs removeMe = _db.Songs.Where(s => s.SId == songId).FirstOrDefault();

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
            IEnumerable<Covers> checkCovers = _db.Covers.Where(c => c.CCover == songId).ToList();
            IEnumerable<Covers> checkOriginal = _db.Covers.Where(c => c.COriginal == songId).ToList();

            if (checkCovers == null && checkOriginal == null)
            {
                return "false";
            }

            try
            {
                _db.Covers.RemoveRange(checkCovers);
                _db.Covers.RemoveRange(checkOriginal);
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
            IEnumerable<Favorites> removeUs = _db.Favorites.Where(s => s.FSong == songId).ToList();

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
            IEnumerable<Favorites> removeUs = _db.Favorites.Where(u => u.FUser == userId).ToList();

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
            Artists removeMe = _db.Artists.Where(ar => ar.ArId == artistId).FirstOrDefault();

            if (removeMe == null)
            {
                return "ERROR: Artist already does not exist.  Operation abandoned.";
            }

            IEnumerable<Albums> artistsAlbums = _db.Albums.Where(ar => ar.AlArtist == artistId).ToList();

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

            IEnumerable<Songs> songsWithNoAlbum = _db.Songs.Where(ar => ar.SArtist == artistId).ToList();

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

        public string RemoveRequest(int requestId)
        {
            PendingRequests removeMe = _db.PendingRequests.Where(r => r.PrId == requestId).FirstOrDefault();

            if (removeMe == null)
            {
                return "ERROR: Request already does not exist.  Operation abandoned.";
            }

            try
            {
                _db.PendingRequests.Remove(removeMe);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Song could not be removed from favorites.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }
    }
}

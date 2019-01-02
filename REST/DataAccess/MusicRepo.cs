using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Add a new album to the database
        public async Task<string> AddAlbum(Library.Albums album)
        {

            if (GetAlbumByNameAndArtist(album.Name, GetArtistByName(album.Artist).Id) != null)
            {
                return "ERROR: Album already exists in the database.  Operation abandoned.";
            }

            Albums newAlbum = Mapper.Map(album);

            newAlbum.AlArtist = GetArtistByName(album.Artist).Id;

            try
            {
                await _db.Albums.AddAsync(newAlbum);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Artist could not be added to the database.  Operation abandoned.  Please contact your system administrator";
            }

            return "true";
        }

        // Add a new artist to the database
        public async Task<string> AddArtist(Library.Artists artist)
        {
            if (GetArtistByName(artist.Name) != null)
            {
                return "ERROR: Artist already exists in the database.  Operation abandoned.";
            }

            Artists newArtist = Mapper.Map(artist);

            try
            {
                await _db.Artists.AddAsync(newArtist);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Artist could not be added to the database.  Operation abandoned.  Please contact your system administrator";
            }
            
            return "true";
        }

        public async Task<string> AddCover(int originalId, int coverId)
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

            try
            {
                await _db.Covers.AddAsync(newCover);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Song could not be added to covers.  Operation abandoned.  Please contact your system administrator";
            }

            // Return true so whatever calls this method can attempt to parse a boolean
            // If boolean can't be parsed, then an error was returned that can be shown to the user
            return "true";
        }

        public async Task<string> AddRequestAsync(Library.PendingRequests request)
        {
            PendingRequests newRequest = Mapper.Map(request);

            try
            {
                await _db.PendingRequests.AddAsync(newRequest);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Request could not be added to the database.  Operation abandoned.  Please contact your system administrator";
            }

            return "true";
        }

        // Add song to database
        public async Task<string> AddSong(Library.Song song)
        {
            if (GetSongByNameAndArtist(song.Name, GetArtistByName(song.Artist).Id) != null)
            {
                return "ERROR: Song already exists in the database.  Operation abandoned.";
            }

            Songs newSong = Mapper.Map(song);

            newSong.SArtist = GetArtistByName(song.Artist).Id;

            try
            {
                await _db.Songs.AddAsync(newSong);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Song could not be added to the database.  Operation abandoned.  Please contact your system administrator";
            }

            return "true";
        }

        public async Task<string> AddSongToAlbum(int songId, int albumId)
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

            try
            {
                await _db.AlbumSongs.AddAsync(songToAlbum);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Song could not be added to the album.  Operation abandoned.  Please contact your system administrator";
            }
            

            // Return true so whatever calls this method can attempt to parse a boolean
            // If boolean can't be parsed, then an error was returned that can be shown to the user
            return "true";
        }

        public async Task<string> AddUser(Library.Users user)
        {
            if (_db.Users.Where(u => u.UName == user.Name).AsNoTracking().FirstOrDefault() != null)
            {
                return "ERROR: User already exists in the database.  Operation abandoned.";
            }

            Users newUser = Mapper.Map(user);

            try
            {
                await _db.Users.AddAsync(newUser);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: User could not be added to the database.  Operation abandoned.  Please contact your system administrator";
            }

            return "true";
        }

        public async Task<string> AddUserFavorite(int userId, int songId)
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

            try
            {
                await _db.Favorites.AddAsync(newFave);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Song could not be added to user's favorites.  Operation abandoned.  Please contact your system administrator";
            }

            // Return true so whatever calls this method can attempt to parse a boolean
            // If boolean can't be parsed, then an error was returned that can be shown to the user
            return "true";
        }

        /* 
         * ---------------------------------------------------
         * ---------| READ - GET INFO FROM DATABASE |---------
         * ---------------------------------------------------
        */

        public async Task<Library.Albums> GetAlbumById(int id)
        {
            try
            {
                Albums awaitMe = await _db.Albums.Where(a => a.AlId == id).AsNoTracking().FirstOrDefaultAsync();

                if (awaitMe == null)
                {
                    return null;
                }

                return Mapper.Map(awaitMe);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public async Task<Library.Albums> GetAlbumByNameAndArtist(string name, int artistId)
        {
            try
            {
                Albums awaitMe = await _db.Albums.Where(a => a.AlName == name).Where(ar => ar.AlArtist == artistId).AsNoTracking().FirstOrDefaultAsync();

                if (awaitMe == null)
                {
                    return null;
                }

                return Mapper.Map(awaitMe);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Library.Albums>> GetAllAlbums()
        {
            try
            {
                IEnumerable<Albums> awaitUs = await _db.Albums.AsNoTracking().ToListAsync();

                if (awaitUs == null)
                {
                    return null;
                }

                return Mapper.Map(awaitUs);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Library.Albums>> GetAllAlbumsByArtist(int artistId)
        {
            try
            {
                IEnumerable<Albums> awaitUs = await _db.Albums.Where(ar => ar.AlArtist == artistId).AsNoTracking().ToListAsync();

                if (awaitUs == null)
                {
                    return null;
                }

                return Mapper.Map(awaitUs);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Library.Albums>> GetAllAlbumsBySong(int songId)
        {
            try
            {
                IEnumerable<AlbumSongs> awaitJunction = await _db.AlbumSongs.Where(s => s.AsSong == songId).ToListAsync();
                IEnumerable<Library.Albums> returnUs = awaitJunction.Select(x => GetAlbumById(x.AsAlbum).Result).ToList();

                if (returnUs == null)
                {
                    return null;
                }

                return returnUs;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Library.Artists>> GetAllArtists()
        {
            try
            {
                IEnumerable<Artists> awaitUs = await _db.Artists.AsNoTracking().ToListAsync();

                if (awaitUs == null)
                {
                    return null;
                }

                return Mapper.Map(awaitUs);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Library.Covers>> GetAllCoversByOriginal(int originalId)
        {
            try
            {
                IEnumerable<Covers> awaitUs = await _db.Covers.Where(o => o.COriginal == originalId).AsNoTracking().ToListAsync();

                if (awaitUs == null)
                {
                    return null;
                }

                return Mapper.Map(awaitUs);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Library.Favorites>> GetAllFavoritesBySong(int songId)
        {
            try
            {
                IEnumerable<Favorites> awaitUs = await _db.Favorites.Where(u => u.FSong == songId).AsNoTracking().ToListAsync();

                if (awaitUs == null)
                {
                    return null;
                }

                return Mapper.Map(awaitUs);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }
       
        // Get all favorites for a user based on User ID
        public async Task<IEnumerable<Library.Favorites>> GetAllFavoritesByUser(int userId)
        {
            try
            {
                IEnumerable<Favorites> awaitUs = await _db.Favorites.Where(u => u.FUser == userId).AsNoTracking().ToListAsync();

                if (awaitUs == null)
                {
                    return null;
                }

                return Mapper.Map(awaitUs);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        // Get all songs - users can search all songs to find new music
        public async Task<IEnumerable<Library.Song>> GetAllSongs()
        {
            try
            {
                IEnumerable<Songs> awaitUs = await _db.Songs.Include(a => a.SArtistNavigation).AsNoTracking().ToListAsync();

                if (awaitUs == null)
                {
                    return null;
                }

                return Mapper.Map(awaitUs);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Library.Song>> GetAllSongsByArtist(int artistId)
        {
            try
            {
                IEnumerable<Songs> awaitUs = await _db.Songs.Where(ar => ar.SArtist == artistId).AsNoTracking().ToListAsync();

                if (awaitUs == null)
                {
                    return null;
                }

                return Mapper.Map(awaitUs);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Library.Users>> GetAllUsers()
        {
            try
            {
                IEnumerable<Users> awaitUs = await _db.Users.AsNoTracking().ToListAsync();

                if (awaitUs == null)
                {
                    return null;
                }

                return Mapper.Map(awaitUs);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public async Task<Library.Artists> GetArtistById(int id)
        {
            try
            {
                Artists awaitMe = await _db.Artists.Where(a => a.ArId == id).AsNoTracking().FirstOrDefaultAsync();

                if (awaitMe == null)
                {
                    return null;
                }

                return Mapper.Map(awaitMe);
            }
            catch (ArgumentNullException)
            {

                return null;
            }
        }

        public async Task<Library.Artists> GetArtistByName(string name)
        {
            try
            {
                Artists awaitMe = await _db.Artists.Where(a => a.ArName == name).AsNoTracking().FirstOrDefaultAsync();

                if (awaitMe == null)
                {
                    return null;
                }

                return Mapper.Map(awaitMe);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public async Task<Library.Covers> GetOriginalByCover(int coverId)
        {
            try
            {
                Covers awaitMe = await _db.Covers.Where(c => c.COriginal == coverId).AsNoTracking().FirstOrDefaultAsync();

                if (awaitMe == null)
                {
                    return null;
                }

                return Mapper.Map(awaitMe);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public async Task<Library.Song> GetSongById(int id)
        {
            try
            {
                Songs awaitMe = await _db.Songs.Where(s => s.SId == id).AsNoTracking().FirstOrDefaultAsync();

                if (awaitMe == null)
                {
                    return null;
                }

                return Mapper.Map(awaitMe);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public async Task<Library.Song> GetSongByNameAndArtist(string name, int artistId)
        {
            try
            {
                Songs awaitMe = await _db.Songs.Where(a => a.SName == name).Where(ar => ar.SArtist == artistId).AsNoTracking().FirstOrDefaultAsync();

                if (awaitMe == null)
                {
                    return null;
                }

                return Mapper.Map(awaitMe);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public async Task<Library.AlbumSongs> GetSongFromAlbum(int songId, int albumId)
        {
            try
            {
                AlbumSongs awaitMe = await _db.AlbumSongs.Where(s => s.AsSong == songId).Where(al => al.AsAlbum == albumId).AsNoTracking().FirstOrDefaultAsync();

                if (awaitMe == null)
                {
                    return null;
                }

                return Mapper.Map(awaitMe);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public async Task<Library.Users> GetUserById(int id)
        {
            try
            {
                Users awaitMe = await _db.Users.Where(u => u.UId == id).AsNoTracking().FirstOrDefaultAsync();

                if (awaitMe == null)
                {
                    return null;
                }

                return Mapper.Map(awaitMe);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public async Task<Library.Users> GetUserByName(string name)
        {
            try
            {
                Users awaitMe = await _db.Users.Where(u => u.UName == name).AsNoTracking().FirstOrDefaultAsync();

                if (awaitMe == null)
                {
                    return null;
                }

                return Mapper.Map(awaitMe);
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

        public async Task<string> UpdateAlbum(Library.Albums libAlbum)
        {
            Albums updateMe = await _db.Albums.Where(al => al.AlName == libAlbum.Name).FirstOrDefaultAsync();

            if (updateMe == null)
            {
                return "ERROR: Album could not be retrieved from database to update.  Operation abandoned.";
            }

            try
            {
                _db.Albums.Update(Mapper.Map(libAlbum));
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Album could not be updated.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public async Task<string> UpdateArtist(Library.Artists libArtist)
        {
            Artists updateMe = await _db.Artists.Where(ar => ar.ArName == libArtist.Name).FirstOrDefaultAsync();

            if (updateMe == null)
            {
                return "ERROR: Artist could not be retrieved from database to update.  Operation abandoned.";
            }      

            try
            {
                _db.Artists.Update(Mapper.Map(libArtist));
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Artist could not be updated.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public async Task<string> UpdateSong(Library.Song libSong)
        {
            Songs updateMe = await _db.Songs.Where(s => s.SName == libSong.Name).FirstOrDefaultAsync();

            if (updateMe == null)
            {
                return "ERROR: Song could not be retrieved from database to update.  Operation abandoned.";
            }

            try
            {
                _db.Songs.Update(Mapper.Map(libSong));
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Song could not be updated.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public async Task<string> UpdateUser(Library.Users libUser)
        {
            Users updateMe = await _db.Users.Where(u => u.UName == libUser.Name).FirstOrDefaultAsync();

            if (updateMe == null)
            {
                return "ERROR: User could not be retrieved from database to update.  Operation abandoned.";
            }

            try
            {
                _db.Users.Update(Mapper.Map(libUser));
                await _db.SaveChangesAsync();
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

        public async Task<string> RemoveAlbum(int albumId)
        {
            Albums removeMe = await _db.Albums.Where(al => al.AlId == albumId).FirstOrDefaultAsync();

            if (removeMe == null)
            {
                return "ERROR: Album already does not exist.  Operation abandoned.";
            }

            string removeSongs = await RemoveAllSongsFromAlbum(albumId);

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
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Album could not be removed.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public async Task<string> RemoveAllSongsFromAlbum(int albumId)
        {
            IEnumerable<AlbumSongs> removeUs = await _db.AlbumSongs.Where(al => al.AsAlbum == albumId).ToListAsync();

            if (removeUs == null)
            {
                return "false";
            }

            try
            {
                _db.AlbumSongs.RemoveRange(removeUs);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Songs could not be removed from album.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public async Task<string> RemoveArtist(int artistId)
        {
            Artists removeMe = await _db.Artists.Where(ar => ar.ArId == artistId).FirstOrDefaultAsync();

            if (removeMe == null)
            {
                return "ERROR: Artist already does not exist.  Operation abandoned.";
            }

            IEnumerable<Albums> artistsAlbums = await _db.Albums.Where(ar => ar.AlArtist == artistId).ToListAsync();

            foreach (var item in artistsAlbums)
            {
                string removeAlbum = await RemoveAlbum(item.AlId);

                try
                {
                    bool.Parse(removeAlbum);
                }
                catch (FormatException)
                {
                    return removeAlbum;
                }
            }

            IEnumerable<Songs> songsWithNoAlbum = await _db.Songs.Where(ar => ar.SArtist == artistId).ToListAsync();

            foreach (var item in songsWithNoAlbum)
            {
                string removeSong = await RemoveSong(item.SId);

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
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Artist could not be removed.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public async Task<string> RemoveFavoritesBySong(int songId)
        {
            IEnumerable<Favorites> removeUs = await _db.Favorites.Where(s => s.FSong == songId).ToListAsync();

            if (removeUs == null)
            {
                return "false";
            }

            try
            {
                _db.Favorites.RemoveRange(removeUs);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Song could not be removed from favorites.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public async Task<string> RemoveFavoritesByUser(int userId)
        {
            IEnumerable<Favorites> removeUs = await _db.Favorites.Where(u => u.FUser == userId).ToListAsync();

            if (removeUs == null)
            {
                return "false";
            }

            try
            {
                _db.Favorites.RemoveRange(removeUs);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: User could not be removed from favorites.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public async Task<string> RemoveRequest(int requestId)
        {
            PendingRequests removeMe = await _db.PendingRequests.Where(r => r.PrId == requestId).FirstOrDefaultAsync();

            if (removeMe == null)
            {
                return "ERROR: Request already does not exist.  Operation abandoned.";
            }

            try
            {
                _db.PendingRequests.Remove(removeMe);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Song could not be removed from favorites.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public async Task<string> RemoveSong(int songId)
        {
            Songs removeMe = await _db.Songs.Where(s => s.SId == songId).FirstOrDefaultAsync();

            if (removeMe == null)
            {
                return "ERROR: Song already does not exist.  Operation abandoned.";
            }

            string removeFromAlbums = await RemoveSongFromAllAlbums(songId);

            try
            {
                bool.Parse(removeFromAlbums);
            }
            catch (FormatException)
            {
                return removeFromAlbums;
            }

            string removeFromCovers = await RemoveSongFromCovers(songId);

            try
            {
                bool.Parse(removeFromCovers);
            }
            catch (FormatException)
            {
                return removeFromCovers;
            }

            string removeFromFavorites = await RemoveFavoritesBySong(songId);

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
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Song could not be removed.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public async Task<string> RemoveSongFromAlbum(int songId, int albumId)
        {
            AlbumSongs removeMe = await _db.AlbumSongs.Where(s => s.AsSong == songId).Where(al => al.AsAlbum == albumId).FirstOrDefaultAsync();

            if (removeMe == null)
            {
                return "ERROR: Song to be removed does not exist on album.  Operation abandoned.";
            }

            try
            {
                _db.AlbumSongs.Remove(removeMe);
                await _db.SaveChangesAsync(); 
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Song could not be removed from album.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public async Task<string> RemoveSongFromAllAlbums(int songId)
        {
            IEnumerable<AlbumSongs> removeUs = await _db.AlbumSongs.Where(al => al.AsSong == songId).ToListAsync();

            if (removeUs == null)
            {
                return "false";
            }

            try
            {
                _db.AlbumSongs.RemoveRange(removeUs);
                await _db.SaveChangesAsync();

                return "true";
            }
            catch (Exception)
            {

                return "CRITICAL ERROR: Song could not be removed from any albums.  Operation abandoned.  Please contact your system administrator immediately.";
            }
        
        }

        public async Task<string> RemoveSongFromCovers(int songId)
        {
            IEnumerable<Covers> checkCovers = await _db.Covers.Where(c => c.CCover == songId).ToListAsync();
            IEnumerable<Covers> checkOriginal = await _db.Covers.Where(c => c.COriginal == songId).ToListAsync();

            if (checkCovers == null && checkOriginal == null)
            {
                return "false";
            }

            try
            {
                _db.Covers.RemoveRange(checkCovers);
                _db.Covers.RemoveRange(checkOriginal);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: Song could not be unlisted as a cover or original version.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }

        public async Task<string> RemoveUser(int userId)
        {
            Users removeMe = await _db.Users.Where(u => u.UId == userId).FirstOrDefaultAsync();

            if (removeMe == null)
            {
                return "ERROR: User already does not exist.  Operation abandoned.";
            }

            string removeFavorites = await RemoveFavoritesByUser(userId);

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
               await  _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return "CRITICAL ERROR: User could not be removed.  Operation abandoned.  Please contact your system administrator immediately.";
            }

            return "true";
        }
    }
}

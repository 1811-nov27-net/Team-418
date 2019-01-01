using System.Collections.Generic;
using Library;

namespace DataAccess
{
    public interface IMusicRepo
    {
        string AddAlbum(Library.Albums album);
        string AddArtist(Library.Artists artist);
        string AddCover(int originalId, int coverId);
        string AddRequest(Library.PendingRequests request);
        string AddSong(Song song);
        string AddSongToAlbum(int songId, int albumId);
        string AddUser(Library.Users user);
        string AddUserFavorite(int userId, int songId);
        Library.Albums GetAlbumById(int id);
        Library.Albums GetAlbumByNameAndArtist(string name, int artistId);
        IEnumerable<Library.Albums> GetAllAlbumsByArtist(int artistId);
        IEnumerable<Song> GetAllSongs();
        IEnumerable<Song> GetAllSongsByArtist(int artistId);
        Library.Artists GetArtistById(int id);
        Library.Artists GetArtistByName(string name);
        IEnumerable<Song> GetCoversByOriginal(int originalId);
        IEnumerable<Library.Favorites> GetFavoritesBySong(int songId);
        IEnumerable<Library.Favorites> GetFavoritesByUser(int userId);
        Song GetOriginalByCover(int coverId);
        Song GetSongById(int id);
        Song GetSongByNameAndArtist(string name, int artistId);
        Library.AlbumSongs GetSongFromAlbum(int songId, int albumId);
        Library.Users GetUserById(int id);
        Library.Users GetUserByName(string name);
        string RemoveAlbum(int albumId);
        string RemoveAllSongsFromAlbum(int albumId);
        string RemoveArtist(int artistId);
        string RemoveFavoritesBySong(int songId);
        string RemoveFavoritesByUser(int userId);
        string RemoveRequest(int requestId);
        string RemoveSong(int songId);
        string RemoveSongFromAlbum(int songId, int albumId);
        string RemoveSongFromAllAlbums(int songId);
        string RemoveSongFromCovers(int songId);
        string RemoveUser(int userId);
        string UpdateAlbum(Library.Albums libAlbum);
        string UpdateArtist(Library.Artists libArtist);
        string UpdateSong(Song libSong);
        string UpdateUser(Library.Users libUser);
        IEnumerable<Library.Artists> GetAllArtists();
    }
}
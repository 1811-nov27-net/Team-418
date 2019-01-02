using System.Collections.Generic;
using System.Threading.Tasks;
using Library;

namespace DataAccess
{
    public interface IMusicRepo
    {
        // CREATE METHODS --------------------------------------------------------
        Task<string> AddAlbum(Library.Albums album);
        Task<string> AddArtist(Library.Artists artist);
        Task<string> AddCover(int originalId, int coverId);
        Task<string> AddFavorite(int userId, int songId);
        Task<string> AddRequest(Library.PendingRequests request);
        Task<string> AddSong(Song song);
        Task<string> AddSongToAlbum(int songId, int albumId);
        Task<string> AddUser(Library.Users user);
        
        // READ METHODS ----------------------------------------------------------
        Task<Library.Albums> GetAlbumById(int id);
        Task<Library.Albums> GetAlbumByNameAndArtist(string name, int artistId);
        Task<IEnumerable<Library.Albums>> GetAllAlbums();
        Task<IEnumerable<Library.Albums>> GetAllAlbumsByArtist(int artistId);
        Task<IEnumerable<Library.Albums>> GetAllAlbumsBySong(int songId);
        Task<IEnumerable<Library.Artists>> GetAllArtists();
        Task<IEnumerable<Library.Covers>> GetAllCoversByOriginal(int originalId);
        Task<IEnumerable<Library.Favorites>> GetAllFavoritesBySong(int songId);
        Task<IEnumerable<Library.Favorites>> GetAllFavoritesByUser(int userId);
        Task<IEnumerable<Library.PendingRequests>> GetAllRequests();
        Task<IEnumerable<Song>> GetAllSongs();
        Task<IEnumerable<Song>> GetAllSongsByArtist(int artistId);
        Task<IEnumerable<Library.Users>> GetAllUsers();
        Task<Library.Artists> GetArtistById(int id);
        Task<Library.Artists> GetArtistByName(string name);
        Task<Library.Covers> GetOriginalByCover(int coverId);
        Task<Library.PendingRequests> GetRequestById(int id);
        Task<Song> GetSongById(int id);
        Task<Song> GetSongByNameAndArtist(string name, int artistId);
        Task<Library.AlbumSongs> GetSongFromAlbum(int songId, int albumId);
        Task<Library.Users> GetUserById(int id);
        Task<Library.Users> GetUserByName(string name);

        // UPDATE METHODS --------------------------------------------------------
        Task<string> UpdateAlbum(Library.Albums libAlbum);
        Task<string> UpdateArtist(Library.Artists libArtist);
        Task<string> UpdateSong(Song libSong);
        Task<string> UpdateUser(Library.Users libUser);

        // DELETE METHODS --------------------------------------------------------
        Task<string> RemoveAlbum(int albumId);
        Task<string> RemoveAllSongsFromAlbum(int albumId);
        Task<string> RemoveArtist(int artistId);
        Task<string> RemoveFavorite(int userId, int songId);
        Task<string> RemoveFavoritesBySong(int songId);
        Task<string> RemoveFavoritesByUser(int userId);
        Task<string> RemoveRequest(int requestId);
        Task<string> RemoveSong(int songId);
        Task<string> RemoveSongFromAlbum(int songId, int albumId);
        Task<string> RemoveSongFromAllAlbums(int songId);
        Task<string> RemoveSongFromCovers(int songId);
        Task<string> RemoveUser(int userId);   
    }
}
using System.Collections.Generic;
using Library;

namespace DataAccess
{
    public interface IMusicRepo
    {
        string AddAlbum(Dictionary<string, string> formData);
        string AddArtist(Dictionary<string, string> formData);
        string AddCover(int originalId, int coverId);
        string AddSong(Dictionary<string, string> formData);
        string AddSongToAlbum(int songId, int albumId);
        string AddUser(string name, bool admin);
        string AddUserFavorite(int userId, int songId);
        Albums GetAlbumById(int id);
        Albums GetAlbumByNameAndArtist(string name, int artistId);
        IEnumerable<Albums> GetAllAlbumsByArtist(int artistId);
        IEnumerable<Song> GetAllSongs();
        IEnumerable<Songs> GetAllSongsByArtist(int artistId);
        Artists GetArtistById(int id);
        Artists GetArtistByName(string name);
        Covers GetCover(int originalId, int coverId);
        IEnumerable<Covers> GetCoversByOriginal(int originalId);
        IEnumerable<Favorites> GetFavoritesBySong(int songId);
        IEnumerable<Favorites> GetFavoritesByUser(int userId);
        Favorites GetOneFavoriteForUser(int userId, int songId);
        Covers GetOriginalByCover(int coverId);
        Songs GetSongById(int id);
        Songs GetSongByNameAndArtist(string name, int artistId);
        AlbumSongs GetSongFromAlbum(int songId, int albumId);
        Users GetUserById(int id);
        Users GetUserByName(string name);
        string RemoveAlbum(int albumId);
        string RemoveAllSongsFromAlbum(int albumId);
        string RemoveArtist(int artistId);
        string RemoveFavoritesBySong(int songId);
        string RemoveFavoritesByUser(int userId);
        string RemoveSong(int songId);
        string RemoveSongFromAlbum(int songId, int albumId);
        string RemoveSongFromAllAlbums(int songId);
        string RemoveSongFromCovers(int songId);
        string RemoveUser(int userId);
        string UpdateArtist(Artists libArtist);
    }
}
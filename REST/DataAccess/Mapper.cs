using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public static class Mapper
    {
        // SONG MAPS ---------------------------------------------------------------
        public static Library.Song Map(Songs song)
        {
            string libLink = null;

            if (song.SLink != null)
            {
                libLink = song.SLink.Substring(song.SLink.IndexOf("=") + 1);
            }

            Library.Song returnMe = new Library.Song
            {
                Id = song.SId,
                Name = song.SName,
                Artist = song.SArtistNavigation.ArName,
                Genre = song.SGenre,
                Length = song.SLength,
                InitialRelease = song.SInitialrelease,
                Cover = song.SCover,
                Link = libLink
            };

            return returnMe;
        }

        public static Songs Map(Library.Song song)
        {
            string dbLink = null;

            if (song.Link != null) 
            {
                dbLink = "https://www.youtube.com/watch?v=" + song.Link;
            }

            Songs returnMe = new Songs
            {
                SName = song.Name,
                // The repo will handle assigning the ID of the artist
                SArtist = 0,
                SGenre = song.Genre,
                SLength = song.Length,
                SInitialrelease = song.InitialRelease,
                SCover = song.Cover,
                SLink = dbLink
            };

            return returnMe;
        }

        public static IEnumerable<Library.Song> Map(IEnumerable<Songs> songs) => songs.Select(Map);
        public static IEnumerable<Songs> Map(IEnumerable<Library.Song> songs) => songs.Select(Map);

        // ARTIST MAPS ---------------------------------------------------------------
        public static Library.Artists Map(Artists artist) => new Library.Artists
        {
            Id = artist.ArId,
            Name = artist.ArName,
            City = artist.ArCity,
            Stateprovince = artist.ArStateprovince,
            Country = artist.ArCountry,
            Formed = artist.ArFormed,
            LatestRelease = artist.ArLatestrelease,
            Website = artist.ArWebsite
        };

        public static Artists Map(Library.Artists artist) => new Artists
        {
            ArName = artist.Name,
            ArCity = artist.City,
            ArStateprovince = artist.Stateprovince,
            ArCountry = artist.Country,
            ArFormed = artist.Formed,
            ArLatestrelease = artist.LatestRelease,
            ArWebsite = artist.Website
        };

        public static IEnumerable<Library.Artists> Map(IEnumerable<Artists> artists) => artists.Select(Map);
        public static IEnumerable<Artists> Map(IEnumerable<Library.Artists> artists) => artists.Select(Map);


        // ALBUM MAPS ---------------------------------------------------------------
        public static Library.Albums Map(Albums album) => new Library.Albums
        {
            Id = album.AlId,
            Name = album.AlName,
            Artist = album.AlArtistNavigation.ArName,
            Release = album.AlRelease,
            Genre = album.AlGenre
        };

        public static Albums Map(Library.Albums album) => new Albums
        {
            AlName = album.Name,
            // The repo will handle assigning the ID of the artist
            AlArtist = 0,
            AlRelease = album.Release,
            AlGenre = album.Genre
        };

        public static IEnumerable<Library.Albums> Map(IEnumerable<Albums> albums) => albums.Select(Map);
        public static IEnumerable<Albums> Map(IEnumerable<Library.Albums> albums) => albums.Select(Map);


        // ALBUM-SONG MAPS ---------------------------------------------------------------
        public static Library.AlbumSongs Map(AlbumSongs albumSong) => new Library.AlbumSongs
        {
            Id = albumSong.AsId,
            Song = albumSong.AsSong,
            Album = albumSong.AsAlbum
        };

        public static AlbumSongs Map(Library.AlbumSongs albumSong) => new AlbumSongs
        {
            AsId = albumSong.Id,
            AsSong = albumSong.Song,
            AsAlbum = albumSong.Album
        };

        public static IEnumerable<Library.AlbumSongs> Map(IEnumerable<AlbumSongs> albumSongs) => albumSongs.Select(Map);
        public static IEnumerable<AlbumSongs> Map(IEnumerable<Library.AlbumSongs> albumSongs) => albumSongs.Select(Map);

        // FAVORITE MAPS ---------------------------------------------------------------
        public static Library.Favorites Map(Favorites favorite) => new Library.Favorites
        {
            Id = favorite.FId,
            User = favorite.FUser,
            Song = favorite.FSong
        };

        public static Favorites Map(Library.Favorites favorite) => new Favorites
        {
            FId = favorite.Id,
            FUser = favorite.User,
            FSong = favorite.Song
        };

        public static IEnumerable<Library.Favorites> Map(IEnumerable<Favorites> favorites) => favorites.Select(Map);
        public static IEnumerable<Favorites> Map(IEnumerable<Library.Favorites> favorites) => favorites.Select(Map);

        // PENDING REQUEST MAPS ---------------------------------------------------------------
        public static Library.PendingRequests Map(PendingRequests request) => new Library.PendingRequests
        {
            Id = request.PrId,
            Artistid = request.PrArtistid,
            Artistname = request.PrArtistname,
            Albumid = request.PrAlbumid,
            Albumname = request.PrAlbumname,
            Songname = request.PrSongname
        };

        public static PendingRequests Map(Library.PendingRequests request) => new PendingRequests
        {
            PrId = request.Id,
            PrArtistid = request.Artistid,
            PrArtistname = request.Artistname,
            PrAlbumid = request.Albumid,
            PrAlbumname = request.Albumname,
            PrSongname = request.Songname
        };

        public static IEnumerable<Library.PendingRequests> Map(IEnumerable<PendingRequests> pendingRequests) => pendingRequests.Select(Map);
        public static IEnumerable<PendingRequests> Map(IEnumerable<Library.PendingRequests> pendingRequests) => pendingRequests.Select(Map);

        // USER MAPS ---------------------------------------------------------------
        public static Library.Users Map(Users user) => new Library.Users
        {
            Id = user.UId,
            Name = user.UName,
            Admin = user.UAdmin
        };

        public static Users Map(Library.Users user) => new Users
        {
            UId = user.Id,
            UName = user.Name,
            UAdmin = user.Admin
        };

        public static IEnumerable<Library.Users> Map(IEnumerable<Users> users) => users.Select(Map);
        public static IEnumerable<Users> Map(IEnumerable<Library.Users> users) => users.Select(Map);

        // COVER MAPS --------------------------------------------------------------
        public static Library.Covers Map(Covers cover) => new Library.Covers
        {
            Id = cover.CId,
            CoverId = cover.CCover,
            OriginalID = cover.COriginal
        };

        public static Covers Map(Library.Covers cover) => new Covers
        {
            CId = cover.Id,
            CCover = cover.CoverId,
            COriginal = cover.OriginalID 
        };

        public static IEnumerable<Library.Covers> Map(IEnumerable<Covers> covers) => covers.Select(Map);
        public static IEnumerable<Covers> Map(IEnumerable<Library.Covers> covers) => covers.Select(Map);
    }
}

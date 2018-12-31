using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public static class Mapper
    {
        // SONG MAPS ---------------------------------------------------------------
        public static Library.Song Map(Songs song) => new Library.Song
        {
            Id = song.SId,
            Name = song.SName,
            Artist = song.SArtist,
            Genre = song.SGenre,
            Length = song.SLength,
            InitialRelease = song.SInitialrelease,
            Cover = song.SCover,
            Link = song.SLink
        };

        public static Songs Map(Library.Song song) => new Songs
        {

        };

        public static IEnumerable<Library.Song> Map(IEnumerable<Songs> songs) => songs.Select(Map);
        public static IEnumerable<Songs> Map(IEnumerable<Library.Song> songs) => songs.Select(Map);

        // ARTIST MAPS ---------------------------------------------------------------
        public static Library.Artists Map(Artists artist) => new Library.Artists
        {

        };

        public static Artists Map(Library.Artists artist) => new Artists
        {

        };

        // ALBUM MAPS ---------------------------------------------------------------
        public static Library.Albums Map(Albums album) => new Library.Albums
        {

        };

        public static Albums Map(Library.Albums album) => new Albums
        {

        };

        // ALBUM-SONG MAPS ---------------------------------------------------------------
        public static Library.AlbumSongs Map(AlbumSongs albumSong) => new Library.AlbumSongs
        {

        };

        public static AlbumSongs Map(Library.AlbumSongs albumSong) => new AlbumSongs
        {

        };

        // FAVORITE MAPS ---------------------------------------------------------------
        public static Library.Favorites Map(Favorites favorite) => new Library.Favorites
        {

        };

        public static Favorites Map(Library.Favorites favorite) => new Favorites
        {

        };

        // PENDING REQUEST MAPS ---------------------------------------------------------------
        public static Library.PendingRequests Map(PendingRequests request) => new Library.PendingRequests
        {

        };

        public static PendingRequests Map(Library.PendingRequests request) => new PendingRequests
        {

        };

        // USER MAPS ---------------------------------------------------------------
        public static Library.Users Map(Users user) => new Library.Users
        {

        };

        public static Users Map(Library.Users user) => new Users
        {

        };

    }
}

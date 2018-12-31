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
            Genre = song.SGenre,
        };

        public static Songs Map(Library.Song song) => new Songs
        {

        };

        public static IEnumerable<Library.Song> Map(IEnumerable<Songs> songs) => songs.Select(Map);
        public static IEnumerable<Songs> Map(IEnumerable<Library.Song> songs) => songs.Select(Map);

        // ARTIST MAPS ---------------------------------------------------------------
        public static Library.Artists Map(Artists artist) => new Library.Artists
        {
            Id = song.SId,
            Name = song.SName,
            Genre = song.SGenre,
        };

        public static Artists Map(Library.Artists artist) => new Artists
        {

        };

        // ALBUM MAPS ---------------------------------------------------------------
        public static Library.Albums Map(Albums album) => new Library.Albums
        {
            Id = song.SId,
            Name = song.SName,
            Genre = song.SGenre,
        };

        public static Albums Map(Library.Albums album) => new Albums
        {

        };

        // ALBUM-SONG MAPS ---------------------------------------------------------------
        public static Library.AlbumSongs Map(AlbumSongs albumSong) => new Library.AlbumSongs
        {
            Id = song.SId,
            Name = song.SName,
            Genre = song.SGenre,
        };

        public static AlbumSongs Map(Library.AlbumSongs albumSong) => new AlbumSongs
        {

        };

        // COVER MAPS ---------------------------------------------------------------
        public static Library.Covers Map(Covers cover) => new Library.Covers
        {
            Id = song.SId,
            Name = song.SName,
            Genre = song.SGenre,
        };

        public static Covers Map(Library.Covers cover) => new Covers
        {

        };

        // FAVORITE MAPS ---------------------------------------------------------------
        public static Library.Favorites Map(Favorites favorite) => new Library.Favorites
        {
            Id = song.SId,
            Name = song.SName,
            Genre = song.SGenre,
        };

        public static Favorites Map(Library.Favorites favorite) => new Favorites
        {

        };

        // PENDING REQUEST MAPS ---------------------------------------------------------------
        public static Library.PendingRequests Map(PendingRequests request) => new Library.PendingRequests
        {
            Id = song.SId,
            Name = song.SName,
            Genre = song.SGenre,
        };

        public static PendingRequests Map(Library.PendingRequests request) => new PendingRequests
        {

        };

        // USER MAPS ---------------------------------------------------------------
        public static Library.Users Map(Users user) => new Library.Users
        {
            Id = song.SId,
            Name = song.SName,
            Genre = song.SGenre,
        };

        public static Users Map(Library.Users user) => new Users
        {

        };

    }
}

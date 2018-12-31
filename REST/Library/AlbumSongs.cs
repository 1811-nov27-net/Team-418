using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class AlbumSongs
    {
        // Id of Albumsongs set in Db
        public int Id { get; set; }
        // Id's of songs in the album
        public int Song { get; set; }
        // Id's of albums in Db
        public int Album { get; set; }
    }
}

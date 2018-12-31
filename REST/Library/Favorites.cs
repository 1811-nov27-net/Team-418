using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    class Favorites
    {
        public int Id { get; set; }

        // foreign key to username table
        public int UserId { get; set; }

        // foreign key to songs table
        public int SongId { get; set; }
    }
}

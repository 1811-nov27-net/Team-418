using System;
using System.Collections.Generic;

namespace DataAccess
{
    public partial class AlbumSongs
    {
        public int AsId { get; set; }
        public int AsSong { get; set; }
        public int AsAlbum { get; set; }

        public virtual Albums AsAlbumNavigation { get; set; }
        public virtual Songs AsSongNavigation { get; set; }
    }
}

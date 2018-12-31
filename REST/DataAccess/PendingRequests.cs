using System;
using System.Collections.Generic;

namespace DataAccess
{
    public partial class PendingRequests
    {
        public int PrId { get; set; }
        public int? PrArtistid { get; set; }
        public string PrArtistname { get; set; }
        public int? PrAlbumid { get; set; }
        public string PrAlbumname { get; set; }
        public string PrSongname { get; set; }

        public virtual Albums PrAlbum { get; set; }
        public virtual Artists PrArtist { get; set; }
    }
}

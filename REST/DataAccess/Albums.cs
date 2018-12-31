using System;
using System.Collections.Generic;

namespace DataAccess
{
    public partial class Albums
    {
        public Albums()
        {
            AlbumSongs = new HashSet<AlbumSongs>();
            PendingRequests = new HashSet<PendingRequests>();
        }

        public int AlId { get; set; }
        public string AlName { get; set; }
        public int AlArtist { get; set; }
        public DateTime? AlRelease { get; set; }
        public string AlGenre { get; set; }

        public virtual Artists AlArtistNavigation { get; set; }
        public virtual ICollection<AlbumSongs> AlbumSongs { get; set; }
        public virtual ICollection<PendingRequests> PendingRequests { get; set; }
    }
}

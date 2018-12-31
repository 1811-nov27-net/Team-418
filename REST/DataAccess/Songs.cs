using System;
using System.Collections.Generic;

namespace DataAccess
{
    public partial class Songs
    {
        public Songs()
        {
            AlbumSongs = new HashSet<AlbumSongs>();
            CoversCCoverNavigation = new HashSet<Covers>();
            CoversCOriginalNavigation = new HashSet<Covers>();
            Favorites = new HashSet<Favorites>();
        }

        public int SId { get; set; }
        public string SName { get; set; }
        public int SArtist { get; set; }
        public TimeSpan? SLength { get; set; }
        public string SGenre { get; set; }
        public DateTime? SInitialrelease { get; set; }
        public bool? SCover { get; set; }
        public string SLink { get; set; }

        public virtual Artists SArtistNavigation { get; set; }
        public virtual ICollection<AlbumSongs> AlbumSongs { get; set; }
        public virtual ICollection<Covers> CoversCCoverNavigation { get; set; }
        public virtual ICollection<Covers> CoversCOriginalNavigation { get; set; }
        public virtual ICollection<Favorites> Favorites { get; set; }
    }
}

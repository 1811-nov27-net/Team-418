using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library
{
    class Artists
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // referece to a Locations table possibly?

        public DateTime Formed { get; set; } // again, we only need the Date, not the Time of when the band was formed

        // LatestRelease will pull an artist's latest album through the Album table
        // need an AlbumID, or name assigned LatestRelease, waiting on DB finalization

        public string Website { get; set; } // string holding URL to artist's Website (if it exists)
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library
{
    public class Artists
    {
        // Id of Artists (set in DB)
        public int Id { get; set; }
        // Name of artist
        public string Name { get; set; }
        // City where Artist originated from
        public string City { get; set; }
        // State/province where artist originated from
        public string Stateproviince { get; set; }
        // Country of origin
        public string Country { get; set; }
        // date when artist/group formed
        // set as new DateTime(int year, int day, int month)
        public DateTime? Formed { get; set; }
        // Date of artist's most recent release in DB
        public DateTime? LatestRelease { get; set; }
        // Url to artist's website (if possible)
        public string Website { get; set; }
    }
}

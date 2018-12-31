using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class AlbumViewModel
    {
        // Album
        // name
        // artist
        // release date
        // genre

        public string Name { get; set; }
        public ArtistViewModel Artist { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
    }
}

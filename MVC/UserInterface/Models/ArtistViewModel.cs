using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class ArtistViewModel
    {
        // Artist
        // name
        // city
        // date formed
        // date of latest release
        // website
        public string Name { get; set; }
        public List<AlbumViewModel> Albums { get; set; }
        public DateTime DateFormed { get; set; }
        public DateTime LatestReleaseDate { get; set; }
        public string Website { get; set; }

        public List<SongViewModel> Songs
        {
            get
            {
                List<SongViewModel> songs = new List<SongViewModel>();
                foreach(var album in Albums)
                {
                    songs.AddRange(album.Songs);
                }
                return songs;
            }
        }

        public ArtistViewModel()
        {
            Albums = new List<AlbumViewModel>();
        }

    }
}

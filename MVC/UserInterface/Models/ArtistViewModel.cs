using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class ArtistViewModel
    {
        public static List<ArtistViewModel> Artists = new List<ArtistViewModel>();

        public string Name { get; set; }
        public List<AlbumViewModel> Albums { get; set; }
        public DateTime DateFormed { get; set; }
        public DateTime LatestReleaseDate { get; set; }
        public string Website { get; set; }

        public int Id { get; set; }

        public static int NextId
        {
            get
            {
                int highestId = 0;
                if (Artists.Count > 0)
                    highestId = Artists.Max(s => s.Id);

                return highestId + 1;
            }
        }
        public static ArtistViewModel GetById(int id)
        {
            return Artists.FirstOrDefault(a => a.Id == id);
        }

        public ArtistViewModel()
        {
            Id = NextId;
            Albums = new List<AlbumViewModel>();
            Artists.Add(this);
        }

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


    }
}

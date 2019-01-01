using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class AlbumViewModel
    {
        public static List<AlbumViewModel> Albums = new List<AlbumViewModel>();

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public ArtistViewModel Artist { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public List<SongViewModel> Songs { get; set; }
        #endregion

        public AlbumViewModel()
        {
            Songs = new List<SongViewModel>();
            Id = NextId;
            Albums.Add(this);
        }

        public static int NextId
        {
            get
            {
                int highestId = 0;
                if (Albums.Count > 0)
                    highestId = Albums.Max(s => s.Id);

                return highestId + 1;
            }
        }
        public static AlbumViewModel GetById(int id)
        {
            return Albums.FirstOrDefault(a => a.Id == id);
        }


    }
}

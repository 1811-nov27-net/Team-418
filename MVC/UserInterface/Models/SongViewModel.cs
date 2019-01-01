using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class SongViewModel
    {
        public enum SortMethod
        {
            Name = 1,
            Artist = 2,
            Album = 4
        }
        // TODO: Update to sql db
        // Storing local song db for now.
        public static List<SongViewModel> Songs = new List<SongViewModel>();
        // TODO: Implement bit field logic for funzies
        public static int sortField;
        public static bool NameSort, ArtistSort, AlbumSort;


        public SongViewModel()
        {
            Id = NextId;
            Songs.Add(this);
        }

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public TimeSpan? PlayTime { get; set; }
        public string Link { get; set; }
        // genre
        // release
        // cover
        public string Genre { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public bool? Cover { get; set; }

        public static int NextId
        {
            get
            {
                int highestId = 0;
                if (Songs.Count > 0)
                    highestId = Songs.Max(s => s.Id);

                return highestId + 1;

            }
        }

        #endregion

        #region Methods

        public static SongViewModel GetById(int id)
        {
            return Songs.FirstOrDefault(s => s.Id == id);
        }

        private static void SortSongs(SortMethod sort, bool ascending)
        {
            switch (sort)
            {
                case SortMethod.Name:
                    {
                        if (ascending)
                            Songs = Songs.OrderBy(s => s.Name).ToList();
                        else
                            Songs = Songs.OrderByDescending(s => s.Name).ToList();
                        break;
                    }

                case SortMethod.Artist:
                    {
                        if (ascending)
                            Songs = Songs.OrderBy(s => s.Artist).ToList();
                        else
                            Songs = Songs.OrderByDescending(s => s.Artist).ToList();
                        break;
                    }
                case SortMethod.Album:
                    {
                        if (ascending)
                            Songs = Songs.OrderBy(s => s.Album).ToList();
                        else
                            Songs = Songs.OrderByDescending(s => s.Album).ToList();
                        break;
                    }
                default:
                    break;
            }
        }

        public static void UpdateSongSort(SortMethod sort)
        {
            bool ascending = false;
            switch (sort)
            {
                case SortMethod.Name:
                    {
                        NameSort = !NameSort;
                        ascending = NameSort;
                        break;
                    }

                case SortMethod.Artist:
                    {
                        ArtistSort = !ArtistSort;
                        ascending = ArtistSort;
                        break;
                    }
                case SortMethod.Album:
                    {
                        AlbumSort = !AlbumSort;
                        ascending = AlbumSort;
                        break;
                    }
                default:
                    break;
            }

            SortSongs(sort, ascending);

            // TODO: Implement bitfield logic
            //if ((sortField) == 0)
            //    SortSongs(sort, ascending: false);
            //else
            //    SortSongs(sort, ascending: true);
        }



        #endregion

    }
}

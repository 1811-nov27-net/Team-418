using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UserInterface.Controllers;

namespace UserInterface.Models
{
    public class SongViewModel
    {
        #region Static Methods / Properties
        private static List<SongViewModel> _songs = new List<SongViewModel>();
        public static List<SongViewModel> Songs
        {
            get
            {
                return _songs;
            }
            set
            {
                _songs = value;
            }
        }
        public static SongViewModel GetByName(string name)
        {
            return Songs.FirstOrDefault(s => s.Name == name);
        }
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
        public static async Task SyncSongsAsync(HttpClient client)
        {
            HttpRequestMessage request = AServiceController.CreateRequestToServiceNoCookie(HttpMethod.Get, "https://localhost:44376/api/song");
            HttpResponseMessage response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                // Error. 
                return;
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            Songs.Clear();
            Songs = JsonConvert.DeserializeObject<List<SongViewModel>>(responseBody);

            return;
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album  { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan PlayTime { get; set; }
        public string Link { get; set; }
        public string Genre { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public bool? Cover { get; set; }
        public ArtistViewModel ArtistModel { get => ArtistViewModel.GetByName(Artist); }
        public AlbumViewModel  AlbumModel  { get => AlbumViewModel.GetByName(Album); }
        #endregion

        #region Constructors
        public SongViewModel()
        {
            Id = NextId;
            _songs.Add(this);
        }
        #endregion

        #region Sorting Stuff
        public enum SortMethod
        {
            Name = 1,
            Artist = 2,
            Album = 4
        }
        // TODO: Implement bit field logic for funzies
        public static int sortField;
        public static bool NameSort, ArtistSort, AlbumSort;
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

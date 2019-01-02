using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UserInterface.Controllers;

namespace UserInterface.Models
{
    public class ArtistViewModel
    {
        #region Static Methods / Properties
        public static List<ArtistViewModel> Artists = new List<ArtistViewModel>();
        public static ArtistViewModel GetByName(string name)
        {
            return Artists.FirstOrDefault(a => a.Name == name);
        }
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
        public static async Task SyncArtistAsync(HttpClient client)
        {
            HttpRequestMessage request = AServiceController.CreateRequestToServiceNoCookie(HttpMethod.Get, "https://localhost:44376/api/artist");
            HttpResponseMessage response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                // Error. 
                return;
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            Artists.Clear();
            Artists = JsonConvert.DeserializeObject<List<ArtistViewModel>>(responseBody);

            return;
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SongViewModel> Songs { get => SongViewModel.Songs.Where(s => s.Artist == Name).ToList(); }
        public List<AlbumViewModel> Albums { get => AlbumViewModel.Albums.Where(a => a.Artist == Name).ToList(); }
        public DateTime LatestReleaseDate { get; set; }
        public DateTime DateFormed { get; set; }
        public string Website { get; set; }
        #endregion

        #region Constructors
        public ArtistViewModel()
        {
            Id = NextId;
            Artists.Add(this);
        }
        #endregion
    }
}

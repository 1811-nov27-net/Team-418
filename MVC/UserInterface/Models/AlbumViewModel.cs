using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UserInterface.Controllers;

namespace UserInterface.Models
{
    public class AlbumViewModel
    {
        #region Static Methods / Properties
        public static List<AlbumViewModel> Albums = new List<AlbumViewModel>();
        public static AlbumViewModel GetByName(string name)
        {
            return Albums.FirstOrDefault(a => a.Name == name);
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
        public static async Task SyncAlbumsAsync(HttpClient client)
        {
            HttpRequestMessage request = AServiceController.CreateRequestToServiceNoCookie(HttpMethod.Get, "https://localhost:44376/api/album");
            HttpResponseMessage response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                // Error. 
                return;
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            Albums.Clear();
            Albums = JsonConvert.DeserializeObject<List<AlbumViewModel>>(responseBody);

            return;
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public ArtistViewModel ArtistModel { get => ArtistViewModel.GetByName(Artist); }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public List<SongViewModel> Songs { get => SongViewModel.Songs.Where(s => s.Album == Name).ToList(); }
        #endregion

        #region Constructors
        public AlbumViewModel()
        {
            Id = NextId;
            Albums.Add(this);
        }
        #endregion
        
    }
}

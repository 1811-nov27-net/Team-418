using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UserInterface.Controllers;

namespace UserInterface.Models
{
    public class UserViewModel
    {
        #region Static Methods / Properties
        public static List<UserViewModel> Users = new List<UserViewModel>();
        public static UserViewModel CurrentUser;
        public static int NextId
        {
            get
            {
                int highestId = 0;
                if (Users.Count > 0)
                    highestId = Users.Max(s => s.Id);

                return highestId + 1;
            }
        }
        public static UserViewModel GetById(int id)
        {
            return Users.FirstOrDefault(u => u.Id == id);
        }
        public static UserViewModel GetByName(string name)
        {
            return Users.FirstOrDefault(u => u.Name == name);
        }
        public static async Task SyncUsersAsync(HttpClient client)
        {
            HttpRequestMessage request = AServiceController.CreateRequestToServiceNoCookie(HttpMethod.Get, "https://localhost:44376/api/user");
            HttpResponseMessage response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                // Error. 
                return;
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            CurrentUser = null;
            Users.Clear();
            Users = JsonConvert.DeserializeObject<List<UserViewModel>>(responseBody);
        }
        #endregion  

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Admin { get; set; }
        public List<string> FavoriteSongs { get; set; }
        public List<SongViewModel> FavoriteSongModels
        {
            get
            {
                List<SongViewModel> songs = new List<SongViewModel>();
                foreach(string song in FavoriteSongs)
                {
                    songs.Add(SongViewModel.GetByName(song));
                }
                return songs;
            }
        }
        #endregion

        #region Constructors
        public UserViewModel()
        {
            Id = NextId;
            FavoriteSongs = new List<string>();
            Users.Add(this);
        }
        #endregion

        #region Methods
        public void AddFavoriteById(int id)
        {
            SongViewModel song = SongViewModel.GetById(id);
            if (FavoriteSongs.Contains(song.Name))
                return;

            FavoriteSongs.Add(song.Name);
        }
        #endregion
    }
}

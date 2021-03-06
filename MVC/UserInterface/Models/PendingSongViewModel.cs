﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UserInterface.Controllers;

namespace UserInterface.Models
{
    public class PendingSongViewModel
    {
        #region Static Methods / Properties
        public static List<PendingSongViewModel> PendingSongs = new List<PendingSongViewModel>();
        public static PendingSongViewModel GetByName(string name)
        {
            return PendingSongs.FirstOrDefault(s => s.Name == name);
        }
        public static PendingSongViewModel GetById(int id)
        {
            return PendingSongs.FirstOrDefault(s => s.Id == id);
        }
        public static int NextId
        {
            get
            {
                int highestId = 0;
                if (PendingSongs.Count > 0)
                    highestId = PendingSongs.Max(s => s.Id);

                return highestId + 1;

            }
        }
        // its late.
        class dbpendingsong
        {
            public string artistname { get; set; }
            public string albumname { get; set; }
            public string songname { get; set; }
        }
        public static async Task SyncPendingSongsAsync(HttpClient client)
        {
            HttpRequestMessage request = AServiceController.CreateRequestToServiceNoCookie(HttpMethod.Get, "https://localhost:44376/api/request");
            HttpResponseMessage response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                // Error. 
                return;
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            PendingSongs.Clear();
            var dbsongs = JsonConvert.DeserializeObject<List<dbpendingsong>>(responseBody);
            foreach(var song in dbsongs)
            {
                PendingSongs.Add(new PendingSongViewModel
                {
                    Name = song.songname,
                    Artist = song.artistname,
                    Album = song.albumname
                });
            }

            return;
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        #endregion

        #region Constructors
        public PendingSongViewModel()
        {
            Id = NextId;
        }
        #endregion
    }
}

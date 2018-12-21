using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Library
{
    public class Song
    {
        public static List<Song> Songs = new List<Song>();

        public static Song GetById()
        {

        }

        public Song()
        {
            Songs.Add(this);
            Id = NextId;
        }

        private static int currentId = 0;

        public int NextId
        {
            get
            {
                int highest = Songs.Max(s => s.Id);
                return ++currentId;
            }
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // key to artists, may be multiple artists for one song

        public string Genre { get; set; }

        //public string Length { get; set; } // length of song(?)

        public DateTime InitialRelease { get; set; } // we only need the Date, not the time
        // DateTime has a struct Date that allows us to access only the Date

        public string SongUrl { get; set; } // URL link to song, saved as a string
    }
}

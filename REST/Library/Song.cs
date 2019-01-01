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
        // Id of song (assigned in DB)
        public int Id { get; set; }
        // Name of the song
        public string Name { get; set; }
        // Name of Artist(s)
        public string Artist { get; set; }
        // Genre of song
        public string Genre { get; set; }
        // Length/duration of the song
        public TimeSpan? Length { get; set; }
        // Release date of song, set as New DateTime(int year, int day, int month)
        public DateTime? InitialRelease { get; set; }
        // Is this song a cover?
        public bool? Cover { get; set; }
        // URL link for song (using mainly youtube, so we use the unique identifier
        // attached at the end of the link, after "watch?v="
        public string Link { get; set; }
    }
}

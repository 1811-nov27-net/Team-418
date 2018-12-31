using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library
{
    public class Albums
    {
        // Album Id set in DB
        public int Id { get; set; }
        // Name of album
        public string Name { get; set; }
        // Id of Artist (foreign key reference)
        public int Artist { get; set; }
        // Date of album release, set as
        // new DateTime(int year, int day, int month)
        public DateTime Release { get; set; }
        // Genre of album
        public string Genre { get; set; }
    }
}

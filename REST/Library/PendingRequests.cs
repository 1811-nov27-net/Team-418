using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class PendingRequests
    {
        // Id of pending requests set in DB
        public int Id { get; set; }
        // Artist Id
        public int? Artistid { get; set; }
        // Artist name
        public string Artistname { get; set; }
        // Album Id
        public int? Albumid { get; set; }
        // Album name
        public string Albumname { get; set; }
        // Song name
        public string Songname { get; set; }
    }
}

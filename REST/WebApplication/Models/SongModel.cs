using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class SongModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Artist { get; set; }
        public string Album { get; set; }
        public TimeSpan? PlayTime { get; set; }
        public string Link { get; set; }
        public string Genre { get; set; }
        public DateTime? Release { get; set; }
        public bool? Cover { get; set; }
    }
}

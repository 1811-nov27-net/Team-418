using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class ArtistModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Stateprovice { get; set; }
        public string Country { get; set; }
        public DateTime? Formed { get; set; }
        public DateTime? LatestRelease { get; set; }
        public string Website { get; set; }
    }
}

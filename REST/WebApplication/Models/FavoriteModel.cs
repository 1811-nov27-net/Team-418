using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class FavoriteModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string SongName { get; set; }
        public string SongArtist { get; set; }
    }
}

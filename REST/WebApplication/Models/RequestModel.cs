using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class RequestModel
    {
        public int Id { get; set; }
        public int? Artistid { get; set; }
        public string Artistname { get; set; }
        public int? Albumid { get; set; }
        public string Albumname { get; set; }
        public string Songname { get; set; }
    }
}

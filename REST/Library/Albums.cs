using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library
{
    class Albums
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int ArtistId { get; set; }

        public DateTime Release { get; set; }

        public string Genre { get; set; }
    }
}

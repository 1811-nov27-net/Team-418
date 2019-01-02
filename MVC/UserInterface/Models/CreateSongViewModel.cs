using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class CreateSongViewModel
    {
        #region Properties
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan Length { get; set; }
        public string Link { get; set; }
        public string Genre { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public bool Cover { get; set; }
        #endregion
    }
}

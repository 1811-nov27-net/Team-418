using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public static class Mapper
    {
        public static Library.Song Map(Songs song) => new Library.Song
        {
            Id = song.SId,
            Name = song.SName,
            Genre = song.SGenre,
        };
    }
}

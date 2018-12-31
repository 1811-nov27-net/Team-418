using System;
using System.Collections.Generic;

namespace DataAccess
{
    public partial class Favorites
    {
        public int FId { get; set; }
        public int FSong { get; set; }
        public int FUser { get; set; }

        public virtual Songs FSongNavigation { get; set; }
        public virtual Users FUserNavigation { get; set; }
    }
}

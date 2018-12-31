using System;
using System.Collections.Generic;

namespace DataAccess
{
    public partial class Covers
    {
        public int CId { get; set; }
        public int CCover { get; set; }
        public int COriginal { get; set; }

        public virtual Songs CCoverNavigation { get; set; }
        public virtual Songs COriginalNavigation { get; set; }
    }
}

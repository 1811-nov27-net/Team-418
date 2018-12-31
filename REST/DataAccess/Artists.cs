using System;
using System.Collections.Generic;

namespace DataAccess
{
    public partial class Artists
    {
        public Artists()
        {
            Albums = new HashSet<Albums>();
            PendingRequests = new HashSet<PendingRequests>();
            Songs = new HashSet<Songs>();
        }

        public int ArId { get; set; }
        public string ArName { get; set; }
        public string ArCity { get; set; }
        public string ArStateprovince { get; set; }
        public string ArCountry { get; set; }
        public DateTime? ArFormed { get; set; }
        public DateTime? ArLatestrelease { get; set; }
        public string ArWebsite { get; set; }

        public virtual ICollection<Albums> Albums { get; set; }
        public virtual ICollection<PendingRequests> PendingRequests { get; set; }
        public virtual ICollection<Songs> Songs { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace DataAccess
{
    public partial class Users
    {
        public Users()
        {
            Favorites = new HashSet<Favorites>();
        }

        public int UId { get; set; }
        public string UName { get; set; }
        public bool UAdmin { get; set; }

        public virtual ICollection<Favorites> Favorites { get; set; }
    }
}

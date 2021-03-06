﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class Favorites
    {
        // Favorites Id set in DB
        public int Id { get; set; }
        // foreign key to username table
        public int User { get; set; }
        // foreign key to songs table
        public int Song { get; set; }
    }
}

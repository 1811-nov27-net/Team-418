using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Library
{
    public class Users
    {
        // User ID set in DB
        public int Id { get; set; }
        // Username used to log into the web app
        // passwords are dealt with in a separate DB on the MVC
        public string Name { get; set; }
        // Boolean to check if the user is an admin
        public bool Admin { get; set; }
    }
}

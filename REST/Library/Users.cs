using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Library
{
    public class Users
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        //public string Password { get; set; } // waiting on final DB implementation
        //public bool Admin { get; set; } // waiting on final DB implementation
    }
}

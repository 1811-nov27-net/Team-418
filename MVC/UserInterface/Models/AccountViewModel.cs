using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class AccountViewModel
    {
        [Required(ErrorMessage = "User name required.")]
        [DataType(DataType.Text)]
        [Display(Name = "User Name", Prompt = "Enter User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long!")]
        [Display(Name = "Password", Prompt = "Enter Password", Description = "Password must consist of letters and numbers and be at least 6 characters long.")]
        public string Password { get; set; }

        public bool Admin { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class AccountCreationViewModel : AccountViewModel
    {
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirmed password must match!")]
        [Display(Name = "Password", Prompt = "Enter Password")]
        public string ConfirmPassword { get; set; }
    }
}

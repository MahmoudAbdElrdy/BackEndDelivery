using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.BAL.Models
{
    public class ResetPasswordVm
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordMobile 
    {
       
        public string PhoneNmber { get; set; } 
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }
    }

}

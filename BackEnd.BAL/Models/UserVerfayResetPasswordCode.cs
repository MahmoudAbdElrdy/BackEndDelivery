using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.BAL.Models
{
    public class UserVerfayResetPasswordCode
    {
        [EmailAddress]
        public string Email { get; set; }
        public int resetCode { get; set; }
    }
}

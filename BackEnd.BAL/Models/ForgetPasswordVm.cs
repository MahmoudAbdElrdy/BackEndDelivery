using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.BAL.Models
{
    public class ForgetPasswordEmail 
    {
       
        public string Email { get; set; }
    }
    public class ForgetPasswordPhone
    {
         
        public string PhoneNumber { get; set; }
    }
}

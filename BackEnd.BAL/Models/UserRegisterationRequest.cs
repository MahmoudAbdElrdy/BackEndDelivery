using BackEnd.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.BAL.Models
{
  public class UserRegisterationRequest
  {  
   // public string UserName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
    [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
    public string ConfirmPassword { get; set; }
    public string Roles { get; set; }
    public string PhoneNumber{ get; set; }
   public string FullName { get; set; }
   public string UserName { get; set; }
   public string Id { get; set; }
   public string RoleName { get; set; }
    }
    public class UserRegisteration 
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }
        public int UserType{ get; set; }
        public string PhoneNumber { get; set; }
    }
    public class UpdateUser 
    {
        // public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Roles { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Id { get; set; }
    }
    public class Certified//تعليق واعتماد
    {
        public string Id { get; set; }
        public bool? MemberCertified { get; set; }//اعتماد العضوية
     
    }
    public class Verification //توثيق وعدم توثيق
    {
        public string Id { get; set; }
        public bool? MemberVerification { get; set; }//توثيق العضوية 
    }
    public class Status//حظر وسماح 
    {
    public string Id { get; set; }
    public bool? StatusType { get; set; }
}

}

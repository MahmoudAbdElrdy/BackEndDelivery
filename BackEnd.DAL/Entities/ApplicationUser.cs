using BackEnd.Helpers.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int verficationCode { get; set; }
        public bool? confirmed { get; set; }
        public bool? confirmedMobile { get; set; } 
        public bool? MemberCertified{ get; set; }//اعتماد العضوية
        public bool? MemberVerification{ get; set; }//توثيق العضوية 
        public bool? StatusType { get; set; }
        public string FullName { get; set; }
        public int? resetPasswordCode { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
     
    }
    

    public class ApplicationRole : IdentityRole
    {
       public UserType? UserType { get; set; }
       public string Permissions { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
    public class ApplicationUserRole : IdentityUserRole<string>
    {
       // public string Id { get; set; }
        //[ForeignKey("User")]
        public virtual ApplicationUser User { get; set; }
      //  [ForeignKey("Role")]
        public virtual ApplicationRole Role { get; set; }
    }
}

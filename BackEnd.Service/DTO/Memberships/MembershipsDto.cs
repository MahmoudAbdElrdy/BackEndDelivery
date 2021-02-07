using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.DTO
{
   public class MembershipsDto
    {
        public bool? MemberCertified { get; set; }//اعتماد العضوية=false=True وتعليق العضوية
        public bool? MemberVerification { get; set; }//توثيق العضوية 
        public bool? StatusType { get; set; }//سماح وحظر
        public string UserName { get; set; }
        public string FullName { get; set; } 
        public string Email{ get; set; }
        public int? UserType { get; set; }
        public string Id { get; set; }
    }
    public class ApplicationRoleDto
    {
        public int? UserType { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public List<int> Permissions { get; set; }
      //  public string UserName { get; set; }
        
    }
    public class UserDto
    {

    }
}

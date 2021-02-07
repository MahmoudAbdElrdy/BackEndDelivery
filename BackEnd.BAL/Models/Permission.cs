using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public  class Permission
    {
        public string Id { get; set; }
       public string RoleName{ get; set; } 
       public List<int> Permissions { get; set; }
    }
}

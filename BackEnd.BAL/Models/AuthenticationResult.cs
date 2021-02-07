using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.BAL.Models
{
  public class AuthenticationResult
  {
    public string Token { get; set; }
   // public Boolean Success { get; set; }
   // public IEnumerable<string> Errors { get; set; }
    public string Message { get; set; } = "";
   public int Code { get; set; } = 400;
   public string Role
        {
            get; set;
        }
   public List<int> Permissions { get; set; }
        public string UserType { get; set; }
        public string ApplicationUserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.BAL.Models
{
  public class AuthFaildResponse
    {
        public string Message { get; set; } = "";
        public int Code { get; set; } = 400;

        public dynamic Data { get; set; }
      //  public IEnumerable<string> Errors { get; set; }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.BAL.ApiRoute
{
  public static class ApiRoute
  {
    public const string Root= "api";
    public const string Version = "v1";
    public const string Base =Root+"/"+Version;

    public static class Identity
    {
      public const string Login = Root + "Identity/Login";
      public const string Register = Root + "/Identity/Register";
      public const string Roles = Root + "/Identity/Roles";
    }


  }
}

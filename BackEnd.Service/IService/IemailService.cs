using BackEnd.BAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
  public interface IemailService
  {
    Task<Boolean> sendVerfication(int verficationCode,string Email);
    Task<Boolean> sendResetPasswordCode(int verficationCode, string Email);
   IResponseDTO SendEmail(string to, string Subject, string body);
    }
}

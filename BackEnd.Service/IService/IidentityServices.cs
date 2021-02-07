using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Service.ISercice
{
  public interface IidentityServices
  {
         Task<AuthenticationResult> RegisterAsync(string FirstName, string LastName, string Email, string Password, int UserType, string PhoneNumber);
    Task<IResponseDTO> RegisterAdminAsync(string FirstName, string LastName,string Email,string Password, string Roles,string PhoneNumber);
    Task<AuthenticationResult> LoginAsync(string Email, string Password);
    Task<Result> verfayUser(UserVerfayRequest request);
    IResponseDTO GetAllUserAdmin();
    IResponseDTO CreatePermissions(string RoleName, List<int> Permissions);
    IResponseDTO UpdatePermissions(string Id, string RoleName, List<int> Permissions);
    Task<Boolean> sendVerficationToEMail(string Email);
   IResponseDTO Delete(string id);
   IResponseDTO DeleteUser(string Id);
   Task<IResponseDTO> GetAllUsersAsync(int pageNumber = 0, int pageSize = 0);
   IResponseDTO UpdateUser(UpdateUser user);
   Task<IResponseDTO> updateresetPasswordCode(int num, string Email);
  Task<Boolean> sendCodeResetPasswordToEMail(int num, string Email);
  Task<IResponseDTO> resetPasswordVerfayCode(UserVerfayResetPasswordCode request);
  Task<IResponseDTO> ResetPassword(ResetPasswordVm resetpasswordVm);
    }
}

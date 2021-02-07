using BackEnd.BAL.ApiRoute;
using BackEnd.BAL.Models;
using BackEnd.DAL.Context;
using BackEnd.DAL.Entities;
using BackEnd.Service;
using BackEnd.Service.ISercice;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers.V1
{
 
  public class IdentityController: Controller
  {
    private IidentityServices _identityService;
    private readonly BakEndContext _BakEndContext;
        private readonly IResponseDTO _response;
        public IdentityController(IidentityServices identityServices,IResponseDTO responseDTO,
      BakEndContext Context)
    {
      _identityService = identityServices;
      _BakEndContext = Context;
       _response = responseDTO;
    }
        [HttpPost("api/Register")]
        public async Task<IResponseDTO> Register([FromBody] UserRegisteration request) {
    
      var authResponse = await _identityService.RegisterAsync(request.FullName,request.UserName, request.Email, request.Password, request.UserType,request.PhoneNumber);
            if (authResponse.Code != 200)
            {

                _response.Data = authResponse;
                _response.Message = authResponse.Message;
                _response.Code = authResponse.Code;
            }
            else
            {
                _response.Data = authResponse;
                _response.Message = authResponse.Message;
                _response.Code = authResponse.Code;
            }

            return _response;
        }
        [HttpPost("api/RegisterAdmin")]
        public async Task<IResponseDTO>  RegisterAdmin([FromBody] UserRegisterationRequest request)
        {
        
            var authResponse = await _identityService.RegisterAdminAsync(request.FullName, request.UserName, request.Email, request.Password, request.Roles,request.PhoneNumber);
           
            authResponse.Data = null;
            return authResponse;
        }

        [HttpPost("api/Login")]
        public async Task<IResponseDTO> Login([FromBody] UserLoginRequest request)
    {
      var authResponse = await _identityService.LoginAsync(request.Email, request.Password);
      if (authResponse.Code!=200)
      {
           
                _response.Data = authResponse;
                _response.Message = authResponse.Message;
                _response.Code = authResponse.Code;
      }
            else
            {
                _response.Data = authResponse;
                _response.Message = authResponse.Message;
                _response.Code = authResponse.Code;
            }

            return _response;
    }

        #region verfayUser
        [HttpPost("api/verfayUser")]
        public async Task<Result> verfayUser([FromBody] UserVerfayRequest request)
        {
            Result res = await _identityService.verfayUser(request);
            return res;
        }
        
        #endregion
        #region verfayUser
        [HttpPost("api/sendVerficationToEMail")]
        public async Task<bool> sendVerficationToEMail([FromBody] UserVerfayRequest request)
        {
            var res = await _identityService.sendVerficationToEMail(request.Email);
            return res;
        }
        #endregion
        #region Get : api/Roles
        [HttpGet("api/Roles")]
        public IResponseDTO GetAllUserAdmin()
        {
            var result = _identityService.GetAllUserAdmin();
            return result;
        }
        #endregion
        #region 
        [HttpPost("api/CreatePermissions")]
        public IResponseDTO CreatePermissions([FromBody]  Permission permission)
        {
            var result = _identityService.CreatePermissions(permission.RoleName, permission.Permissions);
            return result;
        }
        #endregion
        #region 
        [HttpPost("api/UpdatePermissions")]
        public IResponseDTO UpdatePermissions([FromBody] Permission permission)
        {
            var result = _identityService.UpdatePermissions(permission.Id, permission.RoleName, permission.Permissions);
            return result;
        }
        #endregion
        #region Delete : api/Country/Delete
        [HttpDelete("api/Delete")]
        public IResponseDTO Delete(string id)
        {
            var result = _identityService.Delete(id);
            return result;
        }
        #endregion
        #region Delete : api/Country/Delete
        [HttpDelete("api/DeleteManger")]
        public IResponseDTO DeleteManger(string id) 
        {
            var result = _identityService.DeleteUser(id);
            return result;
        }
        #endregion
        #region Get : api/Roles
        [HttpGet("api/GetAllManger")]
        public async Task<IResponseDTO> GetAllUsersAsync(int Page=0,int Size=0) 
        {
            var result = await _identityService.GetAllUsersAsync(Page, Size);
            return result;
        }
        #endregion
        [HttpPost("api/UpdateManger")]
        public IResponseDTO UpdateUser([FromBody] UpdateUser permission)
        {
            var result = _identityService.UpdateUser(permission);
            return result;
        }
    }
}

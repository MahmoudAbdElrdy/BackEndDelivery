using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
using BackEnd.DAL.Context;
using BackEnd.DAL.Entities;
using BackEnd.Helpers.Enums;
using BackEnd.Service.ISercice;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using BackEnd.Service.DTO;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Service.Service
{
    public class IdentityServices : BaseServices, IidentityServices
    {
        private readonly Random _random = new Random();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationSettings _jwtSettings;
        private readonly TokenValidationParameters _TokenValidationParameters;
        private readonly BakEndContext _dataContext;
        private readonly IemailService _emailService;


        public IdentityServices(UserManager<ApplicationUser> userManager, IResponseDTO response,
      ApplicationSettings jwtSettings,
      TokenValidationParameters TokenValidationParameters,
      RoleManager<IdentityRole> roleManager,
      IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper,
      BakEndContext dataContext,
      IemailService emailService)
            : base(unitOfWork, responseDTO, mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings;
            _TokenValidationParameters = TokenValidationParameters;
            _dataContext = dataContext;
            _emailService = emailService;
            _response = response;

        }

        public async Task<AuthenticationResult> LoginAsync(string Email, string Password)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if(user==null)
             user = await _userManager.FindByNameAsync(Email);
            if (user == null)
            {
                return new AuthenticationResult
                {
                    Message = "User does not Exist"
                };
            }


            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, Password);
            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Message = "Password  wrong"
                };
            }
            if (user.confirmed == null || user.confirmed == false)
            {
                return new AuthenticationResult
                {
                    Message = "User Must Send Verfication Code"
                };
            }
            return await GenerateAutheticationForResultForUser(user);
        }


        private ClaimsPrincipal GetPrincipalFromToken(string Token) {
            var tokenHandler = new JwtSecurityTokenHandler();
            try {
                var principal = tokenHandler.ValidateToken(Token, _TokenValidationParameters, out var validtionToken);
                if (!IsJwtWithValidationSecurityAlgorithm(validtionToken)) {
                    return null;
                }
                return principal;
            }
            catch {
                return null;
            }
        }

        private bool IsJwtWithValidationSecurityAlgorithm(SecurityToken validatedToken) {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
              jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
              StringComparison.InvariantCultureIgnoreCase);
        }

        public async Task<AuthenticationResult> RegisterAsync(string fullName, string UserName , string Email, string Password, int UserType, string PhoneNumber)
        {
            var existingUser = await _userManager.FindByEmailAsync(Email);
            var UserId = "";
            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                  UserType=UserType.ToString(),
                    Code = 400,
                    Message = "User with this email adress already Exist"
                };
            }
           var existingUser2 = _dataContext.Users.Where(x => x.PhoneNumber == PhoneNumber);
            if (existingUser2.Count() >0)
            {
                return new AuthenticationResult
                {
                    UserType = UserType.ToString(),
                    Code = 400,
                    Message = "User with this PhoneNumber adress already Exist"
                };
            }
            int num = _random.Next();
            var newUser = new ApplicationUser
            {
                Email = Email,
                UserName = UserName,
                FullName = fullName,
                verficationCode = num,
                PhoneNumber = PhoneNumber,
                confirmed = true,
                MemberCertified = true,
                StatusType = true,
                MemberVerification = false
            };

         

            //-----------------------------add Role to token------------------
            string Roles = "";
            if (UserType == 2)
            {
                Roles = Helpers.Enums.UserType.ServiceIntroduction.ToString();
            }
            else if (UserType == 3)
            {
                Roles = Helpers.Enums.UserType.ServiceRecipient.ToString();

            }
            else
            {
                Roles = Helpers.Enums.UserType.Admin.ToString();
            }
            if (!string.IsNullOrEmpty(Roles))
            {
                var createdUser = await _userManager.CreateAsync(newUser, Password);
                UserId = newUser.Id;
                if (!createdUser.Succeeded)
                {
                    return new AuthenticationResult
                    {
                        UserType = UserType.ToString(),
                        Code = 400,
                        Message = createdUser.Errors.Select(x => x.Description).FirstOrDefault()
                    };

                }
                await _userManager.AddToRoleAsync(newUser, Roles);
            }
            //-----------------------------------------------------------------
            //var res = await sendVerficationToEMail(newUser.Email);
            //if (res != true)
            //{
            //    return new ResponseDTO
            //    {
            //        Data = null,
            //        Code = 400,
            //        Message = createdUser.Errors.Select(x => "email not send").FirstOrDefault(),
            //    };

            //}
            //else
            //{
                //return new ResponseDTO
                //{
                //    Data = UserId,
                //    Code = 200,
                //    Message = "OK"
                //};
            return await GenerateAutheticationForResultForUser(newUser);
            // }




        }

        //return await GenerateAutheticationForResultForUser(newUser);
       private async Task<AuthenticationResult> GenerateAutheticationForResultForUser(ApplicationUser user) {
      var TokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_jwtSettings.JWT_Secret);
      var claims = new List<Claim> {
          new Claim(JwtRegisteredClaimNames.Sub,user.Email),
          new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
          new Claim(JwtRegisteredClaimNames.Email,user.Email),
          new Claim("id",user.Id)
          };

      //get claims of user---------------------------------------
      var Userclaims = await _userManager.GetClaimsAsync(user);
      claims.AddRange(Userclaims);
      //------------------------Add Roles to claims-----------------------------------
      var userRols = await _userManager.GetRolesAsync(user);
            if (userRols == null)
            {
                return new AuthenticationResult
                {
                    UserType = null,
                    Code = 400,
                    Message = "Not Found Role"
                };
            }
      var Role = userRols.FirstOrDefault();
      var roleRole = await _roleManager.FindByNameAsync(Role);
            ApplicationRole applicationRole = new ApplicationRole();
            foreach (var userRole in userRols)
      {
        claims.Add(new Claim(ClaimTypes.Role, userRole));
        var role = await _roleManager.FindByNameAsync(userRole);
        if (role != null)
        {
         
          var roleClaims = await _roleManager.GetClaimsAsync(role);
          foreach (Claim roleClaim in roleClaims)
          {
            claims.Add(roleClaim);
          }
        }
      }
      //---------------------------------------------------------
      var TokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = TokenHandler.CreateToken(TokenDescriptor);
      await _dataContext.SaveChangesAsync();
            if (roleRole != null )
            {
                applicationRole = _unitOfWork.ApplicationRole.GetByID(roleRole.Id);

            }
            return new AuthenticationResult

            { ApplicationUserId = user.Id,
                UserType=applicationRole.UserType.ToString(),
                Message="OK",
                Code = 200,
                Token = TokenHandler.WriteToken(token),
                Role = Role,
                Permissions =!String.IsNullOrEmpty(applicationRole.Permissions) ? applicationRole.Permissions.Split(',').Select(x => int.Parse(x.Trim())).ToList() : new List<int>()
            };



    }
       public async Task<Boolean> sendVerficationToEMail(string Email)
        {
            int num = _random.Next();
            return await _emailService.sendVerfication(num, Email);
        }

        public async Task<Result> verfayUser(UserVerfayRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user.verficationCode == request.verficationCode)
            {
                user.confirmed = true;
                await _userManager.UpdateAsync(user);
                return new Result { success = true,data=user,code=200,message="ok" };
            }
            else
            {
                return new Result { success = false,data = user, code = 400, message = "Falied" };
            }

        }
        public async Task<IResponseDTO> RegisterAdminAsync(string FullName,string UserName, string Email, string Password, string Roles, string PhoneNumber)
        {
            var existingUser = await _userManager.FindByEmailAsync(Email);
            if (existingUser != null)
            {
                return new ResponseDTO
                {
                    Data = null,
                    Code = 400,
                    Message = "User with this email adress already Exist"
                };
            }
            int num = _random.Next();
            var newUser = new ApplicationUser
            {
                Email = Email,
                UserName = UserName,
                FullName = FullName,
                verficationCode = num,
                PhoneNumber = PhoneNumber,
                confirmed = true,
                MemberCertified=true,
                StatusType=true,
                MemberVerification=false
            };
            if (!string.IsNullOrEmpty(Roles))
            {
                
                var adminRole = new ApplicationRole();
                if (!await _roleManager.RoleExistsAsync(Roles))
                {
                    adminRole.Name = Roles;
                    //adminRole.UserType = (UserType)1;
                    //await _roleManager.CreateAsync(adminRole);
                    var Role = _unitOfWork.ApplicationRole.GetByID(Roles);
                    if (Role != null)
                    {
                        var createdUser = await _userManager.CreateAsync(newUser, Password);

                        if (!createdUser.Succeeded)
                        {
                            return new ResponseDTO
                            {
                                Data = null,
                                Code = 400,
                                Message = createdUser.Errors.Select(x => x.Description).FirstOrDefault()
                            };

                        }
                        await _userManager.AddToRoleAsync(newUser,Role.Name);
                    }
                    
                }
              
            }
          
          


            //-----------------------------add Role to token------------------
           
            //-----------------------------------------------------------------
            //var res = await sendVerficationToEMail(newUser.Email);
            //if (res != true)
            //{
            //    return new ResponseDTO
            //    {
            //        Data = null,
            //        Code = 400,
            //        Message = createdUser.Errors.Select(x => "email not send").FirstOrDefault(),
            //    };
               
            //}
            //else
            //{
                return new ResponseDTO
                {
                    Data = null,
                    Code = 200,
                    Message = "OK"
                };
               
          //  }

            //return await GenerateAutheticationForResultForUser(newUser);


        }

        public IResponseDTO CreatePermissions(string RoleName, List<int>Permissions)
        {
          
            try
            {
                var Role = new ApplicationRole();
                var resultPermissions = string.Join(",", Permissions);
                Role.Permissions = resultPermissions;
                Role.Name = RoleName;
                Role.UserType = (UserType)1;
                _unitOfWork.ApplicationRole.Insert(Role);
                var Result= _unitOfWork.Save();
                if (Result == "200")
                {
                    _response.Data = RoleName;
                    _response.Code = 200;
                    _response.Message = "OK";

                }
                else
                {
                    _response.Data = null;
                    _response.Code = 400;
                    _response.Message = Result;
                }

            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Code = 400;
                _response.Message = ex.Message;
            }
            return _response;
          
        }
        public IResponseDTO GetAllUserAdmin() 
        {
            try
            {
                 
                var data = _unitOfWork.ApplicationRole.Get(x=>x.UserType==UserType.Admin).ToList();
               
                var ApplicationRoleDto = new List<ApplicationRoleDto>();
                foreach(var Model in data)
                {
                    var Application = new ApplicationRoleDto();
                 
                       
                    Application.Id = Model.Id;
                    Application.Name = Model.Name;
                    Application.UserType = (int?)Model.UserType;
                    Application.Permissions= !String.IsNullOrEmpty(Model.Permissions) ? Model.Permissions.Split(',').Select(x => int.Parse(x.Trim())).ToList():new List<int>();
                  
                    ApplicationRoleDto.Add(Application);
                }
               
                _response.Data = ApplicationRoleDto;
                _response.Code = 200;
                _response.Message = "OK";
            }
            catch (Exception ex)
            {
                _response.Data =null;
                _response.Code = 200;
                _response.Message = ex.Message;
            }
            return _response;
        }
        public IResponseDTO UpdatePermissions(string Id,string RoleName, List<int> Permissions)
        {

            try
            {
                var Role = _unitOfWork.ApplicationRole.GetByID(Id);
                var resultPermissions = string.Join(",", Permissions);
                Role.Permissions = resultPermissions;
                Role.Name = RoleName;
                Role.NormalizedName= RoleName;
                _unitOfWork.ApplicationRole.Update(Role);
                var Result = _unitOfWork.Save();
                if (Result == "200")
                {
                    _response.Data = Role;
                    _response.Code = 200;
                    _response.Message = "OK";

                }
                else
                {
                    _response.Data = null;
                    _response.Code = 400;
                    _response.Message = Result;
                }

            }
            catch (Exception ex)
            {
                _response.Data = ex.Message;
                _response.Code = 200;
                _response.Message = "Falied";
            }
            return _response;

        }

        #region Delete(CityDto model)
        public IResponseDTO Delete(string Id)
        {
            try
            {
                
                var checkUser = _dataContext.UserRoles.Where(x=>x.RoleId==Id).ToList();
                if (checkUser.Count > 0)
                {
                    return
                           new ResponseDTO()
                           {
                               Data = Id,
                               Code =400,
                               Message = "Can Not Delete as Use in User",
                           };

                }
                _unitOfWork.ApplicationRole.Delete(Id);
                var save = _unitOfWork.Save();

                if (save == "200")
                {
                    _response.Data = Id;
                    _response.Code = 200;
                    _response.Message = "OK";
                }
                else
                {
                    _response.Data = null;

                    _response.Code = 400;
                    _response.Message = save;
                }
            }
            catch (Exception ex)
            {

                _response.Data = null;
                _response.Code = 400;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion
        #region Delete(CityDto model)
        public IResponseDTO DeleteUser(string Id) 
        {
            try
            {

                _unitOfWork.ApplicationUser.Delete(Id);
                var save = _unitOfWork.Save();

                if (save == "200")
                {
                    _response.Data = Id;
                    _response.Code = 200;
                    _response.Message = "OK";
                }
                else
                {
                    _response.Data = null;

                    _response.Code = 400;
                    _response.Message = save;
                }
            }
            catch (Exception ex)
            {

                _response.Data = null;
                _response.Code = 400;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion
        public async Task<IResponseDTO> GetAllUsersAsync(int pageNumber = 0, int pageSize = 0)
        {
            var appUsers =new List<ApplicationUser>();
            var appUserstotal = new List<ApplicationUser>();
            var total = 0;
            try
            {
                List<ApplicationRole> role = new List<ApplicationRole>();
                
                    role = _unitOfWork.ApplicationRole.Get(x =>x.UserType == UserType.Admin).ToList();
                //   total = _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).Where(x=>x.confirmed==true).Count();
                foreach (var Role in role)
                {
                    var usersInRole = await _userManager.GetUsersInRoleAsync(Role.Name);
                    var usersInRoleIds = usersInRole.Select(x => x.Id);
                    var users = _unitOfWork.ApplicationUser.Get(x => x.confirmed == true, page: pageNumber, Take: pageSize).Where(u => usersInRoleIds.Contains(u.Id)).ToList();
                    appUsers.AddRange(users);
                }
                foreach (var Role in role)
                {
                    var usersInRole = await _userManager.GetUsersInRoleAsync(Role.Name);
                    var usersInRoleIds = usersInRole.Select(x => x.Id);
                    var users = _unitOfWork.ApplicationUser.Get(x => x.confirmed == true).Where(u => usersInRoleIds.Contains(u.Id)).ToList();
                    appUserstotal.AddRange(users);
                }


                var ApplicationRoleDto = new List<UserRegisterationRequest>();
                foreach (var Model in appUsers)
                {
                    var Roleid = _dataContext.UserRoles.FirstOrDefault(x => x.UserId == Model.Id);
                    var Application = new UserRegisterationRequest();
                    Application.Id = Model.Id;
                    Application.FullName = Model.FullName ;
                    Application.UserName = Model.UserName;
                    Application.Email = Model.Email;
                    Application.Roles = Roleid.RoleId;
                    Application.RoleName =!string.IsNullOrEmpty(Roleid.RoleId)? _unitOfWork.ApplicationRole.GetByID(Roleid.RoleId).Name:"";
                    Application.PhoneNumber = Model.PhoneNumber;
                  
                    ApplicationRoleDto.Add(Application);
                }

                _response.Data = ApplicationRoleDto;
                _response.Code = 200;
                _response.Message = "OK";
                _response.totalRowCount = appUserstotal.Count;

            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Code = 400;
                _response.Message = ex.Message;
            }
            return _response;
        }
        public IResponseDTO UpdateUser(UpdateUser user)
        {
            try
            {
                var User = _unitOfWork.ApplicationUser.GetByID(user.Id);
                
                User.FullName= user.FullName;
                User.UserName = user.UserName;
                User.PhoneNumber = user.PhoneNumber;
                User.Email = user.Email;
               
                _unitOfWork.ApplicationUser.Update(User);
                var UserRole = _dataContext.UserRoles.Where(x => x.UserId == user.Id).FirstOrDefault();
                if (UserRole != null)
                {
                    
                    _dataContext.UserRoles.Remove(UserRole);
                    var newRole = new IdentityUserRole<string>();
                    newRole.RoleId = user.Roles;
                    newRole.UserId = user.Id;
                    _dataContext.UserRoles.Add(newRole);
                }
               
              
                var Result = _unitOfWork.Save();
                if (Result == "200")
                {
                    _response.Data = null;
                    _response.Code = 200;
                    _response.Message = "OK";

                }
                else
                {
                    _response.Data = null;
                    _response.Code = 400;
                    _response.Message = Result;
                }

            }
            catch (Exception ex)
            {
                _response.Data = ex.Message;
                _response.Code = 200;
                _response.Message = "Falied";
            }
            return _response;
        }
        public async Task<IResponseDTO> updateresetPasswordCode(int num, string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email);
            User.resetPasswordCode = num;
            await _userManager.UpdateAsync(User);
            return new ResponseDTO
            {
                Code = 200,
                Message = "code sent successfuly",
                
                Data = null
            };
        }

        public async Task<Boolean> sendCodeResetPasswordToEMail(int restCode, string Email)
        {
            return await _emailService.sendResetPasswordCode(restCode, Email);
        }


        public async Task<IResponseDTO> resetPasswordVerfayCode(UserVerfayResetPasswordCode request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user.resetPasswordCode == request.resetCode)
            {
                return new ResponseDTO { Data=null, Code = 200, Message = "confirmed scuccess" };
            }
            else
            {
                return new ResponseDTO {  Code = 403, Message = "confirmed faild",Data=null };
            }

        }

        public async Task<IResponseDTO> ResetPassword(ResetPasswordVm resetpasswordVm)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var user = await _userManager.FindByEmailAsync(resetpasswordVm.Email);
            if (!string.IsNullOrEmpty(resetpasswordVm.Password))
                user.PasswordHash = passwordHasher.HashPassword(user, resetpasswordVm.Password);
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ResponseDTO
                {
                    
                    Code = 200,
                    Message = "reset password success",
                    Data = result
                };
            }
            else
            {
                return new ResponseDTO
                {
                    
                    Code = 403,
                    Message = "reset password faild",
                    Data = result
                };
            }
        }

       
    }
}

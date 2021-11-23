
using BuisnessLayer.Token;
using DataLayer.DTOs;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
 
namespace gemography_backend_coding_challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ResultViewModel _resultViewModel;
        private readonly ITokenService _tokenService;

        public UserController(ResultViewModel resultViewModel,ITokenService tokenService)
        {
            _resultViewModel = resultViewModel;
            _tokenService = tokenService;
            
        }

       
        
       
         
        [HttpPost("register")]
        public ActionResult<UserDto> Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (UserExists(registerDto.Username)) { 
                         _resultViewModel.StatusCode = HttpStatusCode.ExpectationFailed;
                        _resultViewModel.IsSuccess = false;
                        _resultViewModel.Message = (_User.CurrentLanguage == Convert.ToInt64(MessageLanguage.EN)) ? "User Already Exists" : "اسم المستخدم موجود بالفعل";
                        return Ok(_resultViewModel);
                         }

            using var hmac = new HMACSHA512();
            var user = new User
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHashWeb = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerDto.Password)),
                SaltWeb = hmac.Key,
                ID = 0,
                IsRegisteredNew = true,
                IsActive = true,
                IsDeleted = false,
                IsAdmin = false,
                IsAVLUser = true,
                ControllingDataPrivilege = false,
                ReportingPrivilege = false,
                UseMobile = true,
                UseWeb = true,
                Rased = true,
                CreatedDate = DateTime.Now,
                UserLastEntry = DateTime.Now,
                GuidId = Guid.NewGuid()


            };
            _User.Insert(user);

                    _resultViewModel.StatusCode = HttpStatusCode.ExpectationFailed;
                    _resultViewModel.IsSuccess = true;
                    _resultViewModel.Message = (_User.CurrentLanguage == Convert.ToInt64(MessageLanguage.EN)) ? "Register Successfully" : "تم تسجيل المستخدم بنجاح";
                    _resultViewModel.Data=new UserDto
            {
                Username = registerDto.Username,
                Token = _tokenService.CreateToken(user)

            };
                    return Ok(_resultViewModel);
                }
                catch (Exception ex)
                {
                    _resultViewModel.StatusCode = HttpStatusCode.InternalServerError;
                    _resultViewModel.Message = ex.Message;
                    _resultViewModel.IsSuccess = false;

                    //Internal Server Error
                    return StatusCode(StatusCodes.Status500InternalServerError, _resultViewModel);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public ActionResult<UserDto> LoginWebApi(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                VDream.Tables.People.User currentuser = null;
                try { 



            var user = _User.SingleOrDefault(x => x.UserName == loginDto.Username);

                    if (user != null)
                    {
                        var userCheck = _User.SingleOrDefault(x => x.UserName == loginDto.Username && x.IsActive == true && x.IsDeleted == false);
                        if (userCheck == null)
                        {
                            _resultViewModel.StatusCode = HttpStatusCode.ExpectationFailed;
                            _resultViewModel.IsSuccess = false;
                            _resultViewModel.Message = (_User.CurrentLanguage == Convert.ToInt64(MessageLanguage.EN)) ? "User Is Inactive or Deleted" : "المستخدم محذوف او غير مفعل";
                            return Ok(_resultViewModel);
                        }
                        else
                            currentuser = userCheck;
                    }
                    else 
                    { 


                        _resultViewModel.StatusCode = HttpStatusCode.ExpectationFailed;
                        _resultViewModel.IsSuccess = false;
                        _resultViewModel.Message = (_User.CurrentLanguage == Convert.ToInt64(MessageLanguage.EN)) ? "Invalid UserName" : "اسم المستخدم غير صحيح";
                        return Ok(_resultViewModel);
                    }
                        

            using var hmac = new HMACSHA512(user.SaltWeb);


            var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < ComputedHash.Length; i++)
            {
                if (ComputedHash[i] != user.PasswordHashWeb[i])
                        {
                            _resultViewModel.StatusCode = HttpStatusCode.InternalServerError;
                            _resultViewModel.IsSuccess = false;
                            _resultViewModel.Message = (_User.CurrentLanguage == Convert.ToInt64(MessageLanguage.EN)) ? "Invalid Password" : "كلمة المرور غير صحيحة";
                            return Ok(_resultViewModel);
                        }
                        

            }
 
                    long BranchId = _Department.FirstOrDefault(comp => currentuser.DepartmentId == comp.ID).BranchId;
                    long DeviceId = _Device.FirstOrDefault(dev => BranchId == dev.BranchId).ID;
                    long CompanyId = _Branch.FirstOrDefault(bran => BranchId == bran.ID).CompanyId;
                    var CompanyLanguages = _CompanyLanguage.Find(compLang => CompanyId == compLang.CompanyId).ToList();
                    CompanyNames names = new CompanyNames();
                    for (int i = 0; i < CompanyLanguages.Count; i++)
                    {
                        if (CompanyLanguages[i].LanguageId == 5)
                            names.CompanyNameAr = CompanyLanguages[i].CompanyName;
                        else
                            names.CompanyNameEn = CompanyLanguages[i].CompanyName;

                    }
                     RolesEnum roleEnum = (RolesEnum)Enum.ToObject(typeof(RolesEnum), currentuser.RoleId);
                    string roleName = roleEnum.ToString();
                    _resultViewModel.StatusCode = HttpStatusCode.ExpectationFailed;
                    _resultViewModel.IsSuccess = true;
                    _resultViewModel.Message = (_User.CurrentLanguage == Convert.ToInt64(MessageLanguage.EN)) ? "Login Successfully" : "تم تسجيل الدخول بنجاح";
                    _resultViewModel.Data = new UserDto
                    {
                        Username = loginDto.Username,
                        Token = _tokenService.CreateToken(user),
                        IsActive = currentuser.IsActive,
                        IsDeleted = currentuser.IsDeleted,
                        Address = currentuser.Address,
                        DepartmentId = currentuser.DepartmentId,
                        Email = currentuser.Email,
                        IsAdmin = currentuser.IsAdmin,
                        IsAVLUser = currentuser.IsAVLUser,
                        MobileNumber = currentuser.MobileNumber,
                        LastName = currentuser.LastName,
                        RoleId = currentuser.RoleId,
                        UseMobile = currentuser.UseMobile,
                        FirstName = currentuser.FirstName,
                        UserLastEntry = DateTime.Now,
                        ReportingPrivilege = currentuser.ReportingPrivilege,
                        ControllingDataPrivilege = currentuser.ControllingDataPrivilege,
                        Rased = currentuser.Rased,
                        UseWeb = currentuser.UseWeb,
                        ID = currentuser.ID,
                         BranchId = BranchId,
                         CompanyId=CompanyId,
                         RoleName = roleName,
                         DeviceId=DeviceId,
                         CompanyNameAr = names.CompanyNameAr,
                         CompanyNameEn = names.CompanyNameEn

                    };

                    return Ok( _resultViewModel);
                   
        }
            catch (Exception ex)
            {
                _resultViewModel.StatusCode = HttpStatusCode.InternalServerError;
                _resultViewModel.Message = ex.Message;
                _resultViewModel.IsSuccess = false;

                    //Internal Server Error
                    return StatusCode(StatusCodes.Status500InternalServerError, _resultViewModel);
    }
            }
            return BadRequest(ModelState);
        }

        private bool UserExists(string username)
        {
            if (_User.FirstOrDefault(x => x.UserName == username.ToLower()) != null)
                return true;
            else
                return false;
        }

    }
}

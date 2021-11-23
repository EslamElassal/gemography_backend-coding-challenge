
using BuisnessLayer.Token;
using DataLayer.DBEntities;
using DataLayer.DTOs;
using DataLayer.Models;
using DataLayer.Repository;
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
        private readonly IRepository<User> _User;

        public UserController(ResultViewModel resultViewModel, ITokenService tokenService, IRepository<User> user)
        {
            _resultViewModel = resultViewModel;
            _tokenService = tokenService;
            _User = user;
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
                        _resultViewModel.Message =  "User Already Exists" ;
                        return Ok(_resultViewModel);
                         }

            using var hmac = new HMACSHA512();
            var user = new User
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
                Id = 0
                 
            };
            _User.Insert(user);

                    _resultViewModel.StatusCode = HttpStatusCode.ExpectationFailed;
                    _resultViewModel.IsSuccess = true;
                    _resultViewModel.Message = "User Registered Successfully";
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
               User currentuser = null;
                try { 



            var user = _User.SingleOrDefault(x => x.UserName == loginDto.Username);

                    if (user != null)
                    {
                         
                            currentuser = user;
                    }
                    else 
                    { 


                        _resultViewModel.StatusCode = HttpStatusCode.ExpectationFailed;
                        _resultViewModel.IsSuccess = false;
                        _resultViewModel.Message =  "Invalid UserName"  ;
                        return Ok(_resultViewModel);
                    }
                        

            using var hmac = new HMACSHA512(user.PasswordSalt);


            var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < ComputedHash.Length; i++)
            {
                if (ComputedHash[i] != user.PasswordHash[i])
                        {
                            _resultViewModel.StatusCode = HttpStatusCode.InternalServerError;
                            _resultViewModel.IsSuccess = false;
                            _resultViewModel.Message = "Invalid Password" ;
                            return Ok(_resultViewModel);
                        }
                        

            }
 
                     
                    _resultViewModel.StatusCode = HttpStatusCode.ExpectationFailed;
                    _resultViewModel.IsSuccess = true;
                    _resultViewModel.Message =  "Login Successfully"  ;
                    _resultViewModel.Data = new UserDto
                    {
                        Username = loginDto.Username,
                        Token = _tokenService.CreateToken(user),
                          
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

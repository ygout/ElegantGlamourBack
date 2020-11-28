using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ElegantGlamour.Api.Validators;
using ElegantGlamour.Api.Dtos;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.Extensions.Logging;
using System.Reflection;
using AutoWrapper.Wrappers;
using ElegantGlamour.Core.Error;
using ElegantGlamour.Api.Swagger;
using Swashbuckle.AspNetCore.Annotations;
using ElegantGlamour.Core.Specifications;
using ElegantGlamour.API.Controllers;
using Microsoft.AspNetCore.Identity;
using ElegantGlamour.Core.Models.Entity.Auth;
using ElegantGlamour.Api.Dtos.User;
using System.Linq;
using ElegantGlamour.API.Dtos.User;

namespace ElegantGlamour.Api.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IMapper mapper, ILogger<AuthController> logger, UserManager<User> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("signup")]
        public async Task<ApiResponse> SignUp(UserSignUpDto userSignUpDto)
        {
            try
            {
                var user = _mapper.Map<UserSignUpDto, User>(userSignUpDto);

                var userCreateResult = await _userManager.CreateAsync(user, userSignUpDto.Password);

                if (!userCreateResult.Succeeded)
                {
                    throw new ApiException(userCreateResult.Errors.First().Description, 500);
                }
                return new ApiResponse("Votre compte a bien été crée", Status201Created);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost("SignIn")]
        public async Task<ApiResponse> SignIn(UserLoginDto userLoginResource)
        {
            try
            {
                var user = _userManager.Users.SingleOrDefault(u => u.UserName == userLoginResource.Email);
                if (user is null)
                {
                    throw new ApiException(ErrorMessage.Err_User_Not_Exist, Status404NotFound);
                }

                var userSigninResult = await _userManager.CheckPasswordAsync(user, userLoginResource.Password);

                if (!userSigninResult)
                {
                    throw new ApiException(ErrorMessage.Err_User_Invalid_Login, Status400BadRequest);
                }

                return new ApiResponse(Status200OK);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
using ChatApp.Application.Utilities.Class;
using ChatApp.Application.Utilities;
using ChatApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Application.CQRS.Auth.Commands.Validator;
using ChatApp.Application.CQRS.Auth.Commands.Models;
using ChatApp.Application.CQRS.Auth.Commands.Response;
using ChatApp.Application.Services.inteface;

namespace ChatApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthService(SignInManager<ApplicationUser> signInManager,
                           UserManager<ApplicationUser> userManager,
                           ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }


        public async Task<ApiResponse<LoginUserResponse>> HandleLogin(LoginUserRequest request)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
            if (signInResult.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                if (user == null)
                {
                    return ResponseHandler.Failure<LoginUserResponse>("User not found.");
                }

                var token = _tokenService.GenerateToken(user.Id, user.UserName);
                return ResponseHandler.Success(new LoginUserResponse { Token = token}, "User logged in successfully.");
            }

            return ResponseHandler.Failure<LoginUserResponse>("Invalid login attempt.");
        }

        public async Task<ApiResponse<RegisterUserResponse>> HandleRegister(RegisterUserCommand request)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                NikName = request.NikName,
                CreatedBy = "Admin",
                CreatedDate = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return ResponseHandler.Success(new RegisterUserResponse { IsSuccess = true }, "User registered successfully.");
            }

            return ResponseHandler.Failure<RegisterUserResponse>(string.Join("; ", result.Errors));
        }
    }
}

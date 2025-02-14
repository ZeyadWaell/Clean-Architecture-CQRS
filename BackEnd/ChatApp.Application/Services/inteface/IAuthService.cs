using ChatApp.Application.CQRS.Auth.Commands.Models;
using ChatApp.Application.CQRS.Auth.Commands.Response;
using ChatApp.Application.CQRS.Auth.Commands.Validator;
using ChatApp.Application.Utilities.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Services.inteface
{
    public interface IAuthService
    {
       Task<ApiResponse<LoginUserResponse>> HandleLogin(LoginUserRequest request);
       Task<ApiResponse<RegisterUserResponse>> HandleRegister(RegisterUserRequest request);

    }
}

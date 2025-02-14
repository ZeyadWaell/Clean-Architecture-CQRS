using ChatApp.Application.CQRS.Auth.Commands.Models;
using ChatApp.Application.CQRS.Auth.Commands.Validator;
using ChatApp.Application.Services;
using ChatApp.Application.Services.inteface;
using ChatApp.Application.Utilities;
using ChatApp.Application.Utilities.Class;
using ChatApp.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Application.CQRS.Commands.Auth
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserRequest, ApiResponse<LoginUserResponse>>
    {
        private readonly IAuthService _authServices;

        public LoginUserCommandHandler(IAuthService authServices)
        {
            _authServices = authServices;
        }


        public async Task<ApiResponse<LoginUserResponse>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            return await _authServices.HandleLogin(request);
        }
    }
}

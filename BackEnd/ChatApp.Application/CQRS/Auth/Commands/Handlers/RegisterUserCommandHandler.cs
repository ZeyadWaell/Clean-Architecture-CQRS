using ChatApp.Application.CQRS.Auth.Commands.Models;
using ChatApp.Application.CQRS.Auth.Commands.Response;
using ChatApp.Application.Services.inteface;
using ChatApp.Application.Utilities.Class;
using MediatR;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.CQRS.Auth.Commands.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserRequest, ApiResponse<RegisterUserResponse>>
    {
        private readonly IAuthService _authServices;

        public RegisterUserCommandHandler(IAuthService authServices)
        {
            _authServices = authServices;
        }


        public async Task<ApiResponse<RegisterUserResponse>> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
           return await _authServices.HandleRegister(request);
        }
    }
}



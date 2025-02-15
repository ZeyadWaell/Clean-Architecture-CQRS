using ChatApp.Application.CQRS.Auth.Commands.Validator;
using ChatApp.Application.Utilities.Class;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.CQRS.Auth.Commands.Models
{
    public class LoginUserRequest : IRequest<ApiResponse<LoginUserResponse>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

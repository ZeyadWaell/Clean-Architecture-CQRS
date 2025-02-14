using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.CQRS.Auth.Commands.Response
{
    public class RegisterUserResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}

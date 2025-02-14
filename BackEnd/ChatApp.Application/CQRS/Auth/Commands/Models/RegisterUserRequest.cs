using ChatApp.Application.CQRS.Auth.Commands.Response;
using ChatApp.Application.Utilities.Class;
using MediatR;


namespace ChatApp.Application.CQRS.Auth.Commands.Models
{
    public class RegisterUserRequest : IRequest<ApiResponse<RegisterUserResponse>>
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string NikName { get; set; }
    }
}

using ChatApp.Application.CQRS.Auth.Commands.Models;
using ChatApp.Routes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [Route(AccountRoutes.Controller)] 
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost(AccountRoutes.Login)]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpPost(AccountRoutes.Register)]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand request)
        {
            var response = await _mediator.Send(request);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

    }
}

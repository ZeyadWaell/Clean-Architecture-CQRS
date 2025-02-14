using ChatApp.Application.CQRS.ChatMessage.Commands.Models;
using ChatApp.Application.CQRS.ChatRoom.Commands.Models;
using ChatApp.Application.CQRS.ChatRoom.Queries.Models;
using ChatApp.Core.Entities;
using ChatApp.Routes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [Route(ChatRoomsRoutes.Controller)]
    [ApiController]
    public class ChatRoomsController : ControllerBase
    {

        private readonly IMediator _mediator;
        public ChatRoomsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // Just for Testing UI 

        //[HttpPost(ChatRoomsRoutes.JoinRoom)]
        //public async Task<IActionResult> JoinRoom([FromBody] JoinRoomRequest command)
        //{
        //    var response = await _mediator.Send(command);
      
        //    return Ok(response);
        //}

        //[HttpDelete(ChatRoomsRoutes.LeaveRoom)]
        //public async Task<IActionResult> LeaveRoom([FromBody] LeaveRoomRequest command)
        //{
        //    var response = await _mediator.Send(command);

        //    return Ok(response);
        //}
        [HttpGet(ChatRoomsRoutes.GetAllRoom)]
        public async Task<IActionResult> GetAllRoom()
        {
            var command = new GetAllRoomsRequest();
            var response = await _mediator.Send(command);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}

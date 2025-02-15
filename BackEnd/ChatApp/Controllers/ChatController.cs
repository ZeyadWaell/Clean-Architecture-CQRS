using ChatApp.Api.Hubs;
using ChatApp.Application.CQRS.ChatMessage.Commands.Models;
using ChatApp.Application.CQRS.ChatMessage.Queries.Models;
using ChatApp.Application.CQRS.Requests.Chat.Models;
using ChatApp.Routes;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Controllers
{

    [Route(ChatRoutes.Controller)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;
        IHubContext<ChatHub> hubContext;

        public ChatController(IMediator mediator, IHubContext<ChatHub> hubContext)
        {
            _mediator = mediator;
            this.hubContext = hubContext;
        }
        // For testing purposes
        //[HttpPost(ChatRoutes.SendMessage)]
        //public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest command)
        //{
        //    var response = await _mediator.Send(command);
        //    if (response.Success)
        //    {
        //        // Send message to all clients (instead of a group)
        //        await hubContext.Clients.All.SendAsync("ReceiveMessage", response.Data);
        //        return Ok(response);
        //    }
        //    return BadRequest(response);
        //}


        //[HttpPut($"{ChatRoutes.EditMessage}" + "/{messageId}")]
        //public async Task<IActionResult> EditMessage(Guid messageId, [FromBody] EditMessageRequest command)
        //{
        //    command.MessageId = messageId;
        //    var response = await _mediator.Send(command);
        //    if (response.Success)
        //        return Ok(response);
        //    return BadRequest(response);
        //}


        //[HttpDelete(ChatRoutes.DeleteMessage)]
        //public async Task<IActionResult> Delete([FromQuery] DeleteMessageRequest command)
        //{
        //    var response = await _mediator.Send(command);
        //    if (response.Success)
        //        return Ok(response);
        //    return BadRequest(response);
        //}

        [HttpGet($"{ChatRoutes.GetMessages}" + "/{chatRoomId}")]
        public async Task<IActionResult> GetMessagesByRoom(Guid chatRoomId)
        {
            var query = new GetChatRoomMessagesQuery { ChatRoomId = chatRoomId };
            var response = await _mediator.Send(query);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }

}

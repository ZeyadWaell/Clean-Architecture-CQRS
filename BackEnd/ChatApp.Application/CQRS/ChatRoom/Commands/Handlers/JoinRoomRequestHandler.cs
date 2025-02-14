using ChatApp.Application.CQRS.ChatMessage.Commands.Response;
using ChatApp.Application.CQRS.ChatRoom.Commands.Models;
using ChatApp.Application.Services.inteface;
using ChatApp.Application.Utilities.Class;
using MediatR;

namespace ChatApp.Application.CQRS.ChatRoom.Commands.Handlers
{
    public class JoinRoomRequestHandler : IRequestHandler<JoinRoomRequest, string>
    {
        private readonly IChatService _chatService;

        public JoinRoomRequestHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<string> Handle(JoinRoomRequest request, CancellationToken cancellationToken)
        {
            return await _chatService.JoinRoomAsync(request);
        }


    }
}

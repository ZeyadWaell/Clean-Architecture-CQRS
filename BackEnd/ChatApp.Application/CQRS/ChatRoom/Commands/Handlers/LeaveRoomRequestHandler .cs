using ChatApp.Application.CQRS.ChatRoom.Commands.Models;
using MediatR;

using ChatApp.Application.Services.inteface;

namespace ChatApp.Application.CQRS.ChatRoom.Commands.Handlers
{
    public class LeaveRoomRequestHandler : IRequestHandler<LeaveRoomRequest, string>
    {
        private readonly IChatService _chatService;

        public LeaveRoomRequestHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<string> Handle(LeaveRoomRequest request, CancellationToken cancellationToken)
        {
            var response = await _chatService.LeaveRoomAsync(request);
            return response.Message;
        }
    }
}

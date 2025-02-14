using ChatApp.Application.CQRS.ChatMessage.Queries.Models;
using ChatApp.Application.CQRS.ChatMessage.Queries.Response;
using ChatApp.Application.Services;
using ChatApp.Application.Services.inteface;
using ChatApp.Application.Utilities;
using ChatApp.Application.Utilities.Class;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Application.CQRS.ChatMessage.Queries.Handlers
{
    public class GetChatRoomMessagesRequestHandler : IRequestHandler<GetChatRoomMessagesQuery, ApiResponse<IList<ChatMessageResponse>>>
    {
        private readonly IChatService _chatService;

        public GetChatRoomMessagesRequestHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<ApiResponse<IList<ChatMessageResponse>>> Handle(GetChatRoomMessagesQuery request, CancellationToken cancellationToken)
        {
            return await _chatService.GetChatRoomMessagesAsync(request.ChatRoomId);
        }
    }
}

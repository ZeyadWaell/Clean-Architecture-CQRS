using ChatApp.Application.CQRS.ChatMessage.Commands.Response;
using ChatApp.Application.CQRS.Requests.Chat.Models;
using ChatApp.Application.Services.inteface;
using ChatApp.Application.Utilities.Class;
using MediatR;

namespace ChatApp.Application.CQRS.Requests.Chat.Handlers
{
    public class DeleteMessageRequestHandler : IRequestHandler<DeleteMessageRequest, ApiResponse<DeleteMessageResponse>>
    {
        private readonly IChatService _chatService;

        public DeleteMessageRequestHandler(IChatService chatService)
        {
            _chatService = chatService;
        }


        public async Task<ApiResponse<DeleteMessageResponse>> Handle(DeleteMessageRequest request, CancellationToken cancellationToken)
        {
            return await _chatService.DeleteMessageAsync(request);
        }
    }
}

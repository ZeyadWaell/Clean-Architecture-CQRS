using ChatApp.Application.CQRS.ChatMessage.Commands.Models;
using ChatApp.Application.CQRS.ChatMessage.Commands.Response;
using ChatApp.Application.Services.inteface;
using ChatApp.Application.Utilities.Class;
using MediatR;


namespace ChatApp.Application.CQRS.Requests.Chat.Handlers
{
    public class EditMessageRequestHandler : IRequestHandler<EditMessageCommand, ApiResponse<EditMessageResponse>>
    {
        private readonly IChatService _chatService;

        public EditMessageRequestHandler(IChatService chatService)
        {
            _chatService = chatService;
        }



        public async Task<ApiResponse<EditMessageResponse>> Handle(EditMessageCommand request, CancellationToken cancellationToken)
        {
           return await _chatService.EditMessageAsync(request);
        }
    }
}

using ChatApp.Application.CQRS.ChatMessage.Commands.Models;
using ChatApp.Application.CQRS.ChatMessage.Queries.Response;
using ChatApp.Application.Services.inteface;
using ChatApp.Application.Utilities;
using ChatApp.Application.Utilities.Class;
using ChatApp.Core.Entities;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Interfaces.Main;
using ChatApp.Infrastructure.Repositories;
using MediatR;
using System;

namespace ChatApp.Application.CQRS.Requests.Chat.Handlers
{
    public class SendMessageRequestHandler : IRequestHandler<SendMessageCommand, ApiResponse<ChatMessageResponse>>
    {
        private readonly IChatService _chatService;

        public SendMessageRequestHandler(IChatService chatService)
        {
            _chatService = chatService;
        }


        public async Task<ApiResponse<ChatMessageResponse>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
           return await _chatService.SendMessageAsync(request);
        }
    }
}

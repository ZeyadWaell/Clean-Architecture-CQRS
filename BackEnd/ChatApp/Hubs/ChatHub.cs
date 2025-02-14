using ChatApp.Application.CQRS.ChatMessage.Commands.Models;
using ChatApp.Application.CQRS.ChatMessage.Queries.Response;
using ChatApp.Application.CQRS.Requests.Chat.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ChatApp.Api.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ChatHub> _logger;
        public ChatHub(IMediator mediator, ILogger<ChatHub> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public Task SendMessage(SendMessageRequest command)
        {
            var broadcastMessage = new ChatMessageResponse
            {
                Sender = command.UserName,
                Message = command.Message,
                Timestamp = DateTime.UtcNow,
                MessageId = Guid.NewGuid()
            };
            _ = Clients.Group(command.ChatRoomId.ToString())
                .SendAsync("ReceiveMessage", broadcastMessage);
            _ = ProcessSendMessageAsync(command);
            return Task.CompletedTask;
        }
 
        public Task EditMessage(EditMessageRequest command)
        {
            var broadcastMessage = new ChatMessageResponse
            {
                Sender = "User",
                Message = command.NewContent,
                Timestamp = DateTime.UtcNow,
                MessageId = command.MessageId
            };
            _ = Clients.Group(command.ChatRoomId.ToString())
                .SendAsync("MessageEdited", broadcastMessage);
            _ = ProcessEditMessageAsync(command);
            return Task.CompletedTask;
        }



        public Task DeleteMessage(DeleteMessageRequest command)
        {
            _ = Clients.Group(command.ChatRoomId.ToString())
                .SendAsync("MessageDeleted", command.MessageId);
            _ = ProcessDeleteMessageAsync(command);
            return Task.CompletedTask;
        }


        public async Task JoinRoom(string chatRoomId)
        {
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId);
                await Clients.Group(chatRoomId)
                    .SendAsync("UserJoined", Context.User?.Identity?.Name ?? "Anonymous");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error joining room for ChatRoomId: {ChatRoomId}", chatRoomId);
            }
        }
        public async Task LeaveRoom(string chatRoomId)
        {
            try
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoomId);
                await Clients.Group(chatRoomId)
                    .SendAsync("UserLeft", Context.User?.Identity?.Name ?? "Anonymous");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error leaving room for ChatRoomId: {ChatRoomId}", chatRoomId);
            }
        }


        #region Private helper
        private async Task ProcessSendMessageAsync(SendMessageRequest command)
        {
            try { await _mediator.Send(command); }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing SendMessage for ChatRoomId: {ChatRoomId}", command.ChatRoomId);
            }
        }
        private async Task ProcessEditMessageAsync(EditMessageRequest command)
        {
            try { await _mediator.Send(command); }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing SendMessage for ChatRoomId: {ChatRoomId}", command.ChatRoomId);
            }
        }
        private async Task ProcessDeleteMessageAsync(DeleteMessageRequest command)
        {
            try
            {
                var response = await _mediator.Send(command);
                if (response.Success)
                    await Clients.Group(command.ChatRoomId.ToString())
                        .SendAsync("MessageDeleted", command.MessageId);
                else
                    _logger.LogWarning("DeleteMessage failed for ChatRoomId: {ChatRoomId}. Message: {Message}", command.ChatRoomId, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting message in ChatRoomId: {ChatRoomId}", command.ChatRoomId);
            }
        }
        #endregion
    }
}

using ChatApp.Application.CQRS.ChatMessage.Commands.Response;
using ChatApp.Application.Utilities.Class;
using MediatR;

namespace ChatApp.Application.CQRS.Requests.Chat.Models
{
    public class DeleteMessageCommand : IRequest<ApiResponse<DeleteMessageResponse>>
    {
        public Guid MessageId { get; set; }
        public Guid ChatRoomId { get; set; }
    }
}

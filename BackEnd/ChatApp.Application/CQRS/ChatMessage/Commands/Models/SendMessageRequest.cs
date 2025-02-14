using ChatApp.Application.CQRS.ChatMessage.Queries.Response;
using ChatApp.Application.Utilities.Class;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.CQRS.ChatMessage.Commands.Models
{
    public class SendMessageRequest : IRequest<ApiResponse<ChatMessageResponse>>
    {
        public string UserName { get; set; }
        public Guid ChatRoomId { get; set; }
        public string Message { get; set; }
    }
}

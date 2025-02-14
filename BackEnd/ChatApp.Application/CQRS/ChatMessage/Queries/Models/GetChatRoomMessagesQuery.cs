using ChatApp.Application.CQRS.ChatMessage.Queries.Response;
using ChatApp.Application.Utilities.Class;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.CQRS.ChatMessage.Queries.Models
{
   public class GetChatRoomMessagesQuery : IRequest<ApiResponse<IList<ChatMessageResponse>>>
    {
        public Guid ChatRoomId { get; set; }
    }
}

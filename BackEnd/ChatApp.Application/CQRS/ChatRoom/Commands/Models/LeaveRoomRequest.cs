using ChatApp.Application.CQRS.ChatMessage.Commands.Response;
using ChatApp.Application.Utilities.Class;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.CQRS.ChatRoom.Commands.Models
{
    public class LeaveRoomRequest : IRequest<string>
    {
        public Guid ChatRoomId { get; set; }
        public string UserName { get; set; }
    }
}

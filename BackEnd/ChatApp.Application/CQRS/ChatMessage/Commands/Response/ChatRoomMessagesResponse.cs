using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.CQRS.ChatMessage.Commands.Response
{
    public class ChatRoomMessagesResponse
    {
        public Guid MessageId { get; set; }
        public string Message { get; set; }
        public string Sender { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.CQRS.ChatMessage.Queries.Response
{
    public class ChatMessageResponse
    {
        public Guid MessageId { get; set; }
        public string Message { get; set; }
        public string Sender { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

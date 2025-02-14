using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.CQRS.ChatMessage.Commands.Response
{
    public class DeleteMessageResponse
    {
        public Guid MessageId { get; set; }
        public string DeletedBy { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}

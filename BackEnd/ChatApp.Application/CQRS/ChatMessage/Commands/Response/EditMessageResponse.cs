using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.CQRS.ChatMessage.Commands.Response
{
    public class EditMessageResponse
    {
        public Guid MessageId { get; set; }
        public string NewContent { get; set; }
        public DateTime EditedAt { get; set; }
    }
}

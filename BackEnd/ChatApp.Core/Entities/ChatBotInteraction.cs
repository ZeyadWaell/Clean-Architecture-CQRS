using ChatApp.Core.Entities.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Entities
{
    public class ChatBotInteraction : BaseEntity
    {

        public Guid TriggerMessageId { get; set; }
        [ForeignKey(nameof(TriggerMessageId))]
        public ChatMessage TriggerMessage { get; set; }

        public string Query { get; set; }

        public string Response { get; set; }

        public DateTime Timestamp { get; set; }
    }
}

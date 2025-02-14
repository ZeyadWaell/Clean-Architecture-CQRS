using ChatApp.Core.Entities.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Entities
{
    public class ChatRoom : BaseEntity
    {

        public string Name { get; set; }

        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

        public ICollection<ChatRoomMember> Members { get; set; } = new List<ChatRoomMember>();

    }
}

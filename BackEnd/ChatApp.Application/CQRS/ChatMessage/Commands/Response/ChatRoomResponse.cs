using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.CQRS.ChatMessage.Commands.Response
{

        public class ChatRoomResponse
        {
            public Guid ChatRoomId { get; set; }
            public string RoomName { get; set; }
            public List<string> Participants { get; set; } = new List<string>();
            public DateTime LastActive { get; set; }
        }
    

}

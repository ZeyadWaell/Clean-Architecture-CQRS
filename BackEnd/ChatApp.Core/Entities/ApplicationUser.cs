using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string NikName { get; set; }
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

        public ICollection<ChatRoomMember> ChatRoomMembers { get; set; } = new List<ChatRoomMember>();

    }
}

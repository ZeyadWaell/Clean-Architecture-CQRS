using ChatApp.Core.Entities;
using ChatApp.Infrastructure.Data;
using ChatApp.Infrastructure.Repositories.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Repositories
{
    public class ChatRoomMemberRepository : GenericRepository<ChatRoomMember>, IChatRoomMemberRepository
    {
        public ChatRoomMemberRepository(ChatDbContext context) : base(context)
        {
        }
    }
}

using ChatApp.Core.Entities;
using ChatApp.Infrastructure.Data;
using ChatApp.Infrastructure.Repositories.Main;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Repositories
{
    public class ChatMessageRepository : GenericRepository<ChatMessage>, IChatMessageRepository
    {
        public ChatMessageRepository(ChatDbContext context) : base(context)
        {
        }
        public async Task<List<ChatMessage>> GetMessagesByChatRoomAsync(Guid chatRoomId)
        {
            return await _dbContext.ChatMessages 
                .Where(m => m.ChatRoomId == chatRoomId)
                 .Include(m => m.User)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }
    }
}

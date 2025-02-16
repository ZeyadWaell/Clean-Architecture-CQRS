using ChatApp.Core.Entities;
using ChatApp.Infrastructure.Data;
using ChatApp.Infrastructure.Repositories.Main;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Repositories
{
    public class ChatMessageRepository : GenericRepository<ChatMessage>, IChatMessageRepository
    {
        private readonly ChatDbContext _chatDbContext;
        private readonly ApplicationIdentityDbContext _userDbContext; 

        public ChatMessageRepository(ChatDbContext chatDbContext, ApplicationIdentityDbContext userDbContext)
            : base(chatDbContext)
        {
            _chatDbContext = chatDbContext;
            _userDbContext = userDbContext;
        }

        public async Task<List<ChatMessage>> GetMessagesByChatRoomAsync(Guid chatRoomId)
        {
            var messages = await _chatDbContext.ChatMessages
           .Where(m => m.ChatRoomId == chatRoomId)
           .OrderByDescending(m => m.Timestamp) 
           .Take(5)                        
           .ToListAsync();


            if (messages.Any())
            {
                var userIds = messages.Select(m => m.UserId).Distinct().ToList();

                var users = await _userDbContext.Users
                    .Where(u => userIds.Contains(u.Id))
                    .ToListAsync();

                foreach (var message in messages)
                {
                    message.User = users.FirstOrDefault(u => u.Id == message.UserId);
                }
            }

            return messages;
        }
    }
}

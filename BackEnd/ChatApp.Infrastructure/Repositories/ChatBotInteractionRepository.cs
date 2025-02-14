using ChatApp.Core.Entities;
using ChatApp.Core.Interfaces;
using ChatApp.Infrastructure.Data;
using ChatApp.Infrastructure.Repositories.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Repositories
{
    public class ChatBotInteractionRepository : GenericRepository<ChatBotInteraction>, IChatBotInteractionRepository
    {
        public ChatBotInteractionRepository(ChatDbContext context) : base(context)
        {
        }
    }
}

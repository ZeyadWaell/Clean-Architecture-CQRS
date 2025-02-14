using ChatApp.Core.Entities;
using ChatApp.Core.Interfaces.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Repositories
{
    public interface IChatMessageRepository : IGenericRepository<ChatMessage>
    {
        Task<List<ChatMessage>> GetMessagesByChatRoomAsync(Guid chatRoomId);

    }
}

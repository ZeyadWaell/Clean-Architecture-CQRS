using ChatApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Interfaces.Main
{
    public interface IUnitOfWork
    {
        IChatMessageRepository ChatMessageRepository { get; }
        IChatRoomRepository ChatRoomRepository { get; }
        IChatRoomMemberRepository ChatRoomMemberRepository { get; }
        Task<int> CompleteAsync();
    }
}

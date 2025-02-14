using System.Threading.Tasks;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Interfaces.Main;
using ChatApp.Infrastructure.Data;
using ChatApp.Infrastructure.Repositories;

namespace ChatApp.Infrastructure.Repositories.Main
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatDbContext _context;
        private IChatMessageRepository _chatMessageRepository;
        private IChatRoomRepository _chatRoomRepository;
        private IChatRoomMemberRepository _chatRoomMemberRepository;

        public UnitOfWork(ChatDbContext context,
                          IChatMessageRepository chatMessageRepository,
                          IChatRoomRepository chatRoomRepository,
                          IChatRoomMemberRepository chatRoomMemberRepository)
        {
            _context = context;
            _chatMessageRepository = chatMessageRepository;
            _chatRoomRepository = chatRoomRepository;
            _chatRoomMemberRepository = chatRoomMemberRepository;
        }

        public IChatMessageRepository ChatMessageRepository => _chatMessageRepository;

        public IChatRoomRepository ChatRoomRepository => _chatRoomRepository;

        public IChatRoomMemberRepository ChatRoomMemberRepository => _chatRoomMemberRepository;

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

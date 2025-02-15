using ChatApp.Application.CQRS.ChatRoom.Queries.Response;
using ChatApp.Application.Services.inteface;
using ChatApp.Application.Utilities.Class;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.CQRS.ChatRoom.Queries.Models
{
    public class GetAllRoomsQuery : IRequest<ApiResponse<List<ChatRoomAllResponse>>>, IBaseRequest
    {
    }
    public class GetAllRoomsRequestHandler : IRequestHandler<GetAllRoomsQuery, ApiResponse<List<ChatRoomAllResponse>>>
    {
        private readonly IChatService _chatService;

        public GetAllRoomsRequestHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<ApiResponse<List<ChatRoomAllResponse>>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            return await _chatService.GetAllRoomAsync(request);
        }
    }
}

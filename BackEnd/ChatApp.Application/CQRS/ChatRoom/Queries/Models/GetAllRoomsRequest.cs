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
    public class GetAllRoomsRequest : IRequest<ApiResponse<List<ChatRoomAllResponse>>>, IBaseRequest
    {
    }
    public class GetAllRoomsRequestHandler : IRequestHandler<GetAllRoomsRequest, ApiResponse<List<ChatRoomAllResponse>>>
    {
        private readonly IChatService _chatService;

        public GetAllRoomsRequestHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<ApiResponse<List<ChatRoomAllResponse>>> Handle(GetAllRoomsRequest request, CancellationToken cancellationToken)
        {
            return await _chatService.GetAllRoomAsync(request);
        }
    }
}
